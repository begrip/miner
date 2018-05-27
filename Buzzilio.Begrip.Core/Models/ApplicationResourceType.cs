using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class ApplicationResourceType : ModelBase<ApplicationResourceType>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _ApplicationResourceTypeId;

        [StaticMember]
        [DataMember]
        public int ApplicationResourceTypeId
        {
            get { return _ApplicationResourceTypeId; }
            set { SetProperty(ref _ApplicationResourceTypeId, value); }
        }

        public bool _ApplicationResourceTypeNameIsModified;
        int _ApplicationResourceTypeName;

        [DataMember]
        public int ApplicationResourceTypeName
        {
            get { return _ApplicationResourceTypeName; }
            set { SetProperty(ref _ApplicationResourceTypeName, value, ref _ApplicationResourceTypeNameIsModified); }
        }

        public bool _ApplicationResourceTypeAbbreviationIsModified;
        string _ApplicationResourceTypeAbbreviation;

        [DataMember]
        public string ApplicationResourceTypeAbbreviation
        {
            get { return _ApplicationResourceTypeAbbreviation; }
            set { SetProperty(ref _ApplicationResourceTypeAbbreviation, value, ref _ApplicationResourceTypeAbbreviationIsModified); }
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

        public ApplicationResourceType()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
