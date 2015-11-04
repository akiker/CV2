using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeVault.Models.Utilities;
using CodeVault.ViewModels;
using EntityFramework.Triggers;
using NLog;

namespace CodeVault.Models
{
    public class Cv2Context : DbContext
    {
        private static readonly Dictionary<Type, EntitySetBase> MappingCache = new Dictionary<Type, EntitySetBase>();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Cv2Context()
            : base("name=CV2Context")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
            Database.Log = sql => Debug.Write(sql);
        }

        public Cv2Context(string databaseName)
            : base(databaseName)
        {
        }

        public Cv2Context(DbConnection connection, bool ownsConnection)
            : base(connection, ownsConnection)
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

        public virtual DbSet<LaVerification> LocalAdminVerification { get; set; }

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

        private ObjectContext SelfContext => (this as IObjectContextAdapter).ObjectContext;

        public DbSet<ProductViewModel> ProductViewModels { get; set; }

        public DbSet<PermissionDetailViewModel> PermissionDetailViewModels { get; set; }

        public DbSet<LicenseViewModel> LicenseDetailViewModels { get; set; }

        public int SaveChanges(string userName, int productId, string journalEntryFieldNameOrTitle)
        {
            foreach (
                var entry in
                    ChangeTracker.Entries()
                        .Where(
                            p =>
                                p.State == EntityState.Added || p.State == EntityState.Deleted ||
                                p.State == EntityState.Modified)
                        .SelectMany(
                            entity =>
                                GetJournalEntriesForChange(entity, userName, productId, journalEntryFieldNameOrTitle)))
            {
                JournalEntries.Add(entry);
            }
            return this.SaveChangesWithTriggers(base.SaveChanges);
        }

        /// <summary>
        ///     Saves changes to the underlying provider
        /// </summary>
        /// <param name="userName">User performing the update</param>
        /// <param name="productId">ProductId of the parent product</param>
        /// <param name="childProductId">ProductIf of the child product</param>
        /// <returns></returns>
        public int SaveChanges(string userName, int productId, int childProductId)
        {
            foreach (
                var entry in
                    ChangeTracker.Entries()
                        .Where(
                            p =>
                                p.State == EntityState.Added || p.State == EntityState.Deleted ||
                                p.State == EntityState.Modified)
                        .SelectMany(entity => GetJournalEntriesForChange(entity, userName, productId, childProductId)))
            {
                JournalEntries.Add(entry);
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
        ///     Overloaded call to add a journal entry to one to many entities like product->documents
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userName"></param>
        /// <param name="parentProductId"></param>
        /// <param name="journalFieldNameOrTitle"></param>
        /// <returns></returns>
        private IEnumerable<JournalEntry> GetJournalEntriesForChange(DbEntityEntry entry, string userName,
            int parentProductId, string journalFieldNameOrTitle)
        {
            var result = new List<JournalEntry>();
            var parentProduct = Products.Find(parentProductId);
            var entitySetBase = GetEntitySet(entry.Entity.GetType());
            var propertyMapper = new PropertyMapper();
            var displayText = propertyMapper.GetFriendlyTextForProperty(entitySetBase.Name) ?? entitySetBase.Name;

            switch (entry.State)
            {
                case EntityState.Added:
                    result.Add(new JournalEntry
                    {
                        JournalEntryCreatedBy = userName,
                        JournalEntryCreatedOn = DateTime.Now,
                        JournalEntryText =
                            $"<html><span style=\"font-size: 9pt;\"><b>Added:</b> {displayText} -&gt; '{journalFieldNameOrTitle}'</span></html>",
                        JournalEntryType = JournalEntryType.System,
                        ProductId = parentProductId
                    });
                    _logger.Info("Added: {0} -> '{1}'", displayText, journalFieldNameOrTitle);
                    break;
                case EntityState.Deleted:
                    result.Add(new JournalEntry
                    {
                        JournalEntryCreatedBy = userName,
                        JournalEntryCreatedOn = DateTime.Now,
                        JournalEntryText =
                            $"<html><span style=\"font-size: 9pt;\"><b>Removed:</b> {displayText} -&gt; '{journalFieldNameOrTitle}'</span></html>",
                        JournalEntryType = JournalEntryType.System,
                        ProductId = parentProductId
                    });
                    _logger.Info("Removed: {0} -> '{1}'", displayText, journalFieldNameOrTitle);
                    break;
                case EntityState.Modified:
                    foreach (var propertyName in entry.OriginalValues.PropertyNames)
                    {
                        //Only capture the values that have changed
                        if (Equals(entry.OriginalValues.GetValue<object>(propertyName),
                            entry.CurrentValues.GetValue<object>(propertyName))) continue;
                        var currentValue = entry.CurrentValues.GetValue<object>(propertyName) == null
                            ? "Null"
                            : entry.CurrentValues.GetValue<object>(propertyName).ToString();
                        currentValue = string.IsNullOrEmpty(currentValue) ? "Null" : currentValue;
                        if (currentValue.Contains("System.Byte[]"))
                        {
                            var currentByteValue = (byte[]) entry.CurrentValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(currentByteValue.LongLength);
                            currentValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        var originalValue = entry.OriginalValues.GetValue<object>(propertyName) == null
                            ? "Null"
                            : entry.OriginalValues.GetValue<object>(propertyName).ToString();
                        originalValue = string.IsNullOrEmpty(originalValue) ? "Null" : originalValue;
                        if (originalValue.Contains("System.Byte[]"))
                        {
                            var originalByteValue = (byte[]) entry.OriginalValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(originalByteValue.LongLength);
                            originalValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        displayText = propertyMapper.GetFriendlyTextForProperty(propertyName) ?? propertyName;

                        result.Add(new JournalEntry
                        {
                            JournalEntryCreatedBy = userName,
                            JournalEntryCreatedOn = DateTime.Now,
                            JournalEntryText =
                                $"<html><span style=\"font-size: 9pt;\"><b>Changed:</b> {displayText} -&gt; <i>Old Value: '{originalValue}'</i> <b>New Value: '{currentValue}'</b></span></html>",
                            JournalEntryType = JournalEntryType.System,
                            ProductId = parentProductId
                        });
                        _logger.Info("Changed: {0} -> Old Value: '{1}' New Value: {2}", displayText, originalValue,
                            currentValue);
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        /// <summary>
        ///     Overloaded call to add a journal entry to nested entities like product->dependency
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userName"></param>
        /// <param name="parentProductId"></param>
        /// <param name="childProductId"></param>
        /// <returns></returns>
        private IEnumerable<JournalEntry> GetJournalEntriesForChange(DbEntityEntry entry, string userName,
            int parentProductId, int childProductId)
        {
            var result = new List<JournalEntry>();

            var parentProduct = Products.Find(parentProductId);
            var childProduct = Products.Find(childProductId);

            var entitySetBase = GetEntitySet(entry.Entity.GetType());
            var propertyMapper = new PropertyMapper();
            var displayText = propertyMapper.GetFriendlyTextForProperty(entitySetBase.Name) ?? entitySetBase.Name;

            switch (entry.State)
            {
                case EntityState.Added:
                    result.Add(new JournalEntry
                    {
                        JournalEntryCreatedBy = userName,
                        JournalEntryCreatedOn = DateTime.Now,
                        JournalEntryText =
                            $"<html><span style=\"font-size: 9pt;\"><b>Added:</b> {displayText} -&gt; {childProduct.ProductName}</span></html>",
                        JournalEntryType = JournalEntryType.System,
                        ProductId = parentProductId
                    });
                    _logger.Info("Added: {0} -> '{1}'", displayText, childProduct.ProductName);
                    break;
                case EntityState.Deleted:
                    result.Add(new JournalEntry
                    {
                        JournalEntryCreatedBy = userName,
                        JournalEntryCreatedOn = DateTime.Now,
                        JournalEntryText =
                            $"<html><span style=\"font-size: 9pt;\"><b>Removed:</b> {displayText} -&gt; {childProduct.ProductName}</span></html>",
                        JournalEntryType = JournalEntryType.System,
                        ProductId = parentProductId
                    });
                    _logger.Info("Removed: {0} -> '{1}'", displayText, childProduct.ProductName);
                    break;
                case EntityState.Modified:
                    foreach (var propertyName in entry.OriginalValues.PropertyNames)
                    {
                        //Only capture the values that have changed
                        if (Equals(entry.OriginalValues.GetValue<object>(propertyName),
                            entry.CurrentValues.GetValue<object>(propertyName))) continue;
                        var currentValue = entry.CurrentValues.GetValue<object>(propertyName) == null
                            ? "Null"
                            : entry.CurrentValues.GetValue<object>(propertyName).ToString();
                        currentValue = string.IsNullOrEmpty(currentValue) ? "Null" : currentValue;
                        if (currentValue.Contains("System.Byte[]"))
                        {
                            var currentByteValue = (byte[]) entry.CurrentValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(currentByteValue.LongLength);
                            currentValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        var originalValue = entry.OriginalValues.GetValue<object>(propertyName) == null
                            ? "Null"
                            : entry.OriginalValues.GetValue<object>(propertyName).ToString();
                        originalValue = string.IsNullOrEmpty(originalValue) ? "Null" : originalValue;
                        if (originalValue.Contains("System.Byte[]"))
                        {
                            var originalByteValue = (byte[]) entry.OriginalValues.GetValue<object>(propertyName);
                            var formattedHolder = FileSizeFormatter.ToFormattedString(originalByteValue.LongLength);
                            originalValue = formattedHolder.Number + " " + formattedHolder.Suffix;
                        }
                        displayText = propertyMapper.GetFriendlyTextForProperty(propertyName) ?? propertyName;
                        result.Add(new JournalEntry
                        {
                            JournalEntryCreatedBy = userName,
                            JournalEntryCreatedOn = DateTime.Now,
                            JournalEntryText =
                                $"<html><span style=\"font-size: 9pt;\"><b>Changed:</b> {displayText} -&gt; <i>Old Value: '{originalValue}'</i> <b>New Value: '{currentValue}' </b></span></html>",
                            JournalEntryType = JournalEntryType.System,
                            ProductId = parentProductId
                        });
                        _logger.Info("Changed: {0} -> Old Value: '{1}' New Value: {2}", displayText, originalValue,
                            currentValue);
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            type = GetObjectType(type);
            if (MappingCache.ContainsKey(type)) return MappingCache[type];

            var baseTypeName = type.BaseType.Name;
            var typeName = type.Name;

            var objContext = SelfContext;
            var entitySetBase =
                objContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                    .GetItems<EntityContainer>()
                    .SelectMany(c => c.BaseEntitySets.Where(e => e.Name == typeName || e.Name == baseTypeName))
                    .FirstOrDefault();
            if (entitySetBase == null)
            {
                throw new ArgumentException("Entity type not found in GetEntitySet", typeName);
            }
            MappingCache.Add(type, entitySetBase);
            return entitySetBase;
        }

        internal string GetTableName(Type type)
        {
            var entitySetBase = GetEntitySet(type);
            return $"[{entitySetBase.Schema}].[{entitySetBase.Table}]";
        }

        internal Type GetObjectType(Type type)
        {
            return ObjectContext.GetObjectType(type);
        }
    }
}