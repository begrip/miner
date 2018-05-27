using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Core.Models.Mappings;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Buzzilio.Begrip.Core.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IEditableRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// DB Context.
        /// </summary>
        private BgDataContext DbContext { get; set; }

        /// <summary>
        /// C-tor.
        /// </summary>
        public RepositoryBase()
        {
            DbContext = InstanceStore.BgDataContextInstance;
        }
        
        /// <summary>
        /// Insert TEntity.
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void InsertMany(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Delete TEntity.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            //DbContext.Set<TEntity>().Remove(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities) { DbContext.Entry(entity).State = EntityState.Deleted; }
        }

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertiesToUpdate"></param>
        public void Update(TEntity obj, List<string> propertiesToUpdate)
        {
            DbContext.Set<TEntity>().Attach(obj);

            foreach (var p in propertiesToUpdate)
            {
                DbContext.Entry(obj).Property(p).IsModified = true;
            }
        }

        /// <summary>
        /// Search for all TEntities.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> SearchForAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Get all TEntities.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Get TEntity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get TEntity by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TEntity GetByName(string name) { throw new NotImplementedException("'GetByName' has to be implemented in the non-base class in order to be used."); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool Exists(string name) { throw new NotImplementedException("'Exists' has to be implemented in the non-base class in order to be used."); }

        /// <summary>
        /// Executes a void query on db.
        /// </summary>
        public int ExecuteNonQuery(string query)
        {
            return DbContext.ExecuteNonQuery(query, InstanceStore.DbConnectionString);
        }

        /// <summary>
        /// Executes a scalar query on db.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query)
        {
            return DbContext.ExecuteScalar(query, InstanceStore.DbConnectionString);
        }

        /// <summary>
        /// Save changes to database.
        /// </summary>
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
