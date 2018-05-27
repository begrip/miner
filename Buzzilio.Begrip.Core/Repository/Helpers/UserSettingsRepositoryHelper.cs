using Buzzilio.Begrip.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class UserSettingsRepositoryHelper : RepositoryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static UserSettings GetUserSettings()
        {
            var userSettings = GetRepositoryInstance<UserSettings, UserSettingsRepository>().GetUserSettings();
            return userSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static UserSettings GetDefaultUserSettings()
        {
            var defaultUserSettings = GetRepositoryInstance<UserSettings, UserSettingsRepository>().GetDefaultUserSettings();
            return defaultUserSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="properties"></param>
        public static void UpdateUserSettings(UserSettings config, List<string> properties)
        {
            UpdateElement<UserSettings, UserSettingsRepository>(config, properties);
        }
    }
}
