using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Buzzilio.Begrip.Utilities.Extensions
{
    public static class PropertyExtensions
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination)
        {
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");

            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };

            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public static List<string> GetPropertyStringList(this object pObject)
        {
            var propertyList = new List<string>();
            var properties = pObject.GetType().GetProperties().ToList();

            properties.ForEach(c => propertyList.Add(c.Name));

            return propertyList;
        }
    }
}
