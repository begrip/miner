using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class ConfigurationRepository : RepositoryBase<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cryptoId"></param>
        /// <returns></returns>
        public Configuration GetConfigurationByCryptoId(int cryptoId)
        {
            return GetAll()
                    .Where(c => c.CryptoId == cryptoId && c.IsDefault == 0)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cryptoId"></param>
        /// <returns></returns>
        public Configuration GetDefaultConfigurationByCryptoId(int cryptoId)
        {
            return GetAll()
                    .Where(c => c.CryptoId == cryptoId && c.IsDefault == 1)
                    .FirstOrDefault();
        }
    }
}
