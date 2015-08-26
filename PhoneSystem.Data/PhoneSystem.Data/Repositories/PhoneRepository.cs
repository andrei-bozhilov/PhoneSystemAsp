namespace PhoneSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using PhoneSystem.Models;

    public class PhoneRepository : DeletableEntityRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(DbContext context)
            : base(context)
        {
        }

        public IQueryable<Phone> GetFreePhones()
        {
            var today = DateTime.Now;
            return this.All()
                .Where(x => x.PhoneStatus == PhoneStatus.Free && (x.AvailableAfter.HasValue ? x.AvailableAfter.Value <= today : true));
        }

        public IQueryable<Phone> GetNotFreePhones()
        {
            return this.All()
                .Where(x => x.PhoneStatus == PhoneStatus.Taken);
        }
    }
}
