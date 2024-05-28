using AutoMapper;
using Suggestions.Entities.Dto;
using Suggestions.Entities.Models;

namespace Backend.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDtoForUpdate, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserDtoForUpdate>();
        }
    }
}
