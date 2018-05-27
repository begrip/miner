using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using Buzzilio.Begrip.Core.ViewModels.PartialViewModels;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Core.Models.Local;
using static Buzzilio.Begrip.Core.Enumerations.Enums;
using System.Threading;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Core.Stores;
using MaterialDesignThemes.Wpf;
using Buzzilio.Begrip.Core.Views.Controls;
using Buzzilio.Begrip.Core.ViewModels.ControlViewModels;
using static Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations.Enums;
using Buzzilio.Begrip.Core.Helpers;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;
using Buzzilio.Begrip.Infrastructure.Providers;
using static Buzzilio.Begrip.Resources.Enumerations.Enums;
using System.Windows;
using Dragablz;
using Buzzilio.Begrip.Core.Models;

namespace Buzzilio.Begrip.Core.ViewModels
{
    public class MainViewModel : ViewModelBase<MainViewModelMessage>, IViewModel
    {
        int _minerTabShutDownNotified = 0;
        ImageIconProvider _ImageIconProvider;
        SettingsTabViewModel _SettingsTabViewModel;
        bool _ShutDownInProgress = false;
        string _AppVersion = "0.0.1";

        public ObservableCollection<ITabViewModel> TabViewModels { get; set; }
        public ObservableCollection<NavigationMenuItem> NavigationMenuItems { get; set; }

        public RelayCommand ProcessExitRequestCommand { get; set; }
        public RelayCommand ProcessOpenAboutDialogRequestCommand { get; set; }
        public RelayCommand HideSnackbarCommand { get; set; }
        public RelayCommand ProcessNavigationMenuItemCommand { get; set; }
        public RelayCommand OpenDisclaimerDialogCommand { get; set; }
        public DialogClosingEventHandler DisclaimerDialogClosingHandler { get; set; }
        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        /// <summary>
        /// 
        /// </summary>
        SystemTrayViewModel _systemTrayViewModel;
        public SystemTrayViewModel SystemTrayViewModel
        {
            get { return _systemTrayViewModel; }
            set { SetProperty(ref _systemTrayViewModel, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        ITabViewModel _selectedTabViewModel;
        public ITabViewModel SelectedTabViewModel
        {
            get { return _selectedTabViewModel; }
            set
            {
                SetProperty(ref _selectedTabViewModel, value);
                if (value == null)
                {
                    SetSelectedNavigationMenuItem(nameof(TabHeader.Dashboard));
                }
                else
                {
                    SetSelectedNavigationMenuItem(value.Header);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        string _appHeaderMessage;
        public string AppHeaderMessage
        {
            get { return _appHeaderMessage; }
            set { SetProperty(ref _appHeaderMessage, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _appVersionStatusText;
        public string AppVersionStatusText
        {
            get { return _appVersionStatusText; }
            set { SetProperty(ref _appVersionStatusText, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _statusBarText;
        public string StatusBarText
        {
            get { return _statusBarText; }
            set { SetProperty(ref _statusBarText, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _snackbarMessage;
        public string SnackbarMessage
        {
            get { return _snackbarMessage; }
            set { SetProperty(ref _snackbarMessage, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GenericDialogHostIdentier
        {
            get { return nameof(Dialogs.GENERIC_CENTER); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _snackbarIsActive;
        public bool SnackbarIsActive
        {
            get { return _snackbarIsActive; }
            set { SetProperty(ref _snackbarIsActive, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _navigationMenuIsOpen;
        public bool NavigationMenuIsOpen
        {
            get { return _navigationMenuIsOpen; }
            set { SetProperty(ref _navigationMenuIsOpen, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _isMining;
        public bool IsMining
        {
            get { return _isMining; }
            set { SetProperty(ref _isMining, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AppIcon
        {
            get
            {
                return _ImageIconProvider.GetResource(nameof(ImageIcons.PICK_48_ICO));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        NavigationMenuItem _selectedNavigationMenuItem;
        public NavigationMenuItem SelectedNavigationMenuItem
        {
            get { return _selectedNavigationMenuItem; }
            set { SetProperty(ref _selectedNavigationMenuItem, value); }
        }

        #region - C-tors -

        public MainViewModel() { /*not used*/}

        public MainViewModel(params object[] parameters)
        {
            Setup();
        }

        #endregion -C-tors -

        public void FillCollections()
        {
            _ImageIconProvider = new ImageIconProvider();
            InstanceStore.WorkerStore = new WorkerStore();
            SystemTrayViewModel = new SystemTrayViewModel
            {
                TooltipMessage = "Stopped"
            };
            _SettingsTabViewModel = CreateSettingsTabViewModel();
        }

        public void RegisterCommands()
        {
            ProcessExitRequestCommand = new RelayCommand(ProcessExitRequest);
            ProcessOpenAboutDialogRequestCommand = new RelayCommand(ProcessOpenAboutDialogRequest);
            HideSnackbarCommand = new RelayCommand(HideSnackbar);
            ProcessNavigationMenuItemCommand = new RelayCommand(ProcessNavigationMenuItem);
            OpenDisclaimerDialogCommand = new RelayCommand(OpenDisclaimerDialog);
            DisclaimerDialogClosingHandler = new DialogClosingEventHandler(DisclaimerDialogClosing);
        }

        public void SetupView()
        {

        }

        public void SetViewData()
        {
            AppHeaderMessage = "Begrip Cryptocurrency Miner";
            AppVersionStatusText = $"v{InstanceStore.ApplicationVersion.SoftwareVersion} ({InstanceStore.ApplicationVersion.ReleaseName})";
            StatusBarText = "Stopped";
            NavigationMenuIsOpen = false;
            IsMining = false;
            var dashboard = new NavigationMenuItem { Name = nameof(TabHeader.Dashboard), Icon = "Coin", Tooltip = "Open Dashboard", ItemType = NavigationMenuItemType.DASHBOARD };
            var userSettings = new NavigationMenuItem { Name = nameof(TabHeader.Settings), Icon = "Settings", Tooltip = "Open Settings", ItemType = NavigationMenuItemType.USER_SETTINGS };
            var closeApplication = new NavigationMenuItem { Name = "Exit", Icon = "ExitToApp", Tooltip = "Close Accplication", ItemType = NavigationMenuItemType.CLOSE_APP };
            NavigationMenuItems = new ObservableCollection<NavigationMenuItem> { dashboard, userSettings, closeApplication };
            ProcessNavigationMenuItem(nameof(TabHeader.Dashboard));
            TabViewModels = InstanceStore.TabViewModels;
        }

        public void Setup()
        {
            FillCollections();
            RegisterCommands();
            SetViewData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void ActionHandler(IMediatorMessage message)
        {
            var action = message as MainViewModelMessage;
            switch (action.Purpose)
            {
                case MessagePurpose.SET_SELECTED_TAB:
                    var tabViewModel = action.Payload as ITabViewModel;
                    SelectedTabViewModel = tabViewModel;
                    break;
                case MessagePurpose.OPEN_SNACKBAR:
                    var snackbarMessage = action.Payload as string;
                    SnackbarIsActive = true;
                    SnackbarMessage = snackbarMessage;
                    HideSnackbarAfterXSeconds(2);
                    break;
                case MessagePurpose.STOP_ALL_WORKERS_CONFIRM:
                    ProcessExitNotified();
                    break;
                case MessagePurpose.STATUS_UPDATE_MINER_RUNNING:
                    SetStatusText(StatusBarHelper.AddRunningMinerToStatus(StatusBarText, action.Payload as string));
                    IsMining = InstanceStore.WorkerStore.HasActiveNonSupportWorkers();
                    break;
                case MessagePurpose.STATUS_UPDATE_MINER_STOPPED:
                    SetStatusText(StatusBarHelper.RemoveRunningMinerFromStatus(StatusBarText, action.Payload as string));
                    IsMining = InstanceStore.WorkerStore.HasActiveNonSupportWorkers();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessExitNotified()
        {
            if (InstanceStore.TabStore.GetTabCount<ConfigurationTabViewModel>() == ++_minerTabShutDownNotified)
            {
                AppHelper.ShutDown();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessExitRequest(object obj = null)
        {
            if (_ShutDownInProgress) { return; }

            if (InstanceStore.TabStore.GetTabCount<ConfigurationTabViewModel>() == 0)
            {
                AppHelper.ShutDown();
            }
            else
            {
                SendMessage(new ConfigurationTabViewModelMessage() { Purpose = MessagePurpose.STOP_ALL_WORKERS });
            }
            _ShutDownInProgress = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void ProcessOpenAboutDialogRequest(object obj = null)
        {
            var viewModel = new AboutDialogViewModel
            {
                Mode = ViewMode.EDIT,
            };
            var result = await InstanceStore.ViewFactory
                .OpenDialogAsync<AboutDialog, AboutDialogViewModel>(
                nameof(Dialogs.GENERIC_CENTER),
                AboutDialogOpened,
                AboutDialogClosing,
                viewModel: viewModel);
        }

        /// <summary>
        /// TODO: graceful shutdown and dialog box
        /// </summary>
        /// <param name="obj"></param>
        public void ProcessNavigationMenuItem(object obj)
        {
            var name = obj as string;
            if (name == nameof(AppStates.Exit))
            {
                ProcessExitRequest();
            }
            else
            {
                OpenTabForNavigationMenuItem(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenTabForNavigationMenuItem(string menuItemName)
        {
            var tab = InstanceStore.TabStore.GetTabByHeader(menuItemName);
            if (tab == null)
            {
                switch (menuItemName)
                {
                    case nameof(TabHeader.Dashboard):
                        SelectedTabViewModel = new MainTabViewModel { Header = nameof(TabHeader.Dashboard) };
                        InstanceStore.TabStore.CreateTab(SelectedTabViewModel);
                        break;
                    case nameof(TabHeader.Settings):
                        SelectedTabViewModel = _SettingsTabViewModel;
                        InstanceStore.TabStore.CreateTab(SelectedTabViewModel);
                        (SelectedTabViewModel as SettingsTabViewModel).SelectedUserSettings.CacheObject();
                        (SelectedTabViewModel as SettingsTabViewModel).SelectedUserSettings.ResetModified();
                        break;
                    default:
                        throw new InvalidOperationException("MainViewModel::OpenTabFromNavigationMenu: invalid tab name!");
                }
            }
            else if (tab == SelectedTabViewModel) { }
            else
            {
                SelectedTabViewModel = tab;
            }
            NavigationMenuIsOpen = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SettingsTabViewModel CreateSettingsTabViewModel()
        {
            var userSettings = UserSettingsRepositoryHelper.GetUserSettings();
            var defaultUserSettings = UserSettingsRepositoryHelper.GetDefaultUserSettings();
            var viewModel = new SettingsTabViewModel
            {
                Header = nameof(TabHeader.Settings),
                SelectedUserSettings = userSettings,
                DefaultUserSettings = defaultUserSettings,
            };
            return viewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        public void SetSelectedNavigationMenuItem(string itemName)
        {
            SelectedNavigationMenuItem = NavigationMenuItems.FirstOrDefault(c => c.Name == itemName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        public void HideSnackbarAfterXSeconds(int seconds)
        {
            if (SnackbarIsActive == false) { return; }
            Timer timer = null;
            timer = new Timer((obj) =>
            {
                HideSnackbar(null);
                timer.Dispose();
            },
            null, seconds * 1000, Timeout.Infinite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void HideSnackbar(object obj)
        {
            SnackbarIsActive = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetStatusText(string text)
        {
            StatusBarText = text;
            SystemTrayViewModel.TooltipMessage = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
        {
            if (args.DragablzItem.Content is ConfigurationTabViewModel)
            {
                var configViewModel = args.DragablzItem.Content as ConfigurationTabViewModel;
                if (configViewModel.WorkerIsRunning)
                {
                    configViewModel.Stop();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async void OpenDisclaimerDialog(object obj)
        {
            var userSettings = UserSettingsRepositoryHelper.GetUserSettings();

            if (userSettings.DisclaimerIsEnabled == 1)
            {
                var viewModel = new DisclaimerDialogViewModel
                {
                    Mode = ViewMode.EDIT,
                    SelectedUserSettings = userSettings
                };
                var result = await InstanceStore.ViewFactory
                    .OpenDialogAsync<DisclaimerDialog, DisclaimerDialogViewModel>(
                    nameof(Dialogs.GENERIC_CENTER),
                    DisclaimerDialogOpened,
                    DisclaimerDialogClosing,
                    viewModel: viewModel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void DisclaimerDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void DisclaimerDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void AboutDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void AboutDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {

        }
    }
}
