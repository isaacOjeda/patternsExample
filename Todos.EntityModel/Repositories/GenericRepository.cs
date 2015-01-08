using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Todos.EntityModel.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        internal ToDoListEntities context;
        /// <summary>
        /// 
        /// </summary>
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(ToDoListEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> ReadAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}