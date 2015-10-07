using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CodeVault.Models.BaseTypes
{
    /// <summary>
    /// Implements the generic IRepository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected DbSet<T> dbSet;

        protected DbContext dbContext;

        public Repository(DbContext dataContext)
        {
            this.dbContext = dataContext;
            dbSet = this.dbContext.Set<T>();
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            this.dbSet.Add(entity);
        }

        public T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>
        /// Returns an IEnumerable based on the query, order  clause, and the navigation properties included
        /// </summary>
        /// <param name="query">Linq query for filtering</param>
        /// <param name="orderBy">Linq query for sorting</param>
        /// <param name="includeProperties">Navigation properties separated by comma for eager loading</param>
        /// <returns>IEnumerable containing the resulting entity set</returns>
        public IEnumerable<T> GetByQuery(Expression<Func<T, bool>> query = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> queryResult = this.dbSet;

            //If there is a query execute it against the DbSet
            if (query != null)
            {
                queryResult = queryResult.Where(query);
            }
            //Get the include request for the navigation properties and add them to the query result
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryResult = queryResult.Include(property);
            }

            //If sort request is made, order the query accordingly
            if (orderBy != null)
            {
                return orderBy(queryResult).ToList();
            }
            //If not, return the result as is
            else
            {
                return queryResult.ToList();
            }
        }

        /// <summary>
        /// Returns the first matching entity based on the query
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.First(predicate);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates an entity by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        public void UpdateById(int id)
        {
            T entity = this.dbSet.Find(id);
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
                this.dbSet.Attach(entity);
            this.dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes an entity by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        public void Delete(int id)
        {
            T entity = this.dbSet.Find(id);
            this.dbSet.Remove(entity);
        }

        #endregion IRepository<T> Members
    }
}