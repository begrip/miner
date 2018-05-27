using Buzzilio.Begrip.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Buzzilio.Begrip.Core.Repository.Helpers
{
    public class LibraryRepositoryHelper : RepositoryHelper
    {
        public static IList<Library> FillLibraryCollection()
        {
            var libraries = GetRepositoryInstance<Library, LibraryRepository>().GetAll();

            return libraries.ToList();
        }
    }
}
