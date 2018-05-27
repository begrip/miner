using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Buzzilio.Begrip.Miner.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Buzzilio.Begrip.Core.Models
{
    public class Configuration : ModelBase<Configuration>, ICollectible, IModifiable, ICacheable, IDto, IConfig
    {
        int _ConfigurationId;

        [StaticMember]
        [DataMember]
        public int ConfigurationId
        {
            get { return _ConfigurationId; }
            set { SetProperty(ref _ConfigurationId, value); }
        }

        public bool _CryptoIdIsModified;
        int _CryptoId;

        [DataMember]
        public int CryptoId
        {
            get { return _CryptoId; }
            set { SetProperty(ref _CryptoId, value, ref _CryptoIdIsModified); }
        }

        public bool _AlgorithmIdIsModified;
        int _AlgorithmId;

        [DataMember]
        public int AlgorithmId
        {
            get { return _AlgorithmId; }
            set { SetProperty(ref _AlgorithmId, value, ref _AlgorithmIdIsModified); }
        }

        public bool _IntensityIsModified;
        string _Intensity;

        [DataMember]
        public string Intensity
        {
            get { return _Intensity; }
            set { SetPropertyPersist(ref _Intensity, value, ref _IntensityIsModified); }
        }

        public bool _GPUStatsIsModified;
        string _GPUStats;

        [DataMember]
        public string GPUStats
        {
            get { return _GPUStats; }
            set { SetPropertyPersist(ref _GPUStats, value, ref _GPUStatsIsModified); }
        }

        public bool _UsernameIsModified;
        string _Username;

        [DataMember]
        [Required]
        public string Username
        {
            get { return _Username; }
            set { SetProperty(ref _Username, value, ref _UsernameIsModified); }
        }

        public bool _PasswordIsModified;
        string _Password;

        [DataMember]
        [Required]
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value, ref _PasswordIsModified); }
        }

        public bool _WalletIsModified;
        string _Wallet;

        [DataMember]
        public string Wallet
        {
            get { return _Wallet; }
            set { SetProperty(ref _Wallet, value, ref _WalletIsModified); }
        }

        public bool _PoolURLIsModified;
        string _PoolURL;

        [DataMember]
        [Required]
        public string PoolURL
        {
            get { return _PoolURL; }
            set { SetProperty(ref _PoolURL, value, ref _PoolURLIsModified); }
        }

        public bool _SupportsParamsIsModified;
        int _SupportsParams;

        [DataMember]
        public int SupportsParams
        {
            get { return _SupportsParams; }
            set { SetProperty(ref _SupportsParams, value, ref _SupportsParamsIsModified); }
        }

        public bool _IsFavouriteIsModified;
        int _IsFavourite;

        [DataMember]
        public int IsFavourite
        {
            get { return _IsFavourite; }
            set { SetProperty(ref _IsFavourite, value, ref _IsFavouriteIsModified); }
        }

        int _IsDefault;

        [StaticMember]
        [DataMember]
        public int IsDefault
        {
            get { return _IsDefault; }
            set { SetProperty(ref _IsDefault, value); }
        }

        Crypto _Crypto;

        [NotMapped]
        [ViewModelMember]
        public Crypto Crypto
        {
            get { return _Crypto; }
            set { SetProperty(ref _Crypto, value); }
        }

        string _Algorithm;

        [NotMapped]
        public string AlgorithmName
        {
            get { return _Algorithm; }
            set { SetProperty(ref _Algorithm, value); }
        }

        /// <summary>
        /// Deep clone current object.
        /// Restore copy of instance.
        /// </summary>
        public void CacheObject()
        {
            base.CacheObject(this);
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
            AlgorithmId = Cache.AlgorithmId;
            Intensity = Cache.Intensity;
            GPUStats = Cache.GPUStats;
            Username = Cache.Username;
            Password = Cache.Password;
            Wallet = Cache.Wallet;
            PoolURL = Cache.PoolURL;
            IsFavourite = Cache.IsFavourite;
            ResetModified();
        }

        #region - C-tor -

        //C-tor.
        public Configuration()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
