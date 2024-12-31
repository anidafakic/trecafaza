using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomService.Data;
using RoomService.DTOs;
using RoomService.Models;

namespace RoomService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IRoomRepo repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<RoomReadDto>> GetRooms()
        {
            var rooms = repository.GetAllRooms();

            return Ok(mapper.Map<IEnumerable<RoomReadDto>>(rooms));
        }

        [HttpGet("{id}")]
        public ActionResult<RoomReadDto> GetRoomById(int id)
        {
            var guest = repository.GetRoom(id);
            if (guest != null)
            {
                return Ok(mapper.Map<RoomReadDto>(guest));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<RoomReadDto> CreateRoom(RoomCreateDto roomCreateDto)
        {
            var room = mapper.Map<Room>(roomCreateDto);
            repository.createRoom(room);
            repository.SaveChanges();

            var roomReadDto = mapper.Map<RoomReadDto>(room);

            return Ok(roomReadDto);
        }
    }
}
