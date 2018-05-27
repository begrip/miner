using System;
using System.IO;

namespace Buzzilio.Begrip.Database.Settings
{
    public static class DatabaseSetupPaths
    {
        public static string executingAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        // Current software version based on assembly info.
        public static string SoftwareVersion = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();

        // Application data
        public static string _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Application specific
        public static string _dbRootPath = Path.Combine(_appDataPath, "Buzzilio", "Begrip");

        // Database connection string related.
        public static string _dbExtension = "sqlite";
        public static string _dbName = "begrip_db";
        public static string _dbFullName = string.Format("{0}.{1}", _dbName, _dbExtension);
        public static string _dbFullPath = Path.GetFullPath(Path.Combine(_dbRootPath, _dbFullName));
        public static string _dbString = string.Format("Data Source={0}", _dbFullPath);

        // Application folder names.
        public static string _scriptsFolderName = "Scripts";
        public static string _dbBackupFolderName = "Backup";
        public static string _dbUpdateScriptsFolderName = "Update";
        public static string _dbCreationScriptsFolderName = "Creation";
        public static string _dbBackupExtension = "backup";

        /// <summary>
        /// 
        /// </summary>
        public static string _scriptsFullPath = Path.Combine
        (
            Path.Combine
            (
                executingAssemblyPath,
                _scriptsFolderName
             )
        );

        public static string _dbBackupFullPath = Path.Combine
        (
            Path.Combine
            (
                _dbRootPath,
                _dbBackupFolderName
             )
        );

        public static string _dbUpdateScriptsPath = Path.GetFullPath
        (
            Path.Combine
            (
                _scriptsFullPath,
                _dbUpdateScriptsFolderName
             )
        );

        public static string _dbCreationScriptsPath = Path.GetFullPath
        (
            Path.Combine
            (
                _scriptsFullPath,
                _dbCreationScriptsFolderName
             )
        );

        public static string _dbSQLiteScriptExtension = "query";
        public static string _dbUpdateScriptBase = "Update_";
        public static string _dbCreationScriptBase = "CreateDatabase";
        public static string _dbPopulationScriptBase = "PopulateDatabase";
        public static string _dbDefaultDataPopulationScriptBase = "PopulateDefaultConfigurations";
        public static int _dbUpdateScriptFirstNumber = 1;
        public static string _dbUpdateScriptBaseFullName = string.Format
        ("{0}.{1}",
            _dbUpdateScriptBase,
            _dbSQLiteScriptExtension
        );
        public static string _dbUpdateScriptBaseFullPath = Path.Combine
        (
            _dbUpdateScriptsPath,
            _dbUpdateScriptBaseFullName
        );
        public static string _dbCreationScriptBaseFullName = string.Format
        ("{0}.{1}",
            _dbCreationScriptBase,
            _dbSQLiteScriptExtension
        );
        public static string _dbCreationScriptBaseFullPath = Path.Combine
        (
            _dbCreationScriptsPath,
            _dbCreationScriptBaseFullName
        );
        public static string _dbPopulationScriptBaseFullName = string.Format
        ("{0}.{1}",
            _dbPopulationScriptBase,
            _dbSQLiteScriptExtension
        );
        public static string _dbDefaultDataPopulationScriptBaseFullName = string.Format
        ("{0}.{1}",
            _dbDefaultDataPopulationScriptBase,
            _dbSQLiteScriptExtension
        );
        public static string _dbPopulationScriptBaseFullPath = Path.Combine
        (
            _dbCreationScriptsPath,
            _dbPopulationScriptBaseFullName
        );

        public static string _dbDefaultDataPopulationScriptBaseFullPath = Path.Combine
        (
            _dbCreationScriptsPath,
            _dbDefaultDataPopulationScriptBaseFullName
        );

        /// <summary>
        /// 
        /// </summary>
        public static string _dateTimeStampDashed = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss");

    }
}
