using Microsoft.EntityFrameworkCore;
using UniTask_backend.Entities;

namespace UniTask_backend.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }

    public DbSet<GroupUser> GroupUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GroupUser>(entity =>
        {
            // Composite primary key of UserId + GroupId
            entity.HasKey(e => new { e.UserId, e.GroupId });

            // Configure relationship: GroupUser -> User (many-to-one)
            entity.HasOne(e => e.User)
                .WithMany(u => u.Members)  // User.Members is the navigation property
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship: GroupUser -> Group (many-to-one)
            entity.HasOne(e => e.Group)
                .WithMany(g => g.Members)  // Group.Members is the navigation property
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}