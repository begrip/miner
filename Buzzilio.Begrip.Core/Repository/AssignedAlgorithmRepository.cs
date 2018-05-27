using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class AssignedAlgorithmRepository : RepositoryBase<AssignedAlgorithm>, IAssignedAlgorithmRepository
    {
        /// <summary>
        /// C-tors.
        /// </summary>
        public AssignedAlgorithmRepository() { }

        public IQueryable<AssignedAlgorithm> GetAssignedAlgorithmsForCryptoId(int cryptoId)
        {
            return GetAll().Where(c => c.CryptoId == cryptoId);
        }
    }
}
