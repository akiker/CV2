using CodeVault.Models.BaseTypes;
using CodeVault.Models;
using System;
using System.Data.Common;

namespace CodeVault.Models
{
    public class DALFacade : IDALFacade
    {
        #region Constructors

        public DALFacade()
        {
        }

        #endregion Constructors

        #region Stores

        /// <summary>
        /// Data base connection used when working in connected state.
        /// </summary>
        private DbConnection dbConnection = null;

        /// <summary>
        /// Managed instance of the DbContext by the DALFacade. Only one context is used at any time.
        /// Injected into unit of work.
        /// </summary>
        private CV2Context context = null;

        /// <summary>
        /// Instance of unit of work managed by the facade. Only one unit of work is used at any time.
        /// </summary>
        private UnitOfWork unitOfWork;

        /// <summary>
        /// Used to initialize the database preemptively on first run.
        /// </summary>
        //private bool firstRequest = true;

        #endregion Stores

        #region IDALFacade Implementation

        /// <summary>
        /// Creates and returns a unit of work to the caller.
        /// If a unit of work is already being used then it throws an exception.
        /// Allows only one instance of unit of work to exists.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork()
        {
            if (this.unitOfWork != null)
                throw new Exception("A unit of work is already in use.");
            else
            {
                this.context = new CV2Context();
                this.unitOfWork = new UnitOfWork(context);
                return this.unitOfWork;
            }
        }

        public void DisposeUnitOfWork()
        {
            if (this.unitOfWork != null)
            {
                //If we do not own the connection, it should be already closed
                if (!KeepConnectionAlive)
                {
                    if (this.dbConnection != null)
                        throw new Exception("Database connection is not null in disconnected state.");
                }
                else if (this.dbConnection == null || this.dbConnection.State != System.Data.ConnectionState.Open)
                    throw new Exception("Database connection is not open in connected state.");
                this.unitOfWork.Dispose();
                this.unitOfWork = null;
                this.context = null;
            }
        }

        #endregion IDALFacade Implementation

        #region Properties

        /// <summary>
        /// Data base name used by the application.
        /// </summary>
        public string DatabaseName { get; set; }

        public string ServerName { get; set; }

        public string Port { get; set; }

        /// <summary>
        /// Reads and holds the "KeepConnectionAlive" setting from the app.config file.
        /// If no setting is found it reverts to false for disconnected operation.
        /// </summary>
        public bool KeepConnectionAlive { get; private set; }

        #endregion Properties
    }
}