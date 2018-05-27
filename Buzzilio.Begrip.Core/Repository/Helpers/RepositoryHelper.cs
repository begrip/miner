using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class RepositoryHelper
    {
        /// <summary>
        /// Returns a repository instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static TRepository GetRepositoryInstance<T, TRepository>()
            where TRepository : IEditableRepository<T>, new()
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository));
        }

        /// <summary>
        /// Current DateTime.
        /// </summary>
        public static string CurrentDateTime
        {
            get { return DateTime.Now.ToString(); }
        }

        /// <summary>
        /// Returns a collection from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        public static IList<T> FillCollection<T, TRepository>()
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();            
            
            return respository.GetAll().ToList();
        }

        /// <summary>
        /// Returns a collection from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        public static T GetFirstElement<T, TRepository>()
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            return FillCollection<T, TRepository>().FirstOrDefault();
        }

        /// <summary>
        /// Returns an element from DB by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetElementById<T, TRepository>(int id)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()

        {
            T element;
            var respository = GetRepositoryInstance<T, TRepository>();            
            element = respository.GetById(id);
            
            return element;
        }

        /// <summary>
        /// Returns an element id DB by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetElementByName<T, TRepository>(string name)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()

        {
            T element;
            var respository = GetRepositoryInstance<T, TRepository>();
            element = respository.GetByName(name);
            
            return element;
        }

        /// <summary>
        /// Deletes an element from DB based on id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="id"></param>
        public static void DeleteElement<T, TRepository>(int id)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();        
            var elementToDelete = GetElementById<T, TRepository>(id);
            respository.Delete(elementToDelete);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="ids"></param>
        public static void DeleteElements<T, TRepository>(IList<int> ids)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();
            foreach (var id in ids)
            {
                var elementToDelete = GetElementById<T, TRepository>(id);
                respository.Delete(elementToDelete);
            }
        }

        /// <summary>
        /// Inserts an element into DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="element"></param>
        public static void InsertElement<T, TRepository>(T element)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();  
            respository.Insert(element);          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="element"></param>
        public static void InsertElements<T, TRepository>(IEnumerable<T> elements)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();
            respository.InsertMany(elements);
        }

        /// <summary>
        /// Updates an existing element in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="element"></param>
        public static void UpdateElement<T, TRepository>(T element, List<string> propertiesToUpdate)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();
            respository.Update(element, propertiesToUpdate);
            element.ResetModified();            
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CommitPendingDbChanges()
        {           
           InstanceStore.BgDataContextInstance.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RollbackPendingChanges()
        {
            var hasPendingChanges = HasPendingChanges();
            if (!hasPendingChanges) { return; }

            var context = InstanceStore.BgDataContextInstance;
            var changedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetNextIdFor<T, TRepository>()
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();

            var typeName = typeof(T).Name;
            var tableName = typeName.Pluralize();
            var query = string.Format("SELECT max({0}Id) FROM {1};", typeName, tableName);

            var sequence = respository.ExecuteScalar(query);
            var sequenceNumber = Convert.ToInt32(sequence);

            return ++sequenceNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool ExistsLocally<T>(T entity) 
            where T : class, ICollectible, IModifiable, ICacheable, IDto
        {
            return InstanceStore.BgDataContextInstance.Set<T>().Local.Any(e => e == entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool Exists<T, TRepository>(string name)
            where T : class, ICollectible, IModifiable, ICacheable, IDto
            where TRepository : RepositoryBase<T>, new()
        {
            var respository = GetRepositoryInstance<T, TRepository>();

            return respository.Exists(name); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool HasPendingChanges()
        {
            return InstanceStore.BgDataContextInstance.ChangeTracker.HasChanges();
        }
       
    }
}
