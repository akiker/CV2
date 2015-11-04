namespace CodeVault.Models.BaseTypes
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepo { get; }

        IRepository<Request> RequestRepo { get; }

        IRepository<ProductPermission> PermissionRepo { get; }

        IRepository<ProductPermissionDetail> PermissionDetailRepo { get; }

        IRepository<License> LicenseRepo { get; }

        IRepository<CosmicConfigRecord> ConfigRecordRepo { get; }

        /// <summary>
        ///     Used to save the changes to the underlying data store
        /// </summary>
        void CommitChanges();
    }
}