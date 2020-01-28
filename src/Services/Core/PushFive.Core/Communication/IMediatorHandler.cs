using PushFive.Core.Messages;
using System.Threading.Tasks;

namespace PushFive.Core.Communication
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;

        Task PublishEvent<T>(T @event) where T : Event;

        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
