using Buzzilio.Begrip.Infrastructure.Providers.Interfaces;
using Buzzilio.Begrip.Infrastructure.Providers.Enumerations;

namespace Buzzilio.Begrip.Infrastructure.Providers
{
    public class CryptoLogoProvider : IResourceProvider<string>
    {
        public string GetResource(string resourceName)
        {
            var provider = new DictionaryResourceProvider();
            var dictionary = provider.GetResource(nameof(Enums.ResourceDictionaryType.CryptoLogoResourceDictionary));

            var imageIcon = dictionary[resourceName];

            return imageIcon as string;
        }
    }
}
