using Buzzilio.Begrip.Infrastructure.Providers.Enumerations;
using Buzzilio.Begrip.Infrastructure.Providers.Interfaces;
using Buzzilio.Begrip.Resources.Paths;
using Buzzilio.Begrip.Utilities.Helpers;
using System;
using System.Windows;

namespace Buzzilio.Begrip.Infrastructure.Providers
{
    public class DictionaryResourceProvider : IResourceProvider<ResourceDictionary>
    {
        public ResourceDictionary GetResource(string dictionaryType)
        {
            var dictionary = new ResourceDictionary();
            var dictionaryPath = string.Empty;
            var dictionaryTypeEnum = EnumHelper.ParseEnum<Enums.ResourceDictionaryType>(dictionaryType);

            switch(dictionaryTypeEnum)
            {
                case Enums.ResourceDictionaryType.ColourResourceDictionary:

                    dictionaryPath = ResourceDictionaryPaths.ColourResourceDictionaryPath;
                    break;

                case Enums.ResourceDictionaryType.VectorIconResourceDictionary:

                    dictionaryPath = ResourceDictionaryPaths.VectorIconResourceDictionaryPath;
                    break;

                case Enums.ResourceDictionaryType.ImageIconResourceDictionary:

                    dictionaryPath = ResourceDictionaryPaths.ImageIconResourceDictionaryPath;
                    break;

                default:
                    break;
            }
        
            dictionary.Source = new Uri(dictionaryPath, UriKind.RelativeOrAbsolute);
            return dictionary;
        }
    }
}
