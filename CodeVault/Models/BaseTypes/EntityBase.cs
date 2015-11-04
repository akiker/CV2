using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeVault.Models.BaseTypes
{
    /// <summary>
    ///     Base class that all entities inherit from
    /// </summary>
    public abstract class EntityBase : ISetProperty, IValidatableObject
    {
        #region IValidatableObject

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ICollection<ValidationResult> result = new Collection<ValidationResult>();
            result.Add(ValidationResult.Success);
            return result;
        }

        #endregion IValidatableObject

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Method called by the derived classes when a property changes and clients need to be notified
        /// </summary>
        /// <param name="propertyName">Name of the property that has been changed</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        #region ISetProperty

        /// <summary>
        ///     Holds the property names and corresponding validate methods for properties
        /// </summary>
        private readonly Dictionary<string, Func<object, IEnumerable<string>>> _validationMethods =
            new Dictionary<string, Func<object, IEnumerable<string>>>();

        /// <summary>
        ///     Adds a validation method to the dictionary
        /// </summary>
        /// <param name="propertyName">Property that will raise the validation method</param>
        /// <param name="method">Delegate that validates the property and returns an IEnumerable containing errors</param>
        public void AddValidationMethod(string propertyName, Func<object, IEnumerable<string>> method)
        {
            _validationMethods.Add(propertyName, method);
        }

        /// <summary>
        ///     Validates a property using the validation method in the dictionary. Called privately
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="newValue">true if no errors, false otherwise</param>
        /// <returns></returns>
        private bool ValidateProperty<T>(string propertyName, T newValue)
        {
            Func<object, IEnumerable<string>> validator = null;

            if (!_validationMethods.TryGetValue(propertyName, out validator))
                throw new MissingMethodException("No validation method is added to the validation dictionary for " +
                                                 propertyName + " property");
            var results = validator(newValue);
            SetErrors(propertyName, validator(newValue));

            return results == null;
        }

        /// <summary>
        ///     Sets a property based on the value being different and performs validation
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="backingField">Backing field of the property</param>
        /// <param name="newValue">New value of the property</param>
        public void SetProperty<T>(string propertyName, ref T backingField, T newValue)
        {
            if (Equals(backingField, newValue)) return;
            if (!ValidateProperty(propertyName, newValue)) return;
            backingField = newValue;
            RaiseErrorsChanged(propertyName);
        }

        #endregion ISetProperty

        #region INotifyDataErrorInfo

        /// <summary>
        ///     Dictionary that holds data errors for properties.
        /// </summary>
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        ///     Event raised when a change in error dictionary occurs
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Called by the derived classes to notify a change in the errors dictionary
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged("HasErrors");
            RaisePropertyChanged("NoErrors");
            RaisePropertyChanged("ErrorCount");
        }

        /// <summary>
        ///     Used to get errors by supplying the property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }

        /// <summary>
        ///     Is true if at least one error is in the error dictionary
        /// </summary>
        public bool HasErrors => (_errors.Count > 0);

        /// <summary>
        ///     Returns the total number of errors in the dictionary
        /// </summary>
        public int ErrorCount => _errors.Count;

        /// <summary>
        ///     Is true if there are no errors in the dictionary
        /// </summary>
        public bool NoErrors => (_errors.Count == 0);

        /// <summary>
        ///     Sets the errors for a property
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="errors">An IEnumberable containing the errors. If this parameter is null all errors are cleared</param>
        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            if (errors != null) return;
            //Clean up all errors for this property if the enumeration is empty
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
            else
            {
                //Create a new entry for the property and add the error list
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors.Add(propertyName, new List<string>(errors));
                }
                else
                {
                    //Replace the whole error list with the new one
                    _errors[propertyName] = new List<string>(errors);
                }
            }
            RaiseErrorsChanged(propertyName);
        }

        #endregion INotifyDataErrorInfo

        #region Common Methods

        /// <summary>
        ///     Registers a validation method for each property
        /// </summary>
        protected abstract void RegisterValidationMethods();

        /// <summary>
        ///     Resets all properties to default values
        /// </summary>
        protected abstract void ResetProperties();

        #endregion Common Methods
    }
}