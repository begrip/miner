using Buzzilio.Begrip.Core.Models.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Miner.Interfaces;
using static Buzzilio.Begrip.Miner.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.Models.Local
{
    public class MinerOutput : ModelBase<MinerOutput>, ICollectible, IModifiable, ICacheable
    {
        public void Update(IMinerReport report)
        {
            MinerStatus = report.MinerStatus == 0 ? MinerStatus : report.MinerStatus;
            Block = report.Block == 0 ? Block : report.Block;
            TotalHashrate = report.TotalHashrate == 0 ? TotalHashrate : report.TotalHashrate;
            StratumDifficulty = report.StratumDifficulty == 0 ? StratumDifficulty : report.StratumDifficulty;
            BlockDifficulty = report.BlockDifficulty == 0 ? BlockDifficulty : report.BlockDifficulty;
            AcceptedShares = report.AcceptedShares == 0 ? AcceptedShares : report.AcceptedShares;
            TotalShares = report.TotalShares == 0 ? TotalShares : report.TotalShares;
            StaleShares = report.StaleShares == 0 ? StaleShares : report.StaleShares;
        }

        MinerStatus _MinerStatus;
        public MinerStatus MinerStatus
        {
            get { return _MinerStatus; }
            set { SetProperty(ref _MinerStatus, value); }
        }

        int _Block;
        public int Block
        {
            get { return _Block; }
            set { SetProperty(ref _Block, value); }
        }

        decimal _TotalHashrate;
        public decimal TotalHashrate
        {
            get { return _TotalHashrate; }
            set { SetProperty(ref _TotalHashrate, value); }
        }

        decimal _StratumDifficulty;
        public decimal StratumDifficulty
        {
            get { return _StratumDifficulty; }
            set { SetProperty(ref _StratumDifficulty, value); }
        }

        decimal _BlockDifficulty;
        public decimal BlockDifficulty
        {
            get { return _BlockDifficulty; }
            set { SetProperty(ref _BlockDifficulty, value); }
        }

        int _TotalShares;
        public int TotalShares
        {
            get { return _TotalShares; }
            set { SetProperty(ref _TotalShares, value); }
        }

        int _AcceptedShares;
        public int AcceptedShares
        {
            get { return _AcceptedShares; }
            set { SetProperty(ref _AcceptedShares, value); }
        }

        int _StaleShares;
        public int StaleShares
        {
            get { return _StaleShares; }
            set { SetProperty(ref _StaleShares, value); }
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

        public MinerOutput()
        {
            SetInstance(this);
            BuildPropertySet();
        }

        #endregion - C-tor -  
    }
}

