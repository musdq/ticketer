using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using E_Ticketer.Authorization.Roles;
using E_Ticketer.Authorization.Users;
using E_Ticketer.Bookings;
using E_Ticketer.MultiTenancy;
using E_Ticketer.Stations;
using E_Ticketer.Tickets;

namespace E_Ticketer.EntityFrameworkCore
{
    public class E_TicketerDbContext : AbpZeroDbContext<Tenant, Role, User, E_TicketerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public E_TicketerDbContext(DbContextOptions<E_TicketerDbContext> options)
            : base(options)
        {
        }
    }
}
