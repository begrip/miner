using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Buzzilio.Begrip.Core.Repository.Interfaces
{
    public interface IEditableRepository<T> : IRepository
    {
        void Insert(T entity);
        void InsertMany(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteMany(IEnumerable<T> entities);
        IQueryable<T> SearchForAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetByName(string name);
        void SaveChanges();
    }
}
