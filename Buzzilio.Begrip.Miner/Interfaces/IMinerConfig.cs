using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Miner.Interfaces
{
    public interface IConfig
    {
        string Intensity { get; }
        string Username { get; }
        string Password { get; }
        string PoolURL { get; }
        string GPUStats { get; }
        string AlgorithmName { get; }
    }
}
