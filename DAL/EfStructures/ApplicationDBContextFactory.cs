using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Domain.Entities;

namespace DAL.EfStructures
{
    public class ApplicationDBContextFactory:IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public  ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            var connectionString = @"server=DESKTOP-MSIISMQ\SQLEXPRESS;Database=VKR_1;integrated security=true;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
            Console.WriteLine(connectionString);
            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}
