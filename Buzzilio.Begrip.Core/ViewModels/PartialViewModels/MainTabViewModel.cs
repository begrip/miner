using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using System;
using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Core.Messages;
using System.Collections.ObjectModel;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Core.ViewModels.ControlViewModels;
using System.ComponentModel;
using System.Windows.Data;
using Buzzilio.Begrip.Infrastructure.Filters;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.ViewModels.PartialViewModels
{
    public class MainTabViewModel : ViewModelBase<MainTabViewModelMessage>, IViewModel, ITabViewModel
    {
        #region - C-tor -
        public MainTabViewModel()
        {
            Setup();
        }

        #endregion - C-tor - 

        #region - Commands -

        #endregion - Commands -

        #region - Properties -

        public string Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollectionView DefaultCryptoCollectionListView
        {
            get { return CollectionViewSource.GetDefaultView(CryptoCollection); }
        }

        /// <summary>
        /// Crypto collection.
        /// </summary>
        ObservableCollection<Crypto> _CryptoCollection;
        public ObservableCollection<Crypto> CryptoCollection
        {
            get { return _CryptoCollection; }
            set { SetProperty(ref _CryptoCollection, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        CryptoCardControlViewModel _cryptoCardControlViewModel;
        public CryptoCardControlViewModel CryptoCardControlViewModel
        {
            get { return _cryptoCardControlViewModel; }
            set { SetProperty(ref _cryptoCardControlViewModel, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                SetProperty(ref _searchQuery, value);
                var filterOption = new SearchFilter(FilterOptions.Text, _searchQuery);
                SetFilter(filterOption);
            }
        }

        #endregion - Properties -

        #region - Filtering - 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="filterCriteria"></param>
        private void SetFilter(SearchFilter searchFilter)
        {
            DefaultCryptoCollectionListView.Filter = null;

            switch (searchFilter.FilterOption)
            {
                case FilterOptions.Text:
                    TextFilter<Crypto> nameFilter = new TextFilter<Crypto>(o => new string[] {
                        (o as Crypto).CryptoName,
                        (o as Crypto).CryptoSymbol
                    }, (searchFilter.FilterCriteria as string));

                    DefaultCryptoCollectionListView.Filter = nameFilter.Filter;
                    break;

                case FilterOptions.All:
                default:
                    break;
            }
        }

        #endregion - Filtering - 


        public void FillCollections()
        {
            CryptoCollection = new ObservableCollection<Crypto>(CryptoRepositoryHelper.FillCryptoCollection());
        }

        public void Setup()
        {
            FillCollections();
            SetViewData();
        }

        public void RegisterCommands()
        {
            throw new NotImplementedException();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void SetViewData()
        {
            CryptoCardControlViewModel = new CryptoCardControlViewModel(Enums.ViewMode.NEW);
        }
    }
}
