using NotificationMessageSender.Core.Common.DTOs;

namespace NotificationMessageSender.Core.Common.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(EmailRequest request);

    }
}
