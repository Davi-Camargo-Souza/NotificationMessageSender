namespace NotificationMessageSender.API.DTOs.Responses.User
{
    public class GetUserResponse : BaseResponse
    {
        public string Cpf { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
