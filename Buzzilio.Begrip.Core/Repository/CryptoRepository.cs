using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class CryptoRepository : RepositoryBase<Crypto>, ICryptoRepository
    {
        /// <summary>
        /// C-tors.
        /// </summary>
        public CryptoRepository() { }

        /// <summary>
        /// Get Crypto by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Crypto GetByName(string name)
        {
            return GetAll().FirstOrDefault(c => c.CryptoName == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cryptoId"></param>
        /// <returns></returns>
        public Crypto GetCryptoById(int cryptoId)
        {
            return GetAll()
                    .Where(c => c.CryptoId == cryptoId)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool Exists(string name)
        {
            return GetAll().Any(i => i.CryptoName == name);
        }
    }
}
