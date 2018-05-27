using System.Collections.Specialized;
using System.Windows.Controls;

namespace Buzzilio.Begrip.Core.Views.Controls
{
    public class ScrollingListBox : ListBox
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count > 0)
                ScrollIntoView(Items.Count - 1);

            base.OnItemsChanged(e);
        }
    }
}
