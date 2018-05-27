using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Buzzilio.Begrip.Utilities.Helpers
{
    public static class BitmapHelper
    {
        /// <summary>
        /// Get name string of a BMP image by source.
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static string BmpImageToNameString(ImageSource imageSource)
        {
            return Path.GetFileName(imageSource.ToString());
        }

        /// <summary>
        /// Returns a cached bitmap image from image path.
        /// If image path is invalid, returns null.
        /// </summary>
        /// <returns></returns>
        public static BitmapImage GetCachedBitmapImage(string imagePath)
        {
            var cachedBitmapImage = new BitmapImage();
            cachedBitmapImage.BeginInit();
            cachedBitmapImage.UriSource = new Uri(imagePath);
            cachedBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            cachedBitmapImage.EndInit();

            return cachedBitmapImage;
        }
    }
}
