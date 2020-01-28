using System.Collections.Generic;

namespace PushFive.Voting.WebApi.Dtos
{
    public class VotingGetResult
    {
        public IEnumerable<SongDto> Songs { get; set; }
    }
}
