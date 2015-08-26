namespace PhoneSystem.Data.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using PhoneSystem.Contracts;
    using PhoneSystem.Data.Migrations;
    using PhoneSystem.Models;

    public class PhoneSystemDbContext : IdentityDbContext<User>, IPhoneSystemDbContext
    {
        public PhoneSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhoneSystemDbContext, Configuration>());
        }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<Phone> Phones { get; set; }

        public virtual IDbSet<PhoneNumberOrder> PhoneNumberOrders { get; set; }

        public virtual IDbSet<JobTitle> JobTitles { get; set; }

        public virtual IDbSet<InvoiceData> InvoiceDatas { get; set; }

        public virtual IDbSet<InvoiceInfo> InvoiceInfos { get; set; }

        public virtual IDbSet<Service> Services { get; set; }

        public static PhoneSystemDbContext Create()
        {
            return new PhoneSystemDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            int result = base.SaveChanges();
            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhoneNumberOrder>()
                    .HasRequired(m => m.Admin)
                    .WithMany(t => t.UserPhoneNumberOrders)
                    .HasForeignKey(m => m.AdminId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumberOrder>()
                        .HasRequired(m => m.User)
                        .WithMany(t => t.AdminPhoneNumberOrders)
                        .HasForeignKey(m => m.UserId)
                        .WillCascadeOnDelete(false);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Modified)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                if (entity.IsDeleted)
                {
                    entity.IsDeleted = false;
                }
            }

            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}