using NotificationMessageSender.Core.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NotificationMessageSender.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<NotificationsRequestEntity> NotificationsRequests { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
