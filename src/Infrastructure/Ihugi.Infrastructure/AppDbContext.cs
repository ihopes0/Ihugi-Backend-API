using Ihugi.Domain.Entities;
using Ihugi.Domain.Entities.Chats;
using Ihugi.Infrastructure.ModelBuilders;
using Ihugi.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Ihugi.Infrastructure;

// TODO: XML docs
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options: options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatModelBuilder());
        modelBuilder.ApplyConfiguration(new UserModelBuilder());
        modelBuilder.ApplyConfiguration(new ChatMemberModelBuilder());
        
        base.OnModelCreating(modelBuilder);
    }
}