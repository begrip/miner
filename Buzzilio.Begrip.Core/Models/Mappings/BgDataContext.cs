using Buzzilio.Begrip.Database.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using Entity = System.Data.Entity.Database;

namespace Buzzilio.Begrip.Core.Models.Mappings
{
    [DbConfigurationType(typeof(DatabaseConfiguration))]
    public class BgDataContext : DbContext
    {
        public static SQLiteConnection _sqlConnection;
        private static string _connectionString;

        public BgDataContext(string connectionString)
            : base(_sqlConnection = new SQLiteConnection(_connectionString = connectionString), true)
        {
            _sqlConnection.Open();
            Entity.SetInitializer<BgDataContext>(null);
        }

        /// <summary>
        /// Executes a void query.
        /// </summary>
        /// <param name="query"></param>
        public int ExecuteNonQuery(string query, string connectionString)
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
        public object ExecuteScalar(string query, string connectionString)
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

        /// <summary>
        /// Map model explicitely.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crypto>().ToTable("Cryptos");
            modelBuilder.Entity<Library>().ToTable("Libraries");
            modelBuilder.Entity<Algorithm>().ToTable("Algorithms");
            modelBuilder.Entity<UserSettings>().ToTable("UserSettings");
            modelBuilder.Entity<Configuration>().ToTable("Configurations");
            modelBuilder.Entity<AssignedAlgorithm>().ToTable("AssignedAlgorithms");
            modelBuilder.Entity<ApplicationVersion>().ToTable("ApplicationVersions");
            modelBuilder.Entity<ApplicationResource>().ToTable("ApplicationResources");
            modelBuilder.Entity<ApplicationResourceType>().ToTable("ApplicationResourceTypes");


        }

        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<AssignedAlgorithm> AssignedAlgorithms { get; set; }
        public DbSet<ApplicationVersion> ApplicationVersions { get; set; }
        public DbSet<ApplicationResource> ApplicationResources { get; set; }
        public DbSet<ApplicationResourceType> ApplicationResourceTypes { get; set; }
    }
}
