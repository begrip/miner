using Buzzilio.Begrip.Resources.Paths;
using System;
using System.IO;

namespace Buzzilio.Begrip.Core.Settings
{
    public static class RuntimeSettings
    {
        // Base paths.
        private static string _imagesDirectory = "Images";
        public static string _localBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string _dbConnectionStringConfigPartName = "DbConnectionString";
        public static string _logDirectory = @"Buzzilio\Begrip Miner";

        public static string _cryptoLogosDirectory = @"CryptoLogos\64";
        public static string _iconImagesDirectory = @"Icons\48";

        public static string _cryptoLogosPath = Path.Combine
        (
            _localBaseDirectory,
            _imagesDirectory,
            _cryptoLogosDirectory
        );

        public static string _iconImagesPath = Path.Combine
        (
            _localBaseDirectory,
            _imagesDirectory,
            _iconImagesDirectory
        );
    }
}
