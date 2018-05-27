namespace Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void CommitSession();
        void RollbackSession();
    }
}
