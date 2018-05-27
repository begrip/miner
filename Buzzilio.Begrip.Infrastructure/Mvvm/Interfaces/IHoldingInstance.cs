namespace Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces
{
    public interface IHoldingInstance<T>
    {
        T Instance { get; set; }
    }
}
