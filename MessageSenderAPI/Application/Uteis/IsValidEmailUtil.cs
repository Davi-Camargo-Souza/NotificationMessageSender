using System.Net.Mail;

namespace NotificationMessageSender.API.Application.Uteis
{
    public static class IsValidEmailUtil
    {
        public static bool Check(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }
    }
}