using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces
{
    public interface IViewModel
    {
        Enums.ViewMode Mode { get; set; }
        bool? DialogResult { get; set; }
        void ActionHandler(IMediatorMessage message);
        void RegisterMessagingService();
        void FillCollections();
        void RegisterCommands();
        void SetupView();
        void SetViewData();
        void Setup();
    }
}
