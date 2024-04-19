using NotificationMessageSender.Core.Common.Interfaces;
using NotificationMessageSender.Infraestructure.Context;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext) 
        { 
            _context = appDbContext;
        }
        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }
    }
}
