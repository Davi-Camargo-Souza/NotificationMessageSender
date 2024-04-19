using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.Common.Exceptions
{
    public class LimiteDeNotificacoesAtingidaException : Exception
    {
        public LimiteDeNotificacoesAtingidaException() : base("Limite de notificações alcançado, atualize seu contrato ou solicite mais notificações amanhã.") { }
        public LimiteDeNotificacoesAtingidaException(string mensagem) : base(mensagem) { }
        public LimiteDeNotificacoesAtingidaException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
    }
}
