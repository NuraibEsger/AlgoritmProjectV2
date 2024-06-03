using AutoMapper;
using WebApi.DTOs.Quiz;
using WebApi.Entities;

namespace WebApi.AutoMapper
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizGetDto>().ReverseMap();
            CreateMap<QuizPostDto, Quiz>().ReverseMap();
            CreateMap<QuizPutDto, Quiz>().ReverseMap();
        }
    }
}
