namespace PhoneSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using PhoneSystem.Contracts;
    using PhoneSystem.Data.DbContext;
    using PhoneSystem.Data.Repositories;
    using PhoneSystem.Models;   

    public class PhoneSystemData : IPhoneSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public PhoneSystemData()
            : this(new PhoneSystemDbContext())
        {
        }

        public PhoneSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public PhoneSystemDbContext Context
        {
            get
            {
                return this.context as PhoneSystemDbContext;
            }
        }

        public IDeletableEntityRepository<User> Users
        {
            get
            {
                return this.GetDeletableEntityRepository<User>();
            }
        }

        public IRepository<IdentityRole> UserRoles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public IDeletableEntityRepository<Department> Departments
        {
            get
            {
                return this.GetDeletableEntityRepository<Department>();
            }
        }

        public IPhoneRepository Phones
        {
            get
            {
                return (PhoneRepository)this.GetDeletableEntityRepository<Phone>();
            }
        }

        public IDeletableEntityRepository<JobTitle> JobTitles
        {
            get
            {
                return this.GetDeletableEntityRepository<JobTitle>();
            }
        }

        public IRepository<PhoneNumberOrder> PhoneNumberOrders
        {
            get
            {
                return this.GetRepository<PhoneNumberOrder>();
            }
        }

        public IRepository<InvoiceData> InvoiceDatas
        {
            get
            {
                return this.GetRepository<InvoiceData>();
            }
        }

        public IRepository<InvoiceInfo> InvoiceInfos
        {
            get
            {
                return this.GetRepository<InvoiceInfo>();
            }
        }

        public IRepository<Service> Services
        {
            get
            {
                return this.GetRepository<Service>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);

            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository =
                    Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);

                if (typeof(T).IsAssignableFrom(typeof(Phone)))
                {
                    type = typeof(PhoneRepository);
                }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
