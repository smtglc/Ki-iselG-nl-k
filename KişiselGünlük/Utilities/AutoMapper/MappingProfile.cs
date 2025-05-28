

using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Model;

namespace KişiselGünlük.Utilities.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<TextDtoForInsertion, Text>();
            CreateMap<Text, TextDto>();
            CreateMap<TextDtoForUpdate, Text>().ReverseMap();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
