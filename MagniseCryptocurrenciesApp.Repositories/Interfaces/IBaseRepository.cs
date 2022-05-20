using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MagniseCryptocurrenciesApp.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    where T : class
    {
        void Add(T entity);

        void AddRange(List<T> entities);

        void Remove(T entity);

        void RemoveRange(List<T> entities);

        List<T> GetAll(Expression<Func<T, bool>> whereCondition);

        List<T> GetAll();

        T Get(Expression<Func<T, bool>> whereCondition);

        bool Any(Expression<Func<T, bool>> whereCondition);

        int Count(Expression<Func<T, bool>> whereCondition);

        void Update(T entity);

        void UpdateRange(List<T> entities);
    }
}
