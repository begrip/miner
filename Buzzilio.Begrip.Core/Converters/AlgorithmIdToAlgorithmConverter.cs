using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository;
using Buzzilio.Begrip.Core.Repository.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Buzzilio.Begrip.Core.Converters
{
    public class AlgorithmIdToAlgorithmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var algorithmRepository = RepositoryHelper.GetRepositoryInstance<Algorithm, AlgorithmRepository>();
            var algorithm = algorithmRepository.GetAlgorithmById((int)value);
            return algorithm;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (value as Algorithm).AlgorithmId;
        }
    }
}
