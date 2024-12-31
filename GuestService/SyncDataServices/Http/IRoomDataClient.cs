using GuestService.DTOs;

namespace GuestService.SyncDataServices.Http
{
    public interface IRoomDataClient
    {
        Task SendGuestToRoom(GuestReadDto guestReadDto);
    }
}
