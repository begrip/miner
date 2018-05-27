using Buzzilio.Begrip.Miner.Interfaces;
using Buzzilio.Begrip.Miner.Cli;
using Buzzilio.Begrip.Miner.Matchers;
using Buzzilio.Begrip.Miner.Reports;
using System;
using System.Diagnostics;
using System.IO;
using Buzzilio.Begrip.Miner.Configuration;
using System.Collections.Generic;

namespace Buzzilio.Begrip.Miner.Helpers
{
    public class CcMinerCli : CliHelper
    {
        public event EventHandler<ReportEventArgs> ReportAvailable;
        CcMinerOutputParser Parser { get; set; }
        public string WorkerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        public CcMinerCli(string workerId)
        {
            WorkerId = workerId;
            Parser = new CcMinerOutputParser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public void Run(IConfig config, bool silent = true)
        {
            var parameters = GetMinerParameters(config);
            Open(CcMinerConfiguration._minerFullPath, parameters);

            if (silent) { return; }
            _outputDataReceived += MinerCli_OutputDataReceived;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetMinerParameters(IConfig config)
        {
            var parameters = new List<string>();

            if (config.GPUStats != "0")
            {
                parameters.Add("-N");
                parameters.Add(config.GPUStats);
            }

            if (config.Intensity != "0")
            {
                parameters.Add("-i");
                parameters.Add(config.Intensity);
            }

            parameters.Add("-a");
            parameters.Add(config.AlgorithmName);

            parameters.Add("-o");
            parameters.Add(config.PoolURL);

            parameters.Add("-u");
            parameters.Add(config.Username);

            parameters.Add("-p");
            parameters.Add(config.Password);

            parameters.Add("--no-color");

            return parameters.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerCli_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            CcMinerReport report = new CcMinerReport();
            var data = e.Data;
            if (Parser.StratumDifficultyOutputParser.IsMatch(data))
            {
                Parser.StratumDifficultyOutputParser.Parse(data);
                report.StratumDifficulty = Parser.StratumDifficultyOutputParser.GetStratumDifficulty();
            }

            if (Parser.BlockDifficultyParser.IsMatch(data))
            {
                Parser.BlockDifficultyParser.Parse(data);
                report.BlockDifficulty = Parser.BlockDifficultyParser.GetBlockDifficulty();
            }

            if (Parser.GpuOutputParser.IsMatch(data))
            {
                Parser.GpuOutputParser.Parse(data);
                report.TotalHashrate = Parser.GpuOutputParser.GetTotalHashrateInKhs();
            }

            if (Parser.ShareOutputParser.IsMatch(data))
            {
                Parser.ShareOutputParser.Parse(data);
                report.AcceptedShares = Parser.ShareOutputParser.GetAcceptedShares();
                report.TotalShares = Parser.ShareOutputParser.GetTotalShares();
                report.StaleShares = Parser.ShareOutputParser.GetStaleShares();
                report.TotalHashrate = Parser.ShareOutputParser.GetTotalHashrateInKhs();
            }
            ReportAvailable(this, new ReportEventArgs { Report = report });
            Parser.ResetParser();
        }
    }
}
