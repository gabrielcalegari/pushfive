using System.Threading.Tasks;

namespace PushFive.Voting.Domain.Repository
{
    public interface IVotingRepository : Core.Data.IRepository<Models.Voting>
    {
        Task AddVoting(Models.Voting voting);

        Domain.Models.Voting GetByVoterEmail(string email);
    }
}
