using AutoMapper;
using GuestService.DTOs;
using GuestService.Models;

namespace GuestService.Profiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            // Source -> Target
            CreateMap<Guest, GuestReadDto>();
            CreateMap<GuestCreateDto, Guest>();
            CreateMap<GuestReadDto, GuestPublishedDto>();
        }
    }
}
