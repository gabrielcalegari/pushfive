using System.Collections.Generic;

namespace PushFive.Voting.WebApi.Dtos
{
    public class VotersGetResult
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<VoterDto> Voters { get; private set; }

        public VotersGetResult(int pageIndex, int pageSize, long count, IEnumerable<VoterDto> voters)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Voters = voters;
        }
    }
}
