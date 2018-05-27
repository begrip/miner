using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Infrastructure.Common;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using System;
using System.Windows;

namespace Buzzilio.Begrip.Core.ViewModels.ControlViewModels
{
    public class SystemTrayViewModel : ViewModelBase<SystemTrayViewModelMessage>, IViewModel
    {
        #region - C-tors -

        public SystemTrayViewModel() { Setup(); }

        #endregion -C-tor -

        #region - Properties -

        public RelayCommand LeftClickCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string _tooltipMessage;
        public string TooltipMessage
        {
            get { return _tooltipMessage; }
            set { SetProperty(ref _tooltipMessage, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        Visibility _isVisible = Visibility.Hidden;
        public Visibility IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _showInTaskbar = true;
        public bool ShowInTaskbar
        {
            get { return _showInTaskbar; }
            set { SetProperty(ref _showInTaskbar, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                if (value == WindowState.Minimized)
                {
                    IsVisible = Visibility.Visible;
                    ShowInTaskbar = false;
                }
                SetProperty(ref _windowState, value);
            }
        }

        #endregion - Properties -


        public void FillCollections()
        {
            throw new NotImplementedException();
        }

        public void RegisterCommands()
        {
            LeftClickCommand = new RelayCommand(HandleLeftClick);
        }

        public void Setup()
        {
            SetViewData();
            RegisterCommands();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void SetViewData()
        {

        }

        public void HandleLeftClick(object obj)
        {
            WindowState = WindowState.Normal;
            IsVisible = Visibility.Hidden;
            ShowInTaskbar = true;
        }

    }
}
