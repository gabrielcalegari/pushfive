using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PushFive.Core.Communication;
using PushFive.Core.Messages;
using PushFive.Voting.Domain.Command;
using PushFive.Voting.Domain.Repository;
using PushFive.Voting.WebApi.Dtos;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IVotingRepository votingRepository;

        public VotingsController(IMediatorHandler mediatorHandler, IMapper mapper, INotificationHandler<DomainNotification> domainNotificationHandler, IVotingRepository votingRepository)
        {
            this.domainNotificationHandler = (DomainNotificationHandler)domainNotificationHandler;
            this.mediatorHandler = mediatorHandler;
            this.mapper = mapper;
            this.votingRepository = votingRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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


        [HttpGet("result")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVotingResult()
        {
            var songs = (await votingRepository.GetFiveMostVotedSongs()).ToArray();

            var songsDto = new List<SongDto>();
            for (int i = 0; i < songs.Length; i++)
            {
                songsDto.Add(new SongDto { Id = songs[i], Order = i + 1 });
            }

            var result = new VotingGetResult { Songs = songsDto };
            return Ok(result);
        }
    }
}