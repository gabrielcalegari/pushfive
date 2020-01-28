using MediatR;
using PushFive.Core.Messages;
using System.Threading.Tasks;

namespace PushFive.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task PublishEvent<T>(T @event) where T : Event
        {
            return mediator.Publish(@event);
        }

        public Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            return mediator.Publish(notification);
        }

        public Task<bool> SendCommand<T>(T command) where T : Command
        {
            return mediator.Send(command);
        }
    }
}
