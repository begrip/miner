using Buzzilio.Begrip.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class CryptoRepositoryHelper : RepositoryHelper
    {
        /// <summary>
        /// Returns a crypto collection from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        public static IList<Crypto> FillCryptoCollection()
        {
            var cryptos = GetRepositoryInstance<Crypto, CryptoRepository>().GetAll();
            var assignedAlgorithmRepository = GetRepositoryInstance<AssignedAlgorithm, AssignedAlgorithmRepository>();
            var algorithmRepository = GetRepositoryInstance<Algorithm, AlgorithmRepository>();

            foreach (var crypto in cryptos)
            {
                crypto.CryptoAlgorithmCollection = new ObservableCollection<Algorithm>();
                var assignedAlgorithms = assignedAlgorithmRepository.GetAssignedAlgorithmsForCryptoId(crypto.CryptoId);
                foreach (var assignedAlgorithm in assignedAlgorithms)
                {
                    var algorithm = algorithmRepository.GetAlgorithmById(assignedAlgorithm.AlgorithmId);
                    crypto.CryptoAlgorithmCollection.Add(algorithm);
                }
            }
            return cryptos.ToList();
        }
    }
}
