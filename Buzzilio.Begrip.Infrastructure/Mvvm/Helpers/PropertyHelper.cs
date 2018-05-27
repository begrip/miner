using Buzzilio.Begrip.Infrastructure.Mvvm.Attributes;
using System.Collections.Generic;
using System.Reflection;
using PropertyHelperUtil = Buzzilio.Begrip.Utilities.Helpers.PropertyHelper;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Helpers
{
    public static  class PropertyHelper
   {
        /// <summary>
        /// 
        /// </summary>
        public static List<PropertyInfo> GetDataMemberClassProperties<T>() where T : class
        {
            var properties = PropertyHelperUtil.GetClassPropertiesWithAttributeAandNotB<T, DataMember, StaticMember>();

            return new List<PropertyInfo>(properties);
        }
    }
}
