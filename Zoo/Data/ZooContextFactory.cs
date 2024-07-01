using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ZooProject.Data;

namespace ZooProject.Data;


public class ZooContextFactory : IDesignTimeDbContextFactory<ZooContext>
{
    public ZooContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ZooContext>();
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ZooDB;Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;");

        return new ZooContext(optionsBuilder.Options);
    }
}
