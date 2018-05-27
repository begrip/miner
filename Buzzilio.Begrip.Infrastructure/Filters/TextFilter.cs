using Buzzilio.Begrip.Infrastructure.Filters.Interfaces;
using Buzzilio.Begrip.Utilities.Extensions;
using System;

namespace Buzzilio.Begrip.Infrastructure.Filters
{
    public class TextFilter<T> : IFilter
    {
        public Func<object, string[]> Property;
        public string Target { get; set; }

        public TextFilter(Func<object, string[]> property, string target)
        {
            Property = property;
            Target = target;
        }

        public virtual bool Filter(object item)
        {
            var contains = false;
            foreach (string property in Property(item))
            {
                contains = contains || property.StringContains(Target, StringComparison.OrdinalIgnoreCase);
            }
            return contains;
        }
    }
}
