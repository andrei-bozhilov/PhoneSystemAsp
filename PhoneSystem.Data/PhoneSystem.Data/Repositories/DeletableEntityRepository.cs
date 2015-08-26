namespace PhoneSystem.Data.Repositories
{   
    using System.Data.Entity;
    using System.Linq;

    using PhoneSystem.Contracts;

    public class DeletableEntityRepository<T> : EFRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public IQueryable<T> AllDeleted()
        {
            return base.All().Where(x => x.IsDeleted);
        }
    }
}
