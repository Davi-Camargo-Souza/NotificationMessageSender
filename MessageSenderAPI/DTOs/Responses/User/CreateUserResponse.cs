namespace NotificationMessageSender.API.DTOs.Responses.User
{
    public class CreateUserResponse : BaseResponse
    {
        public string Cpf { get; set; }
        public Guid CompanyId { get; set; }
    }
}
