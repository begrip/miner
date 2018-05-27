using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Interfaces;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository
{
    public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
    {
        /// <summary>
        /// C-tors.
        /// </summary>
        public LibraryRepository() { }

        /// <summary>
        /// Get Crypto by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Library GetByName(string name)
        {
            return GetAll().FirstOrDefault(c => c.LibraryName == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public Library GetLibraryById(int libraryId)
        {
            return GetAll()
                    .Where(c => c.LibraryId == libraryId)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool Exists(string name)
        {
            return GetAll().Any(i => i.LibraryName == name);
        }
    }
}
