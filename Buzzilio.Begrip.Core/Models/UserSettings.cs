using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using System;

namespace Buzzilio.Begrip.Core.Models
{
    public class UserSettings : ModelBase<UserSettings>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _UserSettingsId;

        [StaticMember]
        [DataMember]
        public int UserSettingsId
        {
            get { return _UserSettingsId; }
            set { SetProperty(ref _UserSettingsId, value); }
        }

        public bool _ShareTimeIsModified;
        string _ShareTime;

        [DataMember]
        public string ShareTime
        {
            get { return _ShareTime; }
            set { SetPropertyPersist(ref _ShareTime, value, ref _ShareTimeIsModified); }
        }

        public bool _MaxConcurrentMinersIsModified;
        string _MaxConcurrentMiners;

        [DataMember]
        public string MaxConcurrentMiners
        {
            get { return _MaxConcurrentMiners; }
            set { SetPropertyPersist(ref _MaxConcurrentMiners, value, ref _MaxConcurrentMinersIsModified); }
        }

        public bool _DefaultThemeIsModified;
        string _DefaultTheme;

        [DataMember]
        public string DefaultTheme
        {
            get { return _DefaultTheme; }
            set { SetProperty(ref _DefaultTheme, value, ref _DefaultThemeIsModified); }
        }

        public bool _DisclaimerIsEnabledIsModified;
        int _DisclaimerIsEnabled;

        [DataMember]
        public int DisclaimerIsEnabled
        {
            get { return _DisclaimerIsEnabled; }
            set { SetProperty(ref _DisclaimerIsEnabled, value, ref _DisclaimerIsEnabledIsModified); }
        }

        public bool _DisclaimerTextIsModified;
        string _DisclaimerText;

        [StaticMember]
        [DataMember]
        public string DisclaimerText
        {
            get { return _DisclaimerText; }
            set { SetProperty(ref _DisclaimerText, value); }
        }

        int _IsDefault;

        [StaticMember]
        [DataMember]
        public int IsDefault
        {
            get { return _IsDefault; }
            set { SetProperty(ref _IsDefault, value); }
        }

        /// <summary>
        /// Deep clone current object.
        /// Restore copy of instance.
        /// </summary>
        public void CacheObject()
        {
            CacheObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RestoreCachedObject()
        {
            if (Cache == null)
            {
                throw new InvalidOperationException(string.Format("Cache is not initialized for '{0}' class", this.GetType().Name));
            }
            DefaultTheme = Cache.DefaultTheme;
            DisclaimerIsEnabled = Cache.DisclaimerIsEnabled;
            ShareTime = Cache.ShareTime;
            MaxConcurrentMiners = Cache.MaxConcurrentMiners;
            ResetModified();
        }

        #region - C-tor -

        // C-tor.
        public UserSettings()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -       
    }
}
