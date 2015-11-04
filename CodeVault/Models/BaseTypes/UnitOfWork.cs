using System;
using System.Data.Entity;

namespace CodeVault.Models.BaseTypes
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructors

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        #endregion Constructors

        #region IDisposable Overrides

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Overrides

        #region Public Methods

        public void CommitChanges()
        {
            _dbContext.SaveChanges();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }
            _disposed = true;
        }

        #endregion Protected Methods

        #region Stores

        private readonly DbContext _dbContext;
        private bool _disposed;
        private IRepository<Product> _productRepo;
        private IRepository<Request> _requestRepo;
        private IRepository<ProductPermission> _permissionRepo;
        private IRepository<ProductPermissionDetail> _permissionDetailRepo;
        private IRepository<License> _licenseRepo;
        private IRepository<CosmicConfigRecord> _configRecordRepo;

        #endregion Stores

        #region Properties

        public IRepository<Product> ProductRepo => _productRepo ?? (_productRepo = new Repository<Product>(_dbContext));

        public IRepository<Request> RequestRepo => _requestRepo ?? (_requestRepo = new Repository<Request>(_dbContext));

        public IRepository<ProductPermission> PermissionRepo
            => _permissionRepo ?? (_permissionRepo = new Repository<ProductPermission>(_dbContext));

        public IRepository<ProductPermissionDetail> PermissionDetailRepo => _permissionDetailRepo ??
                                                                            (_permissionDetailRepo =
                                                                                new Repository<ProductPermissionDetail>(
                                                                                    _dbContext));

        public IRepository<License> LicenseRepo => _licenseRepo ?? (_licenseRepo = new Repository<License>(_dbContext));

        public IRepository<CosmicConfigRecord> ConfigRecordRepo
            => _configRecordRepo ?? (_configRecordRepo = new Repository<CosmicConfigRecord>(_dbContext));

        #endregion Properties
    }
}