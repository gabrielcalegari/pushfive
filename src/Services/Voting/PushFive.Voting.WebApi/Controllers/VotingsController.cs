using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PushFive.Core.Communication;
using PushFive.Core.Messages;
using PushFive.Voting.Domain.Command;
using PushFive.Voting.WebApi.Dtos;
using System.Net;
using System.Threading.Tasks;

namespace PushFive.Voting.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VotingsController : ControllerBase
    {
        private readonly DomainNotificationHandler domainNotificationHandler;
        private readonly IMediatorHandler mediatorHandler;
        private readonly IMapper mapper;

        public VotingsController(IMediatorHandler mediatorHandler, IMapper mapper, INotificationHandler<DomainNotification> domainNotificationHandler)
        {
            this.domainNotificationHandler = (DomainNotificationHandler) domainNotificationHandler;
            this.mediatorHandler = mediatorHandler;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostVoting(VotingPost votingPost)
        {
            var command = mapper.Map<AddVotingCommand>(votingPost);
            await mediatorHandler.SendCommand(command);
            if (domainNotificationHandler.HasNotifications())
            {
                foreach (var notification in domainNotificationHandler.GetNotifications())
                {
                    ModelState.AddModelError(notification.Key, notification.Value);
                }

                return BadRequest(ModelState);
            }

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}