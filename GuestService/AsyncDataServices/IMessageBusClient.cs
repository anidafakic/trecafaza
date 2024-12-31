using GuestService.DTOs;

namespace GuestService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewGuest(GuestPublishedDto guestPublishedDto);
    }
}
