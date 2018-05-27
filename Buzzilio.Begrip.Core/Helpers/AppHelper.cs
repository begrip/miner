using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Buzzilio.Begrip.Core.Helpers
{
    public static class AppHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ShutDown()
        {
            Application.Current.Shutdown();
        }
    }
}
