namespace NotificationMessageSender.Core.Common.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(dynamic request);

    }
}
