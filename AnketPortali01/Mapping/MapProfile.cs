using AnketPortali01.Dtos;
using AnketPortali01.Models;
using AutoMapper;

namespace AnketPortali01.Mapping
{
    public class MapProfile : Profile
    {


        public MapProfile()
        {
            CreateMap<messaging, MessagingDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
