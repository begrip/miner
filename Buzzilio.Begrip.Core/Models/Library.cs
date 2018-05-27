using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class Library : ModelBase<Library>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _LibraryId;

        [StaticMember]
        [DataMember]
        public int LibraryId
        {
            get { return _LibraryId; }
            set { SetProperty(ref _LibraryId, value); }
        }

        public bool _LibraryNameIsModified;
        string _LibraryName;

        [DataMember]
        public string LibraryName
        {
            get { return _LibraryName; }
            set { SetProperty(ref _LibraryName, value, ref _LibraryNameIsModified); }
        }

        public bool _LibraryAuthorIsModified;
        string _LibraryAuthor;

        [DataMember]
        public string LibraryAuthor
        {
            get { return _LibraryAuthor; }
            set { SetProperty(ref _LibraryAuthor, value, ref _LibraryAuthorIsModified); }
        }

        public bool _LibraryLicenseIsModified;
        string _LibraryLicense;

        [DataMember]
        public string LibraryLicense
        {
            get { return _LibraryLicense; }
            set { SetProperty(ref _LibraryLicense, value, ref _LibraryLicenseIsModified); }
        }

        public bool _LibrarySourceLinkIsModified;
        string _LibrarySourceLink;

        [DataMember]
        public string LibrarySourceLink
        {
            get { return _LibrarySourceLink; }
            set { SetProperty(ref _LibrarySourceLink, value, ref _LibrarySourceLinkIsModified); }
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

        public Library()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
