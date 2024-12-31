using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomService.Data;
using RoomService.DTOs;

namespace RoomService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class GuestController() : ControllerBase
    {
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound POST # Guest Service");

            return Ok("Inbound test of from Rooms Controller");
        }
    }
}
