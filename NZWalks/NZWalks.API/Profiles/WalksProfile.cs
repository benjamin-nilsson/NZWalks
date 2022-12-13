using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Model.Domain.Walk, Model.DTO.Walk>()
                .ReverseMap();
        }
    }
}
