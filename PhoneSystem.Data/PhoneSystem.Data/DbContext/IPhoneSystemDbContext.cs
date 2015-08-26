namespace PhoneSystem.Data.DbContext
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using PhoneSystem.Models;

    public interface IPhoneSystemDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<Phone> Phones { get; set; }

        IDbSet<PhoneNumberOrder> PhoneNumberOrders { get; set; }

        IDbSet<JobTitle> JobTitles { get; set; }

        IDbSet<InvoiceData> InvoiceDatas { get; set; }

        IDbSet<InvoiceInfo> InvoiceInfos { get; set; }

        IDbSet<Service> Services { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
