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