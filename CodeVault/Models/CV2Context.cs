namespace CodeVault.Models
{
    using CodeVault.Models;
    using CV2.Models.Utilities;
    using EntityFramework.Triggers;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class CV2Context : DbContext
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CV2Context()
            : base("name=CV2Context")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public CV2Context(string databaseName)
            : base(databaseName)
        {
        }

        public CV2Context(DbConnection connection, bool ownsConnection)
            : base(connection, contextOwnsConnection: ownsConnection)
        {
        }

        public virtual DbSet<ApplicationPermission> ApplicationPermissions { get; set; }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<CosmicConfigRecord> CosmicConfigRecords { get; set; }

        public virtual DbSet<Dependencies> Dependencies { get; set; }

        public virtual DbSet<DistributionLocation> DistributionLocations { get; set; }

        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<DocumentAttachment> Attachments { get; set; }

        public virtual DbSet<InstallDetail> InstallDetails { get; set; }

        public virtual DbSet<JournalEntry> JournalEntries { get; set; }

        public virtual DbSet<KeyWord> KeyWords { get; set; }

        public virtual DbSet<LicenseKey> LicenseKeys { get; set; }

        public virtual DbSet<License> Licenses { get; set; }

        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public virtual DbSet<OsRequirement> OsRequirements { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<ProductContactRole> ProductContactRoles { get; set; }

        public virtual DbSet<ProductContact> ProductContacts { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductKeyWord> ProductKeyWords { get; set; }

        public virtual DbSet<ProductType> ProductTypes { get; set; }

        public virtual DbSet<ProjectType> ProjectTypes { get; set; }

        public virtual DbSet<ProductPermission> ProductPermissions { get; set; }

        public virtual DbSet<ProductPermissionDetail> ProductPermissionDetails { get; set; }

        public virtual DbSet<LAVerification> LocalAdminVerification { get; set; }

        public virtual DbSet<RequestHistory> RequestHistories { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<RequestStatus> RequestStatus { get; set; }

        public virtual DbSet<RequestType> RequestTypes { get; set; }

        public virtual DbSet<SccmDeploymentDetail> SccmDeploymentDetails { get; set; }

        public virtual DbSet<SccmReturnCode> SccmReturnCodes { get; set; }

        public virtual DbSet<SccmRule> SccmRules { get; set; }

        public virtual DbSet<SupersededProducts> SupersededProducts { get; set; }

        public virtual DbSet<SoftwarePolicy> SoftwarePolicies { get; set; }

        public virtual DbSet<SoftwarePolicySupportLevel> SoftwarePolicySupportLevels { get; set; }

        public virtual DbSet<SoftwarePolicyGroupAssociation> SoftwarePolicyGroupAssociations { get; set; }

        public virtual DbSet<SoftwareRequirement> SoftwareRequirements { get; set; }

        public virtual DbSet<SystemRequirement> SystemRequirements { get; set; }

        public virtual DbSet<WorkQueue> WorkQueues { get; set; }

        public int SaveChanges(string userName, int productId, string journalEntryFieldNameOrTitle)
        {
            foreach (var entity in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                foreach (JournalEntry entry in this.GetJournalEntriesForChange(entity, userName, productId, journalEntryFieldNameOrTitle))
                {
                    this.JournalEntries.Add(entry);
                }
            }
            return this.SaveChangesWithTriggers(base.SaveChanges);
        }

        /// <summary>
        /// Saves changes to the underlying provider
        /// </summary>
        /// <param name="userName">User performing the update</param>
        /// <param name="productId">ProductId of the parent product</param>
        /// <param name="childProductId">ProductIf of the child product</param>
        /// <returns></returns>
        public int SaveChanges(string userName, int productId, int childProductId)
        {
            foreach (var entity in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                foreach (JournalEntry entry in this.GetJournalEntriesForChange(entity, userName, productId, childProductId))
                {
                    this.JournalEntries.Add(entry);
                }
            }
            return this.SaveChangesWithTriggers(base.SaveChanges);
        }

        public override int SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, cancellationToken);
        }

        /// <summary>
        /// Overloaded call to add a journal entry to one to many entities like product->documents
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userName"></param>
        /// <param name="parentProductId"></param>
        /// <param name="journalFieldNameOrTitle"></param>
        /// <returns></returns>
        private List<JournalEntry> GetJournalEntriesForChange(DbEntityEntry entry, string userName, int parentProductId, string journalFieldNameOrTitle)
        {
            List<JournalEntry> result = new List<JournalEntry>();
            var parentProduct = this.Products.Find(parentProductId);
            var entitySetBase = this.GetEntitySet(entry.Entity.GetType());
            PropertyMapper propertyMapper = new PropertyMapper();
            string displayText = propertyMapper.GetFriendlyTextForProperty(entitySetBase.Name) ?? entitySetBase.Name;

            if (entry.State == EntityState.Added)
            {
                result.Add(new JournalEntry()
                {
                    JournalEntryCreatedBy = userName,
                    JournalEntryCreatedOn = System.DateTime.Now,
                    JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Added:</b> {0} -&gt; '{1}'</span></html>", displayText, journalFieldNameOrTitle),
                    JournalEntryType = JournalEntryType.System,
                    ProductId = parentProductId
                });
                logger.Info("Added: {0} -> '{1}'", displayText, journalFieldNameOrTitle);
            }
            else if (entry.State == EntityState.Deleted)
            {
                result.Add(new JournalEntry()
                {
                    JournalEntryCreatedBy = userName,
                    JournalEntryCreatedOn = System.DateTime.Now,
                    JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Removed:</b> {0} -&gt; '{1}'</span></html>", displayText, journalFieldNameOrTitle),
                    JournalEntryType = JournalEntryType.System,
                    ProductId = parentProductId
                });
                logger.Info("Removed: {0} -> '{1}'", displayText, journalFieldNameOrTitle);
            }
            else if (entry.State == EntityState.Modified)
            {
                foreach (var propertyName in entry.OriginalValues.PropertyNames)
                {
                    //Only capture the values that have changed
                    if (!object.Equals(entry.OriginalValues.GetValue<object>(propertyName), entry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        string currentValue = entry.CurrentValues.GetValue<object>(propertyName) == null ? "Null" : entry.CurrentValues.GetValue<object>(propertyName).ToString();
                        currentValue = string.IsNullOrEmpty(currentValue) ? "Null" : currentValue;
                        if (currentValue.Contains("System.Byte[]"))
                        {
                            var currentByteValue = (byte[])entry.CurrentValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(currentByteValue.LongLength);
                            currentValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        string originalValue = entry.OriginalValues.GetValue<object>(propertyName) == null ? "Null" : entry.OriginalValues.GetValue<object>(propertyName).ToString();
                        originalValue = string.IsNullOrEmpty(originalValue) ? "Null" : originalValue;
                        if (originalValue.Contains("System.Byte[]"))
                        {
                            var originalByteValue = (byte[])entry.OriginalValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(originalByteValue.LongLength);
                            originalValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        displayText = propertyMapper.GetFriendlyTextForProperty(propertyName) ?? propertyName;

                        result.Add(new JournalEntry()
                        {
                            JournalEntryCreatedBy = userName,
                            JournalEntryCreatedOn = System.DateTime.Now,
                            JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Changed:</b> {0} -&gt; <i>Old Value: '{1}'</i> <b>New Value: '{2}'</b></span></html>", displayText, originalValue, currentValue),
                            JournalEntryType = JournalEntryType.System,
                            ProductId = parentProductId
                        });
                        logger.Info("Changed: {0} -> Old Value: '{1}' New Value: {2}", displayText, originalValue, currentValue);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Overloaded call to add a journal entry to nested entities like product->dependency
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userName"></param>
        /// <param name="parentProductId"></param>
        /// <param name="childProductId"></param>
        /// <returns></returns>
        private List<JournalEntry> GetJournalEntriesForChange(DbEntityEntry entry, string userName, int parentProductId, int childProductId)
        {
            List<JournalEntry> result = new List<JournalEntry>();

            var parentProduct = this.Products.Find(parentProductId);
            var childProduct = this.Products.Find(childProductId);

            var entitySetBase = this.GetEntitySet(entry.Entity.GetType());
            PropertyMapper propertyMapper = new PropertyMapper();
            string displayText = propertyMapper.GetFriendlyTextForProperty(entitySetBase.Name) ?? entitySetBase.Name;

            if (entry.State == EntityState.Added)
            {
                result.Add(new JournalEntry()
                {
                    JournalEntryCreatedBy = userName,
                    JournalEntryCreatedOn = System.DateTime.Now,
                    JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Added:</b> {0} -&gt; {1}</span></html>", displayText, childProduct.ProductName),
                    JournalEntryType = JournalEntryType.System,
                    ProductId = parentProductId
                });
                logger.Info("Added: {0} -> '{1}'", displayText, childProduct.ProductName);
            }
            else if (entry.State == EntityState.Deleted)
            {
                result.Add(new JournalEntry()
                {
                    JournalEntryCreatedBy = userName,
                    JournalEntryCreatedOn = System.DateTime.Now,
                    JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Removed:</b> {0} -&gt; {1}</span></html>", displayText, childProduct.ProductName),
                    JournalEntryType = JournalEntryType.System,
                    ProductId = parentProductId
                });
                logger.Info("Removed: {0} -> '{1}'", displayText, childProduct.ProductName);
            }
            else if (entry.State == EntityState.Modified)
            {
                foreach (var propertyName in entry.OriginalValues.PropertyNames)
                {
                    //Only capture the values that have changed
                    if (!object.Equals(entry.OriginalValues.GetValue<object>(propertyName), entry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        string currentValue = entry.CurrentValues.GetValue<object>(propertyName) == null ? "Null" : entry.CurrentValues.GetValue<object>(propertyName).ToString();
                        currentValue = string.IsNullOrEmpty(currentValue) ? "Null" : currentValue;
                        if (currentValue.Contains("System.Byte[]"))
                        {
                            var currentByteValue = (byte[])entry.CurrentValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(currentByteValue.LongLength);
                            currentValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        string originalValue = entry.OriginalValues.GetValue<object>(propertyName) == null ? "Null" : entry.OriginalValues.GetValue<object>(propertyName).ToString();
                        originalValue = string.IsNullOrEmpty(originalValue) ? "Null" : originalValue;
                        if (originalValue.Contains("System.Byte[]"))
                        {
                            var originalByteValue = (byte[])entry.OriginalValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(originalByteValue.LongLength);
                            originalValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }

                        displayText = propertyMapper.GetFriendlyTextForProperty(propertyName) ?? propertyName;
                        result.Add(new JournalEntry()
                        {
                            JournalEntryCreatedBy = userName,
                            JournalEntryCreatedOn = System.DateTime.Now,
                            JournalEntryText = string.Format("<html><span style=\"font-size: 9pt;\"><b>Changed:</b> {0} -&gt; <i>Old Value: '{1}'</i> <b>New Value: '{2}' </b></span></html>", displayText, originalValue, currentValue),
                            JournalEntryType = JournalEntryType.System,
                            ProductId = parentProductId
                        });
                        logger.Info("Changed: {0} -> Old Value: '{1}' New Value: {2}", displayText, originalValue, currentValue);
                    }
                }
            }
            return result;
        }

        private readonly static Dictionary<Type, EntitySetBase> mappingCache = new Dictionary<Type, EntitySetBase>();

        private ObjectContext SelfContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            type = GetObjectType(type);
            if (mappingCache.ContainsKey(type)) return mappingCache[type];

            string baseTypeName = type.BaseType.Name;
            string typeName = type.Name;

            ObjectContext objContext = SelfContext;
            var entitySetBase = objContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                    .GetItems<EntityContainer>()
                    .SelectMany(c => c.BaseEntitySets
                                    .Where(e => e.Name == typeName
                                    || e.Name == baseTypeName))
                    .FirstOrDefault();
            if (entitySetBase == null)
            {
                throw new ArgumentException("Entity type not found in GetEntitySet", typeName);
            }
            mappingCache.Add(type, entitySetBase);
            return entitySetBase;
        }

        internal string GetTableName(Type type)
        {
            EntitySetBase entitySetBase = GetEntitySet(type);
            return string.Format("[{0}].[{1}]", entitySetBase.Schema, entitySetBase.Table);
        }

        internal Type GetObjectType(Type type)
        {
            return System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(type);
        }

        public System.Data.Entity.DbSet<CodeVault.Models.ViewModels.ProductViewModel> ProductViewModels { get; set; }

        public System.Data.Entity.DbSet<CodeVault.Models.ViewModels.PermissionViewModel> PermissionViewModels { get; set; }

        public System.Data.Entity.DbSet<CodeVault.Models.ViewModels.PermissionDetailViewModel> PermissionDetailViewModels { get; set; }

        public System.Data.Entity.DbSet<CodeVault.Models.ViewModels.LicenseDetailViewModel> LicenseDetailViewModels { get; set; }
    }
}