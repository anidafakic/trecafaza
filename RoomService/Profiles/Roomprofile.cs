using AutoMapper;
using RoomService.DTOs;
using RoomService.Models;

namespace RoomService.Profiles
{
    public class Roomprofile : Profile
    {
        public Roomprofile()
        {
            CreateMap<Room, RoomReadDto>();
            CreateMap<RoomCreateDto, Room>();
        }
    }
}
