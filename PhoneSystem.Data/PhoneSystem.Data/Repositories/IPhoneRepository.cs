namespace PhoneSystem.Data.Repositories
{
    using System.Linq;

    using PhoneSystem.Models;

    public interface IPhoneRepository : IDeletableEntityRepository<Phone>
    {
        IQueryable<Phone> GetFreePhones();

        IQueryable<Phone> GetNotFreePhones();
    }
}
