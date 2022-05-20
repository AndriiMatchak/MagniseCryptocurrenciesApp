using MagniseCryptocurrenciesApp.DataAccess;
using MagniseCryptocurrenciesApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MagniseCryptocurrenciesApp.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T>
       where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public void AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
            SaveChanges();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);

            SaveChanges();
        }

        public void RemoveRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);

            SaveChanges();
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).ToList();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Get(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.FirstOrDefault(whereCondition);
        }

        public bool Any(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Any(whereCondition);
        }

        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Count(whereCondition);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            SaveChanges();
        }

        public void UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
                _dbContext.Entry(entity).State = EntityState.Modified;

            SaveChanges();
        }

        protected void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    entry.Reload();
                }

                _dbContext.SaveChanges();
            }
        }
    }
}
