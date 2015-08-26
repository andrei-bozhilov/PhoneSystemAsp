namespace PhoneSystem.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        T GetById(object id);

        void Add(T entity);

        void AddRange(IQueryable<T> entities);

        void Update(T entity);

        T Delete(T entity);

        T Delete(object id);

        T UnDelete(object id);

        void DeleteRange(IQueryable<T> entities);

        void Detach(T entity);

        int SaveChanges();
    }
}
