using GuestService.DTOs;
using GuestService.SyncDataServices.Http;
using System.Text;
using System.Text.Json;

namespace GuestService.SyncDataServices
{
    public class HttpRoomDataClient : IRoomDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpRoomDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendGuestToRoom(GuestReadDto guestReadDto)
        {
            var httpContent = new StringContent(
                    JsonSerializer.Serialize(guestReadDto),
                    Encoding.UTF8,
                    "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["RoomService"]}", httpContent); 
            
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sync POST to RoomService was OK!");
            }
            else
            {
                Console.WriteLine("Sync POST to RoomService was NOT OK!");
            }
        }
    }
}
