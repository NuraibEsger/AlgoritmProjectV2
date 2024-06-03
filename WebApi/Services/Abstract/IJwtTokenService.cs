namespace WebApi.Services.Abstract
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string firstName, string lastName, string userName,List<string> roles);
    }
}
