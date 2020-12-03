using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserRegistration.Application.IRepository;
using UserRegistration.Data;

namespace UserRegistration.Application.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected UserContext repoContext { get; set; }
        public RepositoryBase(UserContext uContext)
        {
            repoContext = uContext;
        }
        public void Create(T entity)
        {
            this.repoContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.repoContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return this.repoContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.repoContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            this.repoContext.Set<T>().Update(entity);
        }

        public void CommitSave()
        {
            this.repoContext.SaveChanges();
        }
    }
}
