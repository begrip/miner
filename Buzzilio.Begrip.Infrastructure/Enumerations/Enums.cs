using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Infrastructure.Enumerations
{
    public static class Enums
    {
        public enum MessagePurpose
        {
            SET_SELECTED_TAB,
            OPEN_SNACKBAR,
            STOP_ALL_WORKERS,
            STOP_ALL_WORKERS_CONFIRM,
            STATUS_UPDATE_MINER_STOPPED,
            STATUS_UPDATE_MINER_RUNNING
        }

        public enum FilterOptions
        {
            All,
            Text
        }
    }
}
