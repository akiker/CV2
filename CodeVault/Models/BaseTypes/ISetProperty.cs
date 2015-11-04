﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeVault.Models.BaseTypes
{
    public interface ISetProperty : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        ///     Adds a validation method to the dictionary
        /// </summary>
        /// <param name="propertyName">Property that will use the validation method</param>
        /// <param name="method">Delegate that validates the property and returns an IEnumberable containing errors</param>
        void AddValidationMethod(string propertyName, Func<object, IEnumerable<string>> method);

        /// <summary>
        ///     Inheriting classes should call this method to set their properties so that the property notification and
        ///     data error notification can be provided
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="backingField">Private storage backing the property</param>
        /// <param name="newValue">Value to be assigned to the property</param>
        void SetProperty<T>(string propertyName, ref T backingField, T newValue);
    }
}