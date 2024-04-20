namespace NotificationMessageSender.API.DTOs.Requests

{
    public class EmailRequest
    {
        public EmailRequest(string sender, string receiver, string subject, string message, string password)
        {
            Sender = sender;
            Receiver = receiver;
            Subject = subject;
            Message = message;
            Password = password;
        }

        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
    }
}
