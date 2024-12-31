using AutoMapper;
using GuestService.AsyncDataServices;
using GuestService.Data;
using GuestService.DTOs;
using GuestService.Models;
using GuestService.SyncDataServices.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GuestService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GuestController(IGuestRepo guestRepo, IMapper mapper, IRoomDataClient roomDataClient, IMessageBusClient messageBusClient) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<GuestReadDto>> GetGuests()
        {
            var guests = guestRepo.GetAllGuests();

            return Ok(mapper.Map<IEnumerable<GuestReadDto>>(guests));
        }

        [HttpGet("{id}")]
        public ActionResult<GuestReadDto> GetGuestById(int id)
        {
            var guest = guestRepo.GetGuestById(id);
            if(guest != null)
            {
                return Ok(mapper.Map<GuestReadDto>(guest));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<GuestReadDto>> CreateGuest(GuestCreateDto guestCreateDto)
        {
            var guest = mapper.Map<Guest>(guestCreateDto);
            guestRepo.CreateGuest(guest);
            guestRepo.SaveChanges();

            var guestReadDto = mapper.Map<GuestReadDto>(guest);

            // Send Sync Message
            try
            {
                await roomDataClient.SendGuestToRoom(guestReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Send Async Message
            try
            {
                var guestPublishedDto = mapper.Map<GuestPublishedDto>(guestReadDto);
                guestPublishedDto.Event = "Guest_Published";
                messageBusClient.PublishNewGuest(guestPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously {ex.Message}");
            }

            return guestReadDto;
        }
    }
}
