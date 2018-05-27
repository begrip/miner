using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Utilities.Helpers
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
