using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.Core.Common.DTOs;
using NotificationMessageSender.Core.Common.Interfaces;
using System.Net;
using System.Net.Mail;

namespace NotificationMessageSender.API.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailRequest request)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(request.Sender, request.Password)
            };

            await client.SendMailAsync(
                new MailMessage(from: request.Sender,
                to: request.Receiver,
                request.Subject,
                request.Message
                ));
        }

    }
}
