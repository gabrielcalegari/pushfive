using MediatR;
using PushFive.Core.Communication;
using PushFive.Core.Messages;
using PushFive.Voting.Domain.Models;
using PushFive.Voting.Domain.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PushFive.Voting.Domain.Command
{
    public class VotingCommandHandler : IRequestHandler<AddVotingCommand, bool>
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IVotingRepository votingRepository;

        public VotingCommandHandler(IMediatorHandler mediatorHandler, IVotingRepository votingRepository)
        {
            this.mediatorHandler = mediatorHandler;
            this.votingRepository = votingRepository;
        }

        public async Task<bool> Handle(AddVotingCommand request, CancellationToken cancellationToken)
        {
            if (!IsCommandValid(request))
                return false;

            var voting = votingRepository.GetByVoterEmail(request.Email);
            if(voting != null)
            {
                await mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "There is already a vote for this email."));
                return false;
            }

            var voter = new Voter(request.Name, request.Email);
            voting = new Models.Voting(voter);

            if (request.VotingItems.Count() != 5)
            {
                await mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "There are less than 5 songs."));
                return false;
            }

            foreach (var item in request.VotingItems)
            {
                voting.AddVote(new VotingItem(item.Order, item.SongId));
            }

            await votingRepository.AddVoting(voting);

            return await votingRepository.UnitOfWork.Commit();
        }

        private bool IsCommandValid(Core.Messages.Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
