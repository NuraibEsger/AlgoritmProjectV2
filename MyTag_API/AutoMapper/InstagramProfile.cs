using AutoMapper;
using MyTag_API.DTOs.Instagram;
using MyTag_API.Entities;

namespace MyTag_API.AutoMapper
{
    public class InstagramProfile : Profile
    {
        public InstagramProfile()
        {
            CreateMap<Instagram, InstagramGetDto>();
            CreateMap<InstagramPostDto, Instagram>();
        }
    }
}
