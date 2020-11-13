using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using E_Ticketer.Configuration;
using E_Ticketer.Web;

namespace E_Ticketer.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class E_TicketerDbContextFactory : IDesignTimeDbContextFactory<E_TicketerDbContext>
    {
        public E_TicketerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<E_TicketerDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            E_TicketerDbContextConfigurer.Configure(builder, configuration.GetConnectionString(E_TicketerConsts.ConnectionStringName));

            return new E_TicketerDbContext(builder.Options);
        }
    }
}
