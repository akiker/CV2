using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CodeVault.Models.BaseTypes
{
    /// <summary>
    ///     Implements the generic IRepository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected DbContext DbContext;
        protected DbSet<T> DbSet;

        public Repository(DbContext dataContext)
        {
            DbContext = dataContext;
            DbSet = DbContext.Set<T>();
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        ///     Returns an IEnumerable based on the query, order  clause, and the navigation properties included
        /// </summary>
        /// <param name="query">Linq query for filtering</param>
        /// <param name="orderBy">Linq query for sorting</param>
        /// <param name="includeProperties">Navigation properties separated by comma for eager loading</param>
        /// <returns>IEnumerable containing the resulting entity set</returns>
        public IEnumerable<T> GetByQuery(Expression<Func<T, bool>> query = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> queryResult = DbSet;

            //If there is a query execute it against the DbSet
            if (query != null)
            {
                queryResult = queryResult.Where(query);
            }
            //Get the include request for the navigation properties and add them to the query result
            queryResult =
                includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(queryResult, (current, property) => current.Include(property));

            //If sort request is made, order the query accordingly
            return orderBy?.Invoke(queryResult).ToList() ?? queryResult.ToList();
            //If not, return the result as is
        }

        /// <summary>
        ///     Returns the first matching entity based on the query
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        /// <summary>
        ///     Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Updates an entity by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        public void UpdateById(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        /// <summary>
        ///     Deletes an entity by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        #endregion IRepository<T> Members
    }
}