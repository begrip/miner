namespace Buzzilio.Begrip.Core.Models.Interfaces
{
    public interface IModifiable
    {
        bool HasChanges { get; }
        void ResetModified();
    }
}
