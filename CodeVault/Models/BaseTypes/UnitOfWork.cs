using CodeVault.Models;
using System;
using System.Data.Entity;

namespace CodeVault.Models.BaseTypes
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Stores

        private DbContext dbContext = null;
        private bool disposed = false;
        private IRepository<Product> productRepo = null;
        private IRepository<Request> requestRepo = null;
        private IRepository<ProductPermission> permissionRepo = null;
        private IRepository<ProductPermissionDetail> permissionDetailRepo = null;
        private IRepository<License> licenseRepo = null;
        private IRepository<CosmicConfigRecord> configRecordRepo = null;

        #endregion Stores

        #region Properties

        public IRepository<Product> ProductRepo
        {
            get
            {
                if (this.productRepo == null)
                    this.productRepo = new Repository<Product>(this.dbContext);
                return this.productRepo;
            }
        }

        public IRepository<Request> RequestRepo
        {
            get
            {
                if (this.requestRepo == null)
                    this.requestRepo = new Repository<Request>(this.dbContext);
                return this.requestRepo;
            }
        }

        public IRepository<ProductPermission> PermissionRepo
        {
            get
            {
                if (this.permissionRepo == null)
                    this.permissionRepo = new Repository<ProductPermission>(this.dbContext);
                return this.permissionRepo;
            }
        }

        public IRepository<ProductPermissionDetail> PermissionDetailRepo
        {
            get
            {
                if (this.permissionDetailRepo == null)
                    this.permissionDetailRepo = new Repository<ProductPermissionDetail>(this.dbContext);
                return this.permissionDetailRepo;
            }
        }

        public IRepository<License> LicenseRepo
        {
            get
            {
                if (this.licenseRepo == null)
                    this.licenseRepo = new Repository<License>(this.dbContext);
                return this.licenseRepo;
            }
        }

        public IRepository<CosmicConfigRecord> ConfigRecordRepo
        {
            get
            {
                if (this.configRecordRepo == null)
                    this.configRecordRepo = new Repository<CosmicConfigRecord>(this.dbContext);
                return this.configRecordRepo;
            }
        }

        #endregion Properties

        #region Constructors

        public UnitOfWork(DbContext context)
        {
            this.dbContext = context;
        }

        #endregion Constructors

        #region Public Methods

        public void CommitChanges()
        {
            this.dbContext.SaveChanges();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.dbContext.Dispose();
            }
            this.disposed = true;
        }

        #endregion Protected Methods

        #region IDisposable Overrides

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Overrides
    }
}