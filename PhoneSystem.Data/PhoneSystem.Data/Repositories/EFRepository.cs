namespace PhoneSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EFRepository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> set;

        public EFRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.context = context;
            this.set = context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.set;
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this.set.Where(expression);
        }

        public virtual T GetById(object id)
        {
            return this.set.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public virtual void AddRange(IQueryable<T> entities)
        {
            this.set.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public virtual T Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
            return entity;
        }

        public virtual T Delete(object id)
        {
            var entity = this.GetById(id);
            entity = this.Delete(entity);
            return entity;
        }

        public T UnDelete(object id)
        {
            var entity = this.GetById(id);
            this.ChangeState(entity, EntityState.Modified);
            return entity;
        }

        public virtual void DeleteRange(IQueryable<T> entities)
        {
            this.set.RemoveRange(entities);
        }

        public void Detach(T entity)
        {
            this.ChangeState(entity, EntityState.Detached);
        }

        public virtual int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
