namespace MyTag_API.Services.Abstract
{
    public interface ISendEmailService
    {
        Task<bool> EmailSend(string userEmail, string confirmationLink);
    }
}
