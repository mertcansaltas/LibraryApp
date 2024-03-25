using Microsoft.AspNetCore.Identity.UI.Services;

namespace LibraryApp.Context
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Buraya email gönderme işlemlerinizi ekleyebilirsiniz.
            return Task.CompletedTask;
        }
    }
}
