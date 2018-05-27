using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Buzzilio.Begrip.Utilities.Extensions;
using Buzzilio.Begrip.Infrastructure.Mvvm.Helpers;
using PropertyHelperUtil = Buzzilio.Begrip.Utilities.Helpers.PropertyHelper;
using System;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Base
{
    public class ModelBase<T> : PropertyChangedHelper where T : class
    {
        [JsonIgnore]
        [NotMapped]
        protected T Instance { get; set; }

        [JsonIgnore]
        [NotMapped]
        public T Cache { get; set; }

        [JsonIgnore]
        [NotMapped]
        public new bool HasChanges
        {
            get { return base.HasChanges; }
            set { base.HasChanges = value; }
        }

        [JsonIgnore]
        [NotMapped]
        protected List<PropertyInfo> PropertySet { get; set; }

        [JsonIgnore]
        [NotMapped]
        protected List<PropertyInfo> ChangeSet { get; set; }

        #region - C-tor -

        /// <summary>
        /// Builds a changeset collection.
        /// </summary>
        public void BuildPropertySet()
        {
            PropertySet = PropertyHelper.GetDataMemberClassProperties<T>();
            ChangeSet = new List<PropertyInfo>();
            ResetModified();
        }

        #endregion - Ctor -      

        /// <summary>
        /// 
        /// </summary>
        public void ResetModified()
        {
            HasChanges = false;
            foreach (var property in PropertySet)
            {
                PropertyHelperUtil.SetField(Instance, $"_{property.Name}IsModified", false);
            }
        }

        #region - ICacheable -

        /// <summary>
        /// Load cloned object.
        /// </summary>
        public void ResetCachedObject()
        {
            // Sets to null.
            Cache = default(T);
            ResetModified();
        }

        /// <summary>
        /// Caches the entire object.
        /// </summary>
        protected void CacheObject(T obj)
        {
            Cache = obj.CloneJson<T>();
            Instance = obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        protected void SetInstance(T instance)
        {
            Instance = instance;
        }

        /// <summary>
        /// Gets list of changed properties.
        /// </summary>
        public List<string> GetChanges()
        {
            ChangeSet.Clear();
            foreach (var property in PropertySet)
            {
                if (PropertyHelperUtil.GetField<bool, T>(Instance, $"_{property.Name}IsModified") == true)
                {
                    ChangeSet.Add(property);
                }
            }
            return ChangeSet.Select(x => x.Name).ToList();
        }
        #endregion - ICacheable -

        #region - C-tor -
        public ModelBase()
        {

        }

        #endregion C-tor -
    }
}
