using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;

namespace NotificationMessageSender.Worker.BackgroundServices.Base
{
    public class CreateNotificationCommandWorker
    {
        IMediator _mediator;

        public CreateNotificationCommandWorker(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Process (CreateNotificationCommand command)
        {
            var sendNotificationCommand = new SendNotificationCommand(command);
            _mediator.Send(command);
        } 
    }
}
