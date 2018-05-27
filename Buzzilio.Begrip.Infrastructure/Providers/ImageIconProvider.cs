using Buzzilio.Begrip.Infrastructure.Providers.Interfaces;
using Buzzilio.Begrip.Infrastructure.Providers.Enumerations;
using System.IO;
using System.Reflection;

namespace Buzzilio.Begrip.Infrastructure.Providers
{
    public class ImageIconProvider : IResourceProvider<string>
    {
        public string GetResource(string resourceName)
        {
            var provider = new DictionaryResourceProvider();
            var dictionary = provider.GetResource(nameof(Enums.ResourceDictionaryType.ImageIconResourceDictionary));

            var imageIcon = dictionary[resourceName];

            return imageIcon as string;
        }
    }
}
