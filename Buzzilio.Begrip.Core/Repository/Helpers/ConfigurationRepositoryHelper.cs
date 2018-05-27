using Buzzilio.Begrip.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class ConfigurationRepositoryHelper : RepositoryHelper
    {
        /// <summary>
        /// Returns a configuration collection from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        public static IList<Configuration> FillConfigurationCollection()
        {
            var configurations = GetRepositoryInstance<Configuration, ConfigurationRepository>().GetAll();
            var cryptoRepository = GetRepositoryInstance<Crypto, CryptoRepository>();
            var algorithmRepository = GetRepositoryInstance<Algorithm, AlgorithmRepository>();

            foreach (var configuration in configurations)
            {
                var crypto = cryptoRepository.GetCryptoById(configuration.CryptoId);
                configuration.Crypto = crypto ?? null;

                var algorithm = algorithmRepository.GetAlgorithmById(configuration.AlgorithmId);
                configuration.AlgorithmName = algorithm.AlgorithmName;
            }
            return configurations.ToList();
        }

        public static Configuration GetConfigurationByCryptoId(int cryptoId)
        {
            var configuration = GetRepositoryInstance<Configuration, ConfigurationRepository>().GetConfigurationByCryptoId(cryptoId);
            var cryptoRepository = GetRepositoryInstance<Crypto, CryptoRepository>();
            var algorithmRepository = GetRepositoryInstance<Algorithm, AlgorithmRepository>();
            var algorithm = algorithmRepository.GetAlgorithmById(configuration.AlgorithmId);
            configuration.AlgorithmName = algorithm.AlgorithmName;
            configuration.Crypto = cryptoRepository.GetCryptoById(configuration.CryptoId);
            return configuration;
        }

        public static Configuration GetDefaultConfigurationByCryptoId(int cryptoId)
        {
            var configuration = GetRepositoryInstance<Configuration, ConfigurationRepository>().GetDefaultConfigurationByCryptoId(cryptoId);
            var cryptoRepository = GetRepositoryInstance<Crypto, CryptoRepository>();
            var algorithmRepository = GetRepositoryInstance<Algorithm, AlgorithmRepository>();
            var algorithm = algorithmRepository.GetAlgorithmById(configuration.AlgorithmId);
            configuration.AlgorithmName = algorithm.AlgorithmName;
            configuration.Crypto = cryptoRepository.GetCryptoById(configuration.CryptoId);
            return configuration;
        }

        public static void UpdateConfiguration(Configuration config, List<string> properties)
        {
            UpdateElement<Configuration, ConfigurationRepository>(config, properties);
        }
    }
}
