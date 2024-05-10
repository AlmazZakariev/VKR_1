using Microsoft.Extensions.Configuration;
using DAL.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.IO;


namespace DAL.Controllers
{
    public class ConnectionHelper
    {
        
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        public static ApplicationDBContext GetContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            var connectionString = configuration.GetConnectionString("MSSQL");
            optionsBuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            return new ApplicationDBContext(optionsBuilder.Options);
        }
        public static ApplicationDBContext GetSecondContext(ApplicationDBContext oldContext, IDbContextTransaction trans)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionsBuilder.UseSqlServer(
                oldContext.Database.GetDbConnection(),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            var context = new ApplicationDBContext(optionsBuilder.Options);
            context.Database.UseTransaction(trans.GetDbTransaction());
            return context;
        }
    }
}
