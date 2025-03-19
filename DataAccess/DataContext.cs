using Company_Expense_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company_Expense_Tracker.DataAccess;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;
            var baseEntity = (BaseEntity)entity.Entity;

            if (entity.State == EntityState.Added)
            {
                baseEntity.CreatedDate = now;
            }

            baseEntity.UpdatedDate = now;
        }
    }
    
    public DbSet<Department> Departments { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.ToTable("departments");

            entity.HasMany(d => d.Expenses)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(d => d.Workers)
                .WithOne(w => w.Department)
                .HasForeignKey(w => w.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.ToTable("workers");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("expenses");
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("users"); 
        });
    }
}