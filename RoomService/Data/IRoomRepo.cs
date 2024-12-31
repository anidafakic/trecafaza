using RoomService.Models;

namespace RoomService.Data
{
    public interface IRoomRepo
    {
        bool SaveChanges();

        IEnumerable<Room> GetAllRooms();
        void createRoom(Room room);
        Room GetRoom(int roomId);
    }
}
