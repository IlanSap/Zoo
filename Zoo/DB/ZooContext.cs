using Microsoft.EntityFrameworkCore;

public class ZooContext : DbContext
{
    public DbSet<Zoo> Zoos { get; set; }
    public DbSet<Animal> Animals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=your_server;Database=ZooDB;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zoo>()
            .HasMany(z => z._animals)
            .WithOne()
            .HasForeignKey(a => a.ZooId);

        base.OnModelCreating(modelBuilder);
    }
}