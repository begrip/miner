using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Messages;
using System;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Base
{
    public class TabFactoryBase : ViewModelBase<DefaultViewModelMessage>
    {
        /// <summary>
        /// Returns a viewmodel.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        private TViewModel CreateTabViewModel<TViewModel>
            (
                //Enums.ViewMode viewMode = Enums.ViewMode.New,
                TViewModel viewModel = default(TViewModel),
                object parameter = null
            )
            where TViewModel : IViewModel, new()
        {
            Enums.ViewMode mode = viewModel == null ? Enums.ViewMode.NEW : viewModel.Mode;
            switch (mode)
            {
                case Enums.ViewMode.EDIT:
                    return viewModel;

                case Enums.ViewMode.NEW:
                default:

                    return (TViewModel)Activator.CreateInstance(typeof(TViewModel), mode, parameter);
            }
        }
    }
}
