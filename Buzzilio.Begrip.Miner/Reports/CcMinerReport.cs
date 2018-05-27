using Buzzilio.Begrip.Miner.Interfaces;
using static Buzzilio.Begrip.Miner.Enumerations.Enums;

namespace Buzzilio.Begrip.Miner.Reports
{
    public class CcMinerReport : IMinerReport
    {
        public MinerStatus MinerStatus { get; set; }
        public int Block { get; set; }
        public decimal TotalHashrate { get; set; }
        public decimal StratumDifficulty { get; set; }
        public decimal BlockDifficulty { get; set; }
        public int TotalShares { get; set; }
        public int AcceptedShares { get; set; }
        public int StaleShares { get; set; }
    }
}
