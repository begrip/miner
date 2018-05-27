using Buzzilio.Begrip.Miner.Interfaces;
using Buzzilio.Begrip.Miner.Parsers;
using System.Text.RegularExpressions;

namespace Buzzilio.Begrip.Miner.Matchers
{
    public class CcMinerOutputParser : IMinerMatcher
    {
        public BlockDifficultyParser BlockDifficultyParser { get; set; }
        public StratumDifficultyParser StratumDifficultyOutputParser { get; set; }
        public GpuOutputParser GpuOutputParser { get; set; }
        public ShareOutputParser ShareOutputParser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CcMinerOutputParser()
        {
            BlockDifficultyParser = new BlockDifficultyParser();
            StratumDifficultyOutputParser = new StratumDifficultyParser();
            GpuOutputParser = new GpuOutputParser();
            ShareOutputParser = new ShareOutputParser();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetParser()
        {
            BlockDifficultyParser.ResetParser();
            StratumDifficultyOutputParser.ResetParser();
            GpuOutputParser.ResetParser();
            ShareOutputParser.ResetParser();
        }

        //public Regex StratumDifficultyMatch { get { return new Regex(@"Stratum difficulty set to (.+?.*)"); } }
        //public Regex BlockDifficultyMatch { get { return new Regex(@"block (.+?), diff (.+?)$"); } }
        //public Regex GpuMatch { get { return new Regex(@"GPU #[0-9]:.+?,(.+?) (kH/s|MH/s)"); } }
        //public Regex SharesMatch { get { return new Regex(@"accepted: (.+?)/(.+?) \(diff.+?, (.+?) (kH/s|MH/s)"); } }
    }
}
