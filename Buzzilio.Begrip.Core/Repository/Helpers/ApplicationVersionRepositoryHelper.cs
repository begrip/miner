using Buzzilio.Begrip.Core.Models;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class ApplicationVersionRepositoryHelper : RepositoryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLatestSoftwareVersion()
        {
            var repo = GetRepositoryInstance<ApplicationVersion, ApplicationVersionRepository>();

            return repo.GetLatestSoftwareVersion();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ApplicationVersion GetLatestApplicationVersion()
        {
            var repo = GetRepositoryInstance<ApplicationVersion, ApplicationVersionRepository>();

            return repo.GetLatestApplicationVersion();
        }
    }
}
