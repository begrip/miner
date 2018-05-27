using Buzzilio.Begrip.Infrastructure.GenericViewModels;
using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Messages;
using Buzzilio.Begrip.Infrastructure.Mvvm.SupportClasses;
using Buzzilio.Begrip.Resources.GenericViews;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Base
{
    public class ViewFactoryBase : ViewModelBase<DefaultViewModelMessage>
    {
        /// <summary>
        /// Creates a view with a viewmodel.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        public TView CreateView<TView, TViewModel>
            (
                //Enums.ViewMode viewMode = Enums.ViewMode.New,
                TViewModel viewModel = default(TViewModel),
                object parameter = null
            )
            where TView : MetroWindow, new()
            where TViewModel : IViewModel, new()
        {
            Enums.ViewMode mode = viewModel == null ? Enums.ViewMode.NEW : viewModel.Mode;
            TView view;
            switch (mode)
            {
                case Enums.ViewMode.EDIT:

                    view = new TView { DataContext = viewModel };
                    viewModel.DialogResult = null;
                    break;

                case Enums.ViewMode.NEW:
                default:

                    view = new TView { DataContext = Activator.CreateInstance(typeof(TViewModel), mode, parameter) };
                    break;
            }
            return view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUserControl"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public TUserControl CreateUserControl<TUserControl, TViewModel>
        (
            //Enums.ViewMode viewMode = Enums.ViewMode.New,
            TViewModel viewModel = default(TViewModel),
            object parameter = null
        )
         where TUserControl : UserControl, new()
        where TViewModel : IViewModel, new()
        {
            Enums.ViewMode mode = viewModel == null ? Enums.ViewMode.NEW : viewModel.Mode;
            TUserControl view;
            switch (mode)
            {
                case Enums.ViewMode.EDIT:

                    view = new TUserControl { DataContext = viewModel };
                    viewModel.DialogResult = null;
                    break;

                case Enums.ViewMode.NEW:
                default:

                    view = new TUserControl { DataContext = Activator.CreateInstance(typeof(TViewModel), mode, parameter) };
                    viewModel.DialogResult = null;
                    break;
            }
            return view;
        }

        /// <summary>
        /// Closes a view open via Dialog.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        public void CloseView<T>(object parameter) where T : class, IMediatorMessage
        {
            (parameter as IViewModel).DialogResult = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? OpenCustomDialogPrompt(DialogPromptSettings settings)
        {
            var dialog = new DialogPromptView();
            var viewModel = new DialogPromptViewModel();
            viewModel.SetParameters(new BitmapImage(settings.DialogImage), settings.DialogMessage, settings.DialogCancellable);
            dialog.DataContext = viewModel;

            dialog.ShowDialog();

            if (settings.DialogCancellable) { return viewModel.DialogPromptResult; }
            else { return true; }
        }

        /// <summary>
        /// Displays a requested view.
        /// </summary>
        /// <param name="message"></param>
        public void OpenView<TView, TViewModel>
            (
                //Enums.ViewMode viewMode = Enums.ViewMode.New,
                TViewModel viewModel = default(TViewModel),
                object parameter = null
            )
            where TView : MetroWindow, new()
            where TViewModel : IViewModel, new()
        {
            CreateView<TView, TViewModel>(viewModel, parameter).ShowDialog();
        }

        /// <summary>
        /// Displays a requested view.
        /// </summary>
        /// <param name="message"></param>
        public Task<object> OpenDialogAsync<TUserControl, TViewModel>
            (
                string dialogIdentifier,
                DialogOpenedEventHandler openedEventHandler,
                DialogClosingEventHandler closingEventHandler,
                TViewModel viewModel = default(TViewModel),
                object parameter = null
            )
            where TUserControl : UserControl, new()
            where TViewModel : IViewModel, new()
        {
            var view = CreateUserControl<TUserControl, TViewModel>(viewModel, parameter);
            return DialogHost.Show(view, dialogIdentifier, openedEventHandler, closingEventHandler);
        }
    }
}
