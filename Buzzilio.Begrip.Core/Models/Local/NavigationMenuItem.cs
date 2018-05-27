using static Buzzilio.Begrip.Core.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.Models.Local
{
    public class NavigationMenuItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Tooltip { get; set; }
        public NavigationMenuItemType ItemType { get; set; }
    }
}
