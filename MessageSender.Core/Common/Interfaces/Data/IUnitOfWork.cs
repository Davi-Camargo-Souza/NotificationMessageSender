namespace NotificationMessageSender.Core.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken);
    }
}
