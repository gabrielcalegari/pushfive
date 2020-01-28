using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushFive.Voting.Domain.Repository
{
    public interface IVotingRepository : Core.Data.IRepository<Models.Voting>
    {
        Task AddVoting(Models.Voting voting);

        Models.Voting GetByVoterEmail(string email);

        Task<IEnumerable<Guid>> GetFiveMostVotedSongs();

        Task<IEnumerable<Models.Voting>> GetVotings(int pageIndex, int pageSize);

        Task<long> GetVotingsCount();
    }
}
