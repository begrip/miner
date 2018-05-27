using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Infrastructure.Common
{
    public class SnackbarPayload
    {
        public object Content { get; set; }
        public object ActionContent { get; set; }
        public Action ActionHandler  { get;set; }
    }
}
