using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class Algorithm : ModelBase<Algorithm>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _AlgorithmId;

        [StaticMember]
        [DataMember]
        public int AlgorithmId
        {
            get { return _AlgorithmId; }
            set { SetProperty(ref _AlgorithmId, value); }
        }

        public bool _AlgorithmNameIsModified;
        string _AlgorithmName;

        [DataMember]
        public string AlgorithmName
        {
            get { return _AlgorithmName; }
            set { SetProperty(ref _AlgorithmName, value, ref _AlgorithmNameIsModified); }
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

        public Algorithm()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
