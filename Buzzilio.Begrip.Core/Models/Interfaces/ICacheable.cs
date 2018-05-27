using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Core.Models.Interfaces
{
    public interface ICacheable
    {
        void CacheObject();
        void ResetCachedObject();
        void RestoreCachedObject();
    }
}
