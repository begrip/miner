using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Infrastructure.Providers.Interfaces
{
    public interface IResourceProvider<T>
    {
        T GetResource(string resourceName);
    }
}
