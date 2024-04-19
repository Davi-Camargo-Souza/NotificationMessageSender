namespace NotificationMessageSender.Core.Common.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Create(T entity, CancellationToken cancellationToken);
        //void Inativar(Guid id, string tabela, CancellationToken cancellationToken);
        //void Ativar(Guid id, string tabela, CancellationToken cancellationToken);
        Task<T> Get(Guid id, string tabela, CancellationToken cancellationToken);
        Task<List<T>> GetAll(string tabela, CancellationToken cancellationToken);
        Task<List<T>> GetAllAtivos(string tabela, CancellationToken cancellationToken);
        Task<List<T>> GetAllInativos(string tabela, CancellationToken cancellationToken);
        //void Update(T entity);
        void Delete(T entity);
    }
}
