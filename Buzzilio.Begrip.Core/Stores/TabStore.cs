using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace Buzzilio.Begrip.Core.Factories
{
    public class TabStore : TabFactoryBase
    {
        private ObservableCollection<ITabViewModel> _TabsCollection;
        public TabStore(ref ObservableCollection<ITabViewModel> tabsCollection)
        {
            _TabsCollection = tabsCollection;
        }

        public void CreateTab(ITabViewModel viewModel)
        {
            _TabsCollection.Add(viewModel);
        }

        public ITabViewModel GetTabByHeader(string header)
        {
            return _TabsCollection.Where(c => c.Header == header).FirstOrDefault();
        }

        public int GetTabCount<T>()
        {
            int count = 0;
            foreach (ITabViewModel viewModel in _TabsCollection)
            {
                if (viewModel is T)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
