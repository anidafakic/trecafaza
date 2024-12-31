using GuestService.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestService.Data
{
    public class GuestDbContext(DbContextOptions<GuestDbContext> options) : DbContext(options)
    {
        public DbSet<Guest> Guests { get; set; }
    }
}
