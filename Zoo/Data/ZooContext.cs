using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooProject.Animals.AnimalTypes;
using ZooProject.Zoo;

namespace ZooProject.Data;

public class ZooContext : DbContext
{
    public DbSet<Zoo.Zoo> Zoos { get; set; }
    public DbSet<Animal> Animals { get; set; }

    public ZooContext(DbContextOptions<ZooContext> options) : base(options) { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ZooDB;Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
            .Property(a => a.AnimalId)
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedNever();

        base.OnModelCreating(modelBuilder);
    }
}
