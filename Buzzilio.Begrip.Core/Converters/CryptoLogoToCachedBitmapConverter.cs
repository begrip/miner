using Buzzilio.Begrip.Core.Helpers;
using Buzzilio.Begrip.Core.Settings;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Buzzilio.Begrip.Core.Converters
{
    public class CryptoLogoToCachedBitmapConverter : IValueConverter
    {
        /// <summary>
        /// TODO: pick favourite instead of first.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            BitmapImage bitMapImage = null;
            if (value != null)
            {
                string valueStr = value.ToString();
                string imagePath = Path.Combine(RuntimeSettings._cryptoLogosPath, valueStr);
                bitMapImage = ImageHelper.GetCachedImage(imagePath);
            }
            else
            {
                // Load default image
                //bitMapImage = new BitmapImage(new Uri(RuntimeSettings._foodDome64));
            }
            return bitMapImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
