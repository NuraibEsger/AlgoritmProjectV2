using AutoMapper;
using WebApi.DTOs.Question;
using WebApi.Entities;

namespace WebApi.AutoMapper
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionGetDto>().ReverseMap();
            CreateMap<QuestionPostDto, Question>().ReverseMap();
            CreateMap<QuestionPutDto, Question>().ReverseMap();
        }
    }
}
