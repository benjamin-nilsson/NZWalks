using AutoMapper;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Model.Domain.Region, Model.DTO.Region>()
                .ReverseMap();
        }
    }
}
