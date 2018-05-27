using Buzzilio.Begrip.Core.Helpers;
using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.ViewModels.ControlViewModels
{
    public class DisclaimerDialogViewModel : ViewModelBase<DisclaimerDialogViewModelMessage>, IViewModel
    {
        #region - C-tors -

        public DisclaimerDialogViewModel()
        {
            Setup();
        }

        #endregion -C-tor -

        #region - Properties -


        // public RelayCommand DeclineDisclaimerCommand { get; set; }
        public RelayCommand AcceptDisclaimerCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        UserSettings _selectedUserSettings;
        public UserSettings SelectedUserSettings
        {
            get { return _selectedUserSettings; }
            set { SetProperty(ref _selectedUserSettings, value); }
        }

        #endregion - Properties -

        public void FillCollections()
        {


        }

        public void RegisterCommands()
        {
            // DeclineDisclaimerCommand = new RelayCommand(DeclineDisclaimer);
            AcceptDisclaimerCommand = new RelayCommand(AcceptDisclaimer);
        }

        public void Setup()
        {
            RegisterCommands();
            SetViewData();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void SetViewData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        //public void DeclineDisclaimer(object obj)
        //{
        //    AppHelper.ShutDown();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void AcceptDisclaimer(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, obj as UserControl);
            SaveUserSettingsAfterAcceptedDisclaimer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void SaveUserSettingsAfterAcceptedDisclaimer()
        {
            SelectedUserSettings.DisclaimerIsEnabled = 0;
            var changes = SelectedUserSettings.GetChanges();
            UserSettingsRepositoryHelper.UpdateUserSettings(SelectedUserSettings, changes);
            RepositoryHelper.CommitPendingDbChanges();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Welcome!"
            });
        }
    }
}
