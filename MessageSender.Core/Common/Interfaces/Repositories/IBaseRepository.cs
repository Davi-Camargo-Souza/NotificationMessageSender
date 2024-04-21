namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        public void Create(T entity, CancellationToken cancellationToken);
        public Task<T> Get(Guid id, string tabela, CancellationToken cancellationToken);
        public Task<List<T>> GetAll(string tabela, CancellationToken cancellationToken);
        public Task Delete(T entity);
    }
}
