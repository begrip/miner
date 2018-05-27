using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Helpers;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Core.Scheduler;

namespace Buzzilio.Begrip.Core.ViewModels.PartialViewModels
{
    public class SettingsTabViewModel : ViewModelBase<MainViewModelMessage>, IViewModel, ITabViewModel
    {
        #region - C-tor -
        public SettingsTabViewModel()
        {
            Setup();
        }

        #endregion - C-tor - 

        #region - Commands -

        public RelayCommand SaveUserSettingsCommand { get; set; }
        public RelayCommand LoadDefaultUserSettingsCommand { get; set; }
        public RelayCommand CancelUserSettingsChangesCommand { get; set; }

        #endregion - Commands -

        #region - Properties -

        public string Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        UserSettings _selectedUserSettings;
        public UserSettings SelectedUserSettings
        {
            get { return _selectedUserSettings; }
            set { SetProperty(ref _selectedUserSettings, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        UserSettings _defaultUserSettings;
        public UserSettings DefaultUserSettings
        {
            get { return _defaultUserSettings; }
            set { SetProperty(ref _defaultUserSettings, value); }
        }

        #endregion - Properties -


        public void FillCollections()
        {

        }

        public void Setup()
        {
            RegisterCommands();
            FillCollections();
            SetViewData();
            SetupView();
        }

        public void RegisterCommands()
        {
            SaveUserSettingsCommand = new RelayCommand(SaveUserSettings);
            CancelUserSettingsChangesCommand = new RelayCommand(CancelUserSettingsChanges);
            LoadDefaultUserSettingsCommand = new RelayCommand(LoadDefaultUserSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetupView()
        {

        }

        public void SetViewData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void SaveUserSettings(object ob)
        {
            var changes = SelectedUserSettings.GetChanges();
            UserSettingsRepositoryHelper.UpdateUserSettings(SelectedUserSettings, changes);
            RepositoryHelper.CommitPendingDbChanges();
            SelectedUserSettings.CacheObject();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Saved user settings"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void LoadDefaultUserSettings(object ob)
        {
            SelectedUserSettings.DefaultTheme = DefaultUserSettings.DefaultTheme;
            SelectedUserSettings.DisclaimerIsEnabled = DefaultUserSettings.DisclaimerIsEnabled;
            SelectedUserSettings.ShareTime = DefaultUserSettings.ShareTime;
            SelectedUserSettings.MaxConcurrentMiners = DefaultUserSettings.MaxConcurrentMiners;
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Loaded default user settings"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void CancelUserSettingsChanges(object ob)
        {
            SelectedUserSettings.RestoreCachedObject();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Discarded changes"
            });
        }
    }
}
