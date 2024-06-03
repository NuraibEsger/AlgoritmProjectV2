namespace MyTag_API.Services.Abstract
{
    public interface IPasswordHashService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
