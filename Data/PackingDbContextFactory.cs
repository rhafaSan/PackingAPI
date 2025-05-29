using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PackingServiceApi.Data
{
    public class PackingDbContextFactory : IDesignTimeDbContextFactory<EmpacotamentoDbContext>
    {
        public EmpacotamentoDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<EmpacotamentoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EmpacotamentoDbContext(optionsBuilder.Options);
        }
    }
}
