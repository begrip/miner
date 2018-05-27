using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using System.Windows.Media;
using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Core.ViewModels.PartialViewModels;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Core.Messages;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;
using System.Diagnostics;

namespace Buzzilio.Begrip.Core.ViewModels.ControlViewModels
{
    public class CryptoCardControlViewModel : ViewModelBase<CryptoCardControlViewModelMessage>, IViewModel
    {
        #region - Properties -

        public RelayCommand EditConfigurationCommand { get; set; }
        public RelayCommand AddToFavouritesCommand { get; set; }
        public RelayCommand OpenUrlCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Crypto _selectedCrypto;
        public Crypto SelectedCrypto
        {
            get { return _selectedCrypto; }
            set { SetProperty(ref _selectedCrypto, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        Brush _FavouritesIconColour;
        public Brush FavouritesIconColour
        {
            get { return _FavouritesIconColour; }
            set { SetProperty(ref _FavouritesIconColour, value); }
        }

        #endregion - Properties -

        #region - C-tors -

        public CryptoCardControlViewModel() { }

        public CryptoCardControlViewModel(params object[] parameters)
        {
            Mode = (Enums.ViewMode)parameters[0];

            Setup();
        }

        #endregion -C-tor -

        #region - IViewModel Interface Methods -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void SetViewData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void FillCollections()
        {

        }

        /// <summary>
        /// Registers actions within the view.
        /// </summary>
        public void RegisterCommands()
        {
            EditConfigurationCommand = new RelayCommand(EditConfiguration);
            AddToFavouritesCommand = new RelayCommand(AddToFavourites);
            OpenUrlCommand = new RelayCommand(OpenUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetupView()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void Setup()
        {
            SetViewData();
            FillCollections();
            RegisterCommands();
            SetupView();
        }

        #endregion - IViewModel Interface Methods -

        #region - Actions -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void OpenUrl(object parameter)
        {
            var url = parameter as string;
            Process.Start(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void EditConfiguration(object parameter)
        {
            var crypto = parameter as Crypto;
            var header = string.Format("{0} ({1})", crypto.CryptoName, crypto.CryptoSymbol);
            var tab = InstanceStore.TabStore.GetTabByHeader(header);
            if (tab == null)
            {
                var configuration = ConfigurationRepositoryHelper.GetConfigurationByCryptoId(crypto.CryptoId);
                var defaultConfiguration = ConfigurationRepositoryHelper.GetDefaultConfigurationByCryptoId(crypto.CryptoId);
                var viewModel = new ConfigurationTabViewModel
                {
                    Header = header,
                    SelectedConfiguration = configuration,
                    DefaultConfiguration = defaultConfiguration,
                };
                InstanceStore.TabStore.CreateTab(viewModel);
                viewModel.SelectedConfiguration.CacheObject();
                viewModel.SelectedConfiguration.ResetModified();
            }
            else
            {
                SendMessage(new MainViewModelMessage() { Purpose = MessagePurpose.SET_SELECTED_TAB, Payload = tab });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void AddToFavourites(object obj)
        {

        }

        #endregion - Actions -

        #region - Cached Images -


        #endregion - Cached Images - 

        #region - Messaging - 

        /// <summary>
        /// Handler for incoming actions on the view.
        /// </summary>
        /// <param name="viewModelMessage"></param>
        public override void ActionHandler(IMediatorMessage message)
        {
        }

        #endregion - Messaging -
    }
}
