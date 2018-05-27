using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Messages;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Buzzilio.Begrip.Infrastructure.GenericViewModels
{
    public class DialogPromptViewModel : ViewModelBase<DefaultViewModelMessage>,  IViewModel
    {
        #region - C-tors -
        public DialogPromptViewModel()
        {
            Setup();
        }

        #endregion - C-tors -

        #region - Properties -

        public RelayCommand ConfirmPromptCommand { get; set; }
        public RelayCommand CancelPromptCommand { get; set; }

        BitmapImage _PromptIcon;
        public BitmapImage PromptIcon
        {
            get { return _PromptIcon; }
            set { SetProperty(ref _PromptIcon, value); }
        }

        string _PromptText;
        public string PromptText
        {
            get { return _PromptText; }
            set { SetProperty(ref _PromptText, value); }
        }

        bool _DialogPromptResult;
        public bool DialogPromptResult
        {
            get { return _DialogPromptResult; }
            set { SetProperty(ref _DialogPromptResult, value); }
        }

        bool _PromptCancellable;
        public bool PromptCancellable
        {
            get { return _PromptCancellable; }
            set { SetProperty(ref _PromptCancellable, value); }
        }

        #endregion - Properties -

        #region - IViewModel Interface Methods -

        public void FillCollections()
        {

        }

        public void SetupView()
        {

        }

        public void SetViewData()
        {

        }

        public void RegisterCommands()
        {
            ConfirmPromptCommand = new RelayCommand(ConfirmPrompt);
            CancelPromptCommand = new RelayCommand(CancelPrompt);
        }
        public void Setup()
        {
            SetViewData();
            FillCollections();
            RegisterCommands();
            SetupView();
        }

        #endregion - IViewModel Interface Methods -

        #region - Actions -
        public void ConfirmPrompt(object parameter)
        {
            var dialog = parameter as Window;
            if (dialog == null)
            {
                // TODO: log
                return;
            }
            DialogPromptResult = true;
            dialog.Close();

        }

        public void CancelPrompt(object parameter)
        {
            var dialog = parameter as Window;
            if (dialog == null)
            {
                // TODO: log
                return;
            }
            DialogPromptResult = false;
            dialog.Close();
        }

        public void SetParameters(BitmapImage promptIcon, string promptText, bool promptCancellable)
        {
            PromptCancellable = promptCancellable;
            PromptIcon = promptIcon;
            PromptText = promptText;
        }

        #endregion - Actions -
    }
}
