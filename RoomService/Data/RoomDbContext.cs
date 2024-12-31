using Microsoft.EntityFrameworkCore;
using RoomService.Models;

namespace RoomService.Data
{
    public class RoomDbContext : DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
