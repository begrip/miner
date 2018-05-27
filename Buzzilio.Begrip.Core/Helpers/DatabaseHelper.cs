using Buzzilio.Begrip.Core.Repository;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Database.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace Buzzilio.Begrip.Core.Helpers
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Returns database path from database connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetDatabasePathFromConnectionString(string connectionString)
        {
            return connectionString.Replace("Data Source=", string.Empty);
        }

        /// <summary>
        /// Returns database name from database path.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseNameFromDatabasePath(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// Returns database file name from database connection string.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseNameFromDatabaseConnectionString(string connectionString)
        {
            var databasePath = GetDatabasePathFromConnectionString(connectionString);

            return GetDatabaseNameFromDatabasePath(databasePath);
        }

        /// <summary>
        /// Checks whether database file exists based on provided connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool DatabaseFileExists(string connectionString)
        {
            var databaseFile = GetDatabasePathFromConnectionString(connectionString);

            return File.Exists(databaseFile);
        }

        /// <summary>
        /// Creates a database file.
        /// </summary>
        public static void CreateDatabaseFile(string path)
        {
            SQLiteConnection.CreateFile(path);
        }

        /// <summary>
        /// Validates database connection.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateDatabaseConnection(string connectionString)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates database connection string.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateDatabaseConnectionString(string connectionString)
        {
            try
            {
                var sqliteConnectionString = new SQLiteConnectionStringBuilder(connectionString);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// TODO: add logging to each stage
        /// TODO: add dialog norification about resetting to default
        /// </summary>
        public static void InitializeDatabase(string connectionString)
        {
            // WARNING: This case should never happen if config is not corrupt.
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                CreateDefaultDatabase(writeToConfigFile: true);
            }
            // Database connection string is not empty, continue checking.
            else
            {
                // Database connection string is valid.
                var isValidDatabaseConnectionString = ValidateDatabaseConnectionString(connectionString);
                if (isValidDatabaseConnectionString)
                {
                    // Database file does not exist, create database based on 
                    // connection string provided.
                    var dbFileExists = DatabaseFileExists(connectionString);
                    if (!dbFileExists)
                    {
                        var databasePath = GetDatabasePathFromConnectionString(connectionString);
                        CreateDatabase(databasePath, writeToConfigFile: false);
                    }

                    // Database connection string is invalid, create default database.
                }
                else
                {
                    CreateDefaultDatabase(writeToConfigFile: true);
                }
            }

            var isValidated = ValidateDatabaseConnection(connectionString);
            if (!isValidated)
            {
                throw new Exception("Cannot connect to Database!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFilePath"></param>
        private static void CreateDatabase(string dbFilePath, bool writeToConfigFile = true)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbFilePath));
            CreateDatabaseFile(dbFilePath);
            if (writeToConfigFile)
            {
                WriteConfigFile(dbFilePath);
            }
            var dbConnectionString = CreateDatabaseConnectionString(dbFilePath);
            CreateBareboneDatabase(dbConnectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateBareboneDatabase(string connectionString)
        {
            var queries = GetDatabaseCreationQueries();
            foreach (var query in queries)
            {
                ExecuteNonQuery(query, connectionString);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void UpdateDatabaseToSoftwareVersion(string connectionString)
        {
            var repository = RepositoryHelper.GetRepositoryInstance<Models.ApplicationVersion, ApplicationVersionRepository>();
            var updateQueryNumber = repository.GetUpdateQueryNumber(DatabaseSetupPaths.SoftwareVersion);

            // If no queies to execute, return. Database is up to date.
            if (updateQueryNumber == null) { return; }

            // Perform a database backup.
            BackupDatabase(connectionString);

            // If there are queries to update, execute.
            for (int i = DatabaseSetupPaths._dbUpdateScriptFirstNumber; i <= updateQueryNumber; i++)
            {
                var query = File.ReadAllText(DatabaseSetupPaths._dbUpdateScriptBaseFullName);
                repository.ExecuteNonQuery(query);
            }
        }

        /// <summary>
        /// Performs a backup of a valid database.
        /// </summary>
        private static void BackupDatabase(string connectionString)
        {
            var databasePath = GetDatabasePathFromConnectionString(connectionString);
            var databaseBackupFileName = GenerateDatabaseBackupFileName(databasePath);
            var databaseBackupFilePath = Path.Combine(DatabaseSetupPaths._dbBackupFullPath, databaseBackupFileName);

            File.Copy(databasePath, databaseBackupFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databasePath"></param>
        /// <returns></returns>
        private static string GenerateDatabaseBackupFileName(string databasePath)
        {
            var databaseName = GetDatabaseNameFromDatabasePath(databasePath);
            var databaseBackupName = string.Format
                ("{0}_{1}.{2}",
                    databaseName,
                    DatabaseSetupPaths._dateTimeStampDashed,
                    DatabaseSetupPaths._dbBackupExtension
                );

            return databaseBackupName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetUpdateQuery(int updateScriptNumber)
        {
            var query = File.ReadAllText(string.Format("{0}.{2}",
                DatabaseSetupPaths._dbUpdateScriptBaseFullPath, DatabaseSetupPaths._dbSQLiteScriptExtension));

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<string> GetDatabaseCreationQueries()
        {
            var creationSql = File.ReadAllText(DatabaseSetupPaths._dbCreationScriptBaseFullPath);
            var populationSql = File.ReadAllText(DatabaseSetupPaths._dbPopulationScriptBaseFullPath);
            var defaultDataPopulationSql = File.ReadAllText(DatabaseSetupPaths._dbDefaultDataPopulationScriptBaseFullPath);

            return new List<string> { creationSql, populationSql, defaultDataPopulationSql };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateDatabaseConnectionString(string databaseFilePath)
        {
            return string.Format("Data Source={0}", databaseFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFilePath"></param>
        private static void WriteConfigFile(string dbFilePath)
        {
            var databaseConnectionString = CreateDatabaseConnectionString(dbFilePath);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["DbConnectionString"].ConnectionString = databaseConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateDefaultDatabase(bool writeToConfigFile = true)
        {
            CreateDatabase(DatabaseSetupPaths._dbFullPath, writeToConfigFile: writeToConfigFile);
        }

        /// <summary>
        /// Executes a void query.
        /// </summary>
        /// <param name="query"></param>
        public static int ExecuteNonQuery(string query, string connectionString)
        {
            var sequence = -1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                sequence = command.ExecuteNonQuery();
            }
            return sequence;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string query, string connectionString)
        {
            var obj = new object();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                obj = command.ExecuteScalar();
            }
            return obj;
        }
    }
}
