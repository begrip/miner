using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository() { }

        /// <summary>
        /// Get non-default usersettings
        /// </summary>
        /// <returns></returns>
        public UserSettings GetUserSettings()
        {
            return GetAll()
                    .Where(c => c.IsDefault == 0)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Get default usersettings
        /// </summary>
        /// <returns></returns>
        public UserSettings GetDefaultUserSettings()
        {
            return GetAll()
                .Where(c => c.IsDefault == 1)
                .FirstOrDefault();
        }

    }
}
