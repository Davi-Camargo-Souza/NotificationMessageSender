namespace NotificationMessageSender.API.DTOs.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string Cpf { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string BearerToken { get; set; }
    }
}
