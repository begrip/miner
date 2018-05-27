using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Buzzilio.Begrip.Utilities.Helpers
{
    public static class PropertyHelper
    {
        /// <summary>
        /// T: variable type, U: object type
        /// </summary>
        /// <returns></returns>
        public static T GetField<T, U>(U obj, string fieldName)
        {
            return (T)typeof(U).GetField(fieldName).GetValue(obj);
        }

        /// <summary>
        /// T: variable type, U: object type
        /// </summary>
        /// <returns></returns>
        public static void SetField<T, U>(U obj, string fieldName, T newValue)
        {
            typeof(U).GetField(fieldName).SetValue(obj, newValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<PropertyInfo> GetClassPropertiesWithAttribute<T, A>()
            where T : class
            where A : Attribute
        {
            var properties = typeof(T).GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(A)));

            return new List<PropertyInfo>(properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<PropertyInfo> GetClassPropertiesWithAttributeAandNotB<T, A, B>()
            where T : class
            where A : Attribute
            where B : Attribute
        {
            var properties = typeof(T).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(A)))
                .Where(prop => !Attribute.IsDefined(prop, typeof(B)));

            return new List<PropertyInfo>(properties);
        }
    }
}
