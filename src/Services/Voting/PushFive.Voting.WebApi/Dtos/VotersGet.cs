using Microsoft.AspNetCore.Mvc;

namespace PushFive.Voting.WebApi.Dtos
{
    public class VotersGet
    {
        [FromQuery]
        public int PageIndex { get; set; }

        [FromQuery]
        public int PageSize { get; set; }
    }
}
