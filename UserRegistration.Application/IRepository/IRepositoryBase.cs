using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UserRegistration.Application.IRepository
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void CommitSave();
        
    }
}
