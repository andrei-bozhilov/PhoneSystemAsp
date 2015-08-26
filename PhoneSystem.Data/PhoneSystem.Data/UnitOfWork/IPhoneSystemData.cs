namespace PhoneSystem.Data.UnitOfWork
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using PhoneSystem.Data.Repositories;
    using PhoneSystem.Models;

    public interface IPhoneSystemData
    {
        IDeletableEntityRepository<User> Users { get; }

        IRepository<IdentityRole> UserRoles { get; }

        IDeletableEntityRepository<Department> Departments { get; }

        IPhoneRepository Phones { get; }

        IDeletableEntityRepository<JobTitle> JobTitles { get; }

        IRepository<PhoneNumberOrder> PhoneNumberOrders { get; }

        IRepository<InvoiceData> InvoiceDatas { get; }

        IRepository<InvoiceInfo> InvoiceInfos { get; }

        IRepository<Service> Services { get; }

        int SaveChanges();

        void Dispose();
    }
}
