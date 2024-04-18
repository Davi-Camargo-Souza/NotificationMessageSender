using MessageSender.Core.Common.Interfaces;
using MessageSender.Infraestructure.Context;

namespace MessageSender.Infraestructure.Repositories
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
