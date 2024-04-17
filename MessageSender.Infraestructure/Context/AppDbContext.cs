using MessageSender.Core.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageSender.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<NotificationsRequestsEntity> NotificationsRequests { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
