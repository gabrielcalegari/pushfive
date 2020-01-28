using Microsoft.EntityFrameworkCore;
using PushFive.Core.Data;
using PushFive.Voting.Domain.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PushFive.Voting.Data.Repository
{
    public class VotingRepository : IVotingRepository
    {
        private readonly VotingContext votingContext;

        public VotingRepository(VotingContext votingContext)
        {
            this.votingContext = votingContext ?? throw new ArgumentNullException(nameof(votingContext));
        }

        public IUnitOfWork UnitOfWork => votingContext;

        public async Task AddVoting(Domain.Models.Voting voting)
        {
            await votingContext.Votings.AddAsync(voting);
        }

        public Domain.Models.Voting GetByVoterEmail(string email)
        {
            return votingContext.Votings.AsNoTracking()
                    .SingleOrDefault(voting => voting.Voter.Email == email);
        }

        public void Dispose()
        {
            votingContext?.Dispose();
        }
    }
}
