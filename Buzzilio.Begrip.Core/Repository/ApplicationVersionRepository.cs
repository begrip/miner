using System.Linq;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;

namespace Buzzilio.Begrip.Core.Repository
{
    public class ApplicationVersionRepository : RepositoryBase<ApplicationVersion>, IApplicationVersionRepository
    {
        public ApplicationVersionRepository() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="softwareVersion"></param>
        /// <returns></returns>
        public int? GetUpdateQueryNumber(string softwareVersion)
        {
            var queries = GetAll();
            if (queries == null) { return null; }

            var updateQueryNumber = queries.FirstOrDefault(c => c.SoftwareVersion == softwareVersion).UpdateScript;
            if (updateQueryNumber == null || updateQueryNumber == 0) { return null; }
            else { return updateQueryNumber; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="softwareVersion"></param>
        /// <returns></returns>
        public ApplicationVersion GetLatestApplicationVersion()
        {
            var queries = GetAll();
            if (queries == null) { return null; }

            var latest = queries.OrderByDescending(item => item.ApplicationVersionId).First();

            return latest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetLatestSoftwareVersion()
        {
            var latestAppVersion = GetLatestApplicationVersion();

            return latestAppVersion.SoftwareVersion;
        }
    }
}
