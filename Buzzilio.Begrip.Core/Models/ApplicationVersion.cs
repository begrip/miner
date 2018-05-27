using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class ApplicationVersion : ModelBase<ApplicationVersion>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _ApplicationVersionId;

        [StaticMember]
        [DataMember]
        public int ApplicationVersionId
        {
            get { return _ApplicationVersionId; }
            set { SetProperty(ref _ApplicationVersionId, value); }
        }

        public bool _SoftwareVersionIsModified;
        string _SoftwareVersion;

        [DataMember]
        public string SoftwareVersion
        {
            get { return _SoftwareVersion; }
            set { SetProperty(ref _SoftwareVersion, value, ref _SoftwareVersionIsModified); }
        }

        public bool _ReleaseNameIsModified;
        string _ReleaseName;

        [DataMember]
        public string ReleaseName
        {
            get { return _ReleaseName; }
            set { SetProperty(ref _ReleaseName, value, ref _ReleaseNameIsModified); }
        }

        public bool _DatabaseVersionIsModified;
        string _DatabaseVersion;

        [DataMember]
        public string DatabaseVersion
        {
            get { return _DatabaseVersion; }
            set { SetProperty(ref _DatabaseVersion, value, ref _DatabaseVersionIsModified); }
        }

        public bool _UpdateScriptIsModified;
        int? _UpdateScript;

        [DataMember]
        public int? UpdateScript
        {
            get { return _UpdateScript; }
            set { SetProperty(ref _UpdateScript, value, ref _UpdateScriptIsModified); }
        }

        /// <summary>
        /// Deep clone current object.
        /// Restore copy of instance.
        /// </summary>
        public void CacheObject()
        {
            CacheObject(this);
        }

        public void RestoreCachedObject()
        {
            throw new System.NotImplementedException();
        }

        #region - C-tor -

        public ApplicationVersion()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
