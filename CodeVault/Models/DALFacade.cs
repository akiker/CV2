using System;
using System.Data;
using System.Data.Common;
using CodeVault.Models.BaseTypes;

namespace CodeVault.Models
{
    public class DalFacade : IDalFacade
    {
        #region Constructors

        #endregion Constructors

        #region Stores

        /// <summary>
        ///     Data base connection used when working in connected state.
        /// </summary>
        private readonly DbConnection _dbConnection = null;

        /// <summary>
        ///     Managed instance of the DbContext by the DALFacade. Only one context is used at any time.
        ///     Injected into unit of work.
        /// </summary>
        private Cv2Context _context;

        /// <summary>
        ///     Instance of unit of work managed by the facade. Only one unit of work is used at any time.
        /// </summary>
        private UnitOfWork _unitOfWork;

        public DalFacade(bool keepConnectionAlive)
        {
            KeepConnectionAlive = keepConnectionAlive;
        }

        /// <summary>
        ///     Used to initialize the database preemptively on first run.
        /// </summary>
        /// <summary>
        ///     Creates and returns a unit of work to the caller.
        ///     If a unit of work is already being used then it throws an exception.
        ///     Allows only one instance of unit of work to exists.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork()
        {
            if (_unitOfWork != null)
                throw new Exception("A unit of work is already in use.");
            _context = new Cv2Context();
            _unitOfWork = new UnitOfWork(_context);
            return _unitOfWork;
        }

        public void DisposeUnitOfWork()
        {
            if (_unitOfWork != null)
            {
                //If we do not own the connection, it should be already closed
                if (!KeepConnectionAlive)
                {
                    if (_dbConnection != null)
                        throw new Exception("Database connection is not null in disconnected state.");
                }
                else if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                    throw new Exception("Database connection is not open in connected state.");
                _unitOfWork.Dispose();
                _unitOfWork = null;
                _context = null;
            }
        }

        #endregion IDALFacade Implementation

        #region Properties

        /// <summary>
        ///     Data base name used by the application.
        /// </summary>
        public string DatabaseName { get; set; }

        public string ServerName { get; set; }

        public string Port { get; set; }

        /// <summary>
        ///     Reads and holds the "KeepConnectionAlive" setting from the app.config file.
        ///     If no setting is found it reverts to false for disconnected operation.
        /// </summary>
        public bool KeepConnectionAlive { get; }

        #endregion Properties
    }
}