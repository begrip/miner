using Buzzilio.Begrip.Infrastructure.Providers.Enumerations;
using Buzzilio.Begrip.Infrastructure.Providers.Interfaces;
using System.Windows.Media;

namespace Buzzilio.Begrip.Infrastructure.Providers
{
    public class VectorIconProvider : IResourceProvider<Geometry>
    {
        public Geometry GetResource(string resourceName)
        {
            var provider = new DictionaryResourceProvider();
            var dictionary = provider.GetResource(Enums.ResourceDictionaryType.VectorIconResourceDictionary.ToString());

            var geometry = dictionary[resourceName];

            return geometry as Geometry;
        }
    }
}
