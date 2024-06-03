using AutoMapper;
using WebApi.DTOs.Option;
using WebApi.DTOs.Quiz;
using WebApi.Entities;

namespace WebApi.AutoMapper
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionGetDto>().ReverseMap();
            CreateMap<OptionPostDto, Option>().ReverseMap();
            CreateMap<OptionPutDto, Option>().ReverseMap();
        }
    }
}
