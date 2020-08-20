using AutoMapper;
using NISApi.Data.Entity;
using NISApi.DTO;
using NISApi.DTO.Response;
using NISApi.DTO.Request;

namespace NISApi.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Person, CreatePersonRequest>().ReverseMap();
            CreateMap<Person, UpdatePersonRequest>().ReverseMap();
            CreateMap<Person, PersonQueryResponse>().ReverseMap();
        }
    }
}
