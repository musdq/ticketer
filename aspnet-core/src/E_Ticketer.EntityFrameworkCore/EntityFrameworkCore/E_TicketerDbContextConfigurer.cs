using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace E_Ticketer.EntityFrameworkCore
{
    public static class E_TicketerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<E_TicketerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<E_TicketerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
