using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.DataService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define the db entities
    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<Achievement> Achievements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // specified the relationship between the entities
        modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Achievements_Driver");
            }
        );
    }
}