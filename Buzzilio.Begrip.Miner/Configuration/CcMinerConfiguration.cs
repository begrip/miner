using System.IO;

namespace Buzzilio.Begrip.Miner.Configuration
{
    public class CcMinerConfiguration
    {
        static string _minerRootDir = "CCMiner";
        static string _minerExecutableName = "ccminer-x64.exe";
        static string _executingAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static string _minerFullPath = Path.Combine(_executingAssemblyPath, _minerRootDir, _minerExecutableName);
    }
}
