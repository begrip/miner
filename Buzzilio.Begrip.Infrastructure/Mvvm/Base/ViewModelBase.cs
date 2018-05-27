using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Helpers;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using GalaSoft.MvvmLight.Messaging;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Base
{
    public class ViewModelBase<T> : PropertyChangedHelper
        where T : class, IMediatorMessage
    {
        /// <summary>
        /// Dialog result.
        /// </summary>
        bool? _DialogResult;
        public bool? DialogResult
        {
            get { return _DialogResult; }
            set { SetProperty(ref _DialogResult, value); }
        }

        /// <summary>
        /// Determines whether viewmodel is loaded with an existing or a new context. 
        /// </summary>
        Enums.ViewMode _mode;
        public Enums.ViewMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        bool _changeIsPending;
        public bool ChangeIsPending
        {
            get { return _changeIsPending; }
            set { SetProperty(ref _changeIsPending, value); }
        }

        /// <summary>
        /// C-tor
        /// </summary>
        public ViewModelBase()
        {
            RegisterMessagingService();
            SetDefaultValues();
        }

        /// <summary>
        /// Virtual handler for incoming actions on the view.
        /// </summary>
        /// <param name="viewModelMessage"></param>
        public virtual void ActionHandler(IMediatorMessage message) { }

        /// <summary>
        /// Sends a message using the messaging service provided within Mvvm Light.
        /// </summary>
        public void SendMessage<M>(M message) { Messenger.Default.Send(message); }

        /// <summary>
        /// Registers messaging service provided within Mvvm Light.
        /// </summary>
        public void RegisterMessagingService()
        {
            Messenger.Default.Register<T>(this, ActionHandler);
        }

        /// <summary>
        /// Sets default values.
        /// </summary>
        public void SetDefaultValues()
        {
            Mode = Enums.ViewMode.NOT_SET;
            ChangeIsPending = false;
        }
    }
}