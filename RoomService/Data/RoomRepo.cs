using RoomService.Models;

namespace RoomService.Data
{
    public class RoomRepo(RoomDbContext context) : IRoomRepo
    {
        public void createRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));

            context.Rooms.Add(room);
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return context.Rooms.ToList();
        }

        public Room GetRoom(int roomId)
        {
            var room = context.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null) throw new ArgumentNullException(nameof(room));
            return room;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
