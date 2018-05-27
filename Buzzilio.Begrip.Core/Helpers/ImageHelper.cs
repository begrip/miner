using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Settings;
using Buzzilio.Begrip.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Buzzilio.Begrip.Core.Helpers
{
    public static class ImageHelper
    {
        public static BitmapImage GetCachedImage(string imagePath)
        {
            if (ImageIsValid(imagePath))
            {
                return BitmapHelper.GetCachedBitmapImage(imagePath);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static bool ImageIsValid(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) { return false; }
            else
            {
                return Path.GetFileName(imagePath) != null;
            }
        }
    }
}
