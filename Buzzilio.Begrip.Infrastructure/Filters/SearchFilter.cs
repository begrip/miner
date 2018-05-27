using Buzzilio.Begrip.Infrastructure.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Infrastructure.Filters
{
    public class SearchFilter
    {
        public Enums.FilterOptions FilterOption { get; set; }
        public object FilterCriteria { get; set; }

        public SearchFilter(Enums.FilterOptions filterOption, object filterCriteria)
        {
            FilterOption = filterOption;
            FilterCriteria = filterCriteria;
        }
    }
}
