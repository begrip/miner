using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace Buzzilio.Begrip.Core.Models
{
    public class Crypto : ModelBase<Crypto>, ICollectible, IModifiable, ICacheable, IDto
    {   
        int _CryptoId;

        [StaticMember]
        [DataMember]
        public int CryptoId
        {
            get { return _CryptoId; }
            set { SetProperty(ref _CryptoId, value); }
        }

        public bool _CryptoNameIsModified;
        string _CryptoName;

        [DataMember]
        public string CryptoName
        {
            get { return _CryptoName; }
            set { SetProperty(ref _CryptoName, value, ref _CryptoNameIsModified); }
        }

        public bool _CryptoSymbolIsModified;
        string _CryptoSymbol;

        [DataMember]
        public string CryptoSymbol
        {
            get { return _CryptoSymbol; }
            set { SetProperty(ref _CryptoSymbol, value, ref _CryptoSymbolIsModified); }
        }
        
        public bool _CryptoLogoIsModified;
        string _CryptoLogo;

        [DataMember]
        public string CryptoLogo
        {
            get { return _CryptoLogo; }
            set { SetProperty(ref _CryptoLogo, value, ref _CryptoLogoIsModified); }
        }

        public bool _CryptoWebsiteIsModified;
        string _CryptoWebsite;

        [DataMember]
        public string CryptoWebsite
        {
            get { return _CryptoWebsite; }
            set { SetProperty(ref _CryptoWebsite, value, ref _CryptoWebsiteIsModified); }
        }

        ObservableCollection<Algorithm> _AlgorithmCollection;

        [NotMapped]
        [ViewModelMember]
        public ObservableCollection<Algorithm> CryptoAlgorithmCollection
        {
            get { return _AlgorithmCollection; }
            set { SetProperty(ref _AlgorithmCollection, value); }
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

        // C-tor.
        public Crypto()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -       
    }
}
