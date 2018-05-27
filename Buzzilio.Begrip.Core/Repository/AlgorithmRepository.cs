using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class AlgorithmRepository : RepositoryBase<Algorithm>, IAlgorithmRepository
    {
        /// <summary>
        /// C-tors.
        /// </summary>
        public AlgorithmRepository() { }

        /// <summary>
        /// Get Crypto by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Algorithm GetByName(string name)
        {
            return GetAll().FirstOrDefault(c => c.AlgorithmName == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorithmId"></param>
        /// <returns></returns>
        public Algorithm GetAlgorithmById(int algorithmId)
        {
            return GetAll()
                    .Where(c => c.AlgorithmId == algorithmId)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool Exists(string name)
        {
            return GetAll().Any(i => i.AlgorithmName == name);
        }
    }
}
