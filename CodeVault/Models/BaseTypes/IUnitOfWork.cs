using CodeVault.Models;

namespace CodeVault.Models.BaseTypes
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepo { get; }

        IRepository<Request> RequestRepo { get; }

        /// <summary>
        /// Used to save the changes to the underlying data store
        /// </summary>
        void CommitChanges();
    }
}