using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class ApplicationResource : ModelBase<ApplicationResource>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _ApplicationResourceId;

        [StaticMember]
        [DataMember]
        public int ApplicationResourceId
        {
            get { return _ApplicationResourceId; }
            set { SetProperty(ref _ApplicationResourceId, value); }
        }

        public bool _ApplicationResourceTypeIdIsModified;
        int _ApplicationResourceTypeId;

        [DataMember]
        public int ApplicationResourceTypeId
        {
            get { return _ApplicationResourceTypeId; }
            set { SetProperty(ref _ApplicationResourceTypeId, value, ref _ApplicationResourceTypeIdIsModified); }
        }

        public bool _ApplicationResourceValueIsModified;
        string _ApplicationResourceValue;

        [DataMember]
        public string ApplicationResourceValue
        {
            get { return _ApplicationResourceValue; }
            set { SetProperty(ref _ApplicationResourceValue, value, ref _ApplicationResourceValueIsModified); }
        }

        /// <summary>
        /// Deep clone current object.
        /// Restore copy of instance.
        /// </summary>
        public void CacheObject()
        {
            base.CacheObject(this);
        }

        public void RestoreCachedObject()
        {
            throw new System.NotImplementedException();
        }

        #region - C-tor -

        public ApplicationResource()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
