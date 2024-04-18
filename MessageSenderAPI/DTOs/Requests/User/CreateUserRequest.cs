namespace NotificationMessageSender.API.DTOs.Requests.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
    }
}
