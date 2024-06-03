using AutoMapper;
using MyTag_API.DTOs.Instagram;
using MyTag_API.Entities;
using MyTag_API.Services.Abstract;

namespace MyTag_API.Services.Concrete
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IMapper _mapper;

        public UserRegistrationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Instagram> MapAndAssignSocialMedia(AppUser user, List<InstagramPostDto> instagramDtos)
        {
            var instagrams = _mapper.Map<List<Instagram>>(instagramDtos);

            foreach (var instagram in instagrams)
            {
                instagram.AppUserId = user.Id;
            }

            return instagrams;
        }
    }
}
