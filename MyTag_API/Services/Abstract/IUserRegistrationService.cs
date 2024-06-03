using MyTag_API.DTOs.Instagram;
using MyTag_API.Entities;

namespace MyTag_API.Services.Abstract
{
    public interface IUserRegistrationService
    {
        List<Instagram> MapAndAssignSocialMedia(AppUser user, List<InstagramPostDto> instagramDtos);
    }
}
