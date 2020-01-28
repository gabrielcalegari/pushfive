using Microsoft.EntityFrameworkCore;
using PushFive.Core.Data;
using PushFive.Voting.Domain.Models;
using PushFive.Voting.Domain.Repository;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Guid>> GetFiveMostVotedSongs()
        {
            return await votingContext.VotingItems.AsNoTracking()
                 .GroupBy(v => v.SongId)
                 .OrderBy(g => g.Count())
                 .Select(g => g.Key)
                 .Take(5)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Voting>> GetVotings(int pageIndex, int pageSize)
        {
            return await votingContext.Votings.AsNoTracking()
                .Include(v => v.Voter)
                .Include(v => v.VotingItems)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<long> GetVotingsCount()
        {
            return await votingContext.Votings.LongCountAsync();
        }

        public void Dispose()
        {
            votingContext?.Dispose();
        }
    }
}
