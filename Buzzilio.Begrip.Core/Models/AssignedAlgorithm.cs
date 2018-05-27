using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;

namespace Buzzilio.Begrip.Core.Models
{
    public class AssignedAlgorithm : ModelBase<AssignedAlgorithm>, ICollectible, IModifiable, ICacheable, IDto
    {
        int _AssignedAlgorithmId;

        [StaticMember]
        [DataMember]
        public int AssignedAlgorithmId
        {
            get { return _AssignedAlgorithmId; }
            set { SetProperty(ref _AssignedAlgorithmId, value); }
        }

        public bool _AlgorithmIdIsModified;
        int _AlgorithmId;

        [DataMember]
        public int AlgorithmId
        {
            get { return _AlgorithmId; }
            set { SetProperty(ref _AlgorithmId, value, ref _AlgorithmIdIsModified); }
        }

        public bool _CryptoIdIsModified;
        int _CryptoId;

        [DataMember]
        public int CryptoId
        {
            get { return _CryptoId; }
            set { SetProperty(ref _CryptoId, value, ref _CryptoIdIsModified); }
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
            throw new System.NotImplementedException();
        }

        #region - C-tor -

        public AssignedAlgorithm()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}
