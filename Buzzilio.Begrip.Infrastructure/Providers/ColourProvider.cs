using Buzzilio.Begrip.Infrastructure.Providers.Enumerations;
using Buzzilio.Begrip.Infrastructure.Providers.Interfaces;
using System.Windows.Media;

namespace Buzzilio.Begrip.Infrastructure.Providers
{
    public class ColourProvider : IResourceProvider<Brush>
    {
        public Brush GetResource(string resource)
        {
            var provider = new DictionaryResourceProvider();
            var dictionary = provider.GetResource(Enums.ResourceDictionaryType.ColourResourceDictionary.ToString());
            var colourName = resource.Substring(resource.IndexOf('_') + 1);
            var brush = dictionary[colourName];

            return brush as Brush;
        }
    }
}
