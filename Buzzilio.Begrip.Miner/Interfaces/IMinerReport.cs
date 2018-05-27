using static Buzzilio.Begrip.Miner.Enumerations.Enums;

namespace Buzzilio.Begrip.Miner.Interfaces
{
    public interface IMinerReport
    {
        MinerStatus MinerStatus { get; set; }
        int Block { get; set; }
        decimal TotalHashrate { get; set; }
        decimal StratumDifficulty { get; set; }
        decimal BlockDifficulty { get; set; }
        int TotalShares { get; set; }
        int AcceptedShares { get; set; }
        int StaleShares { get; set; }
    }
}
