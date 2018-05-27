using Buzzilio.Begrip.Miner.Interfaces;
using System;

namespace Buzzilio.Begrip.Miner.Cli
{
    public class ReportEventArgs : EventArgs
    {
        public IMinerReport Report { get; set; }
    }
}
