using Microsoft.AspNetCore.Mvc;

namespace PushFive.Catalog.WebApi.Dtos
{
    public class SongsGet
    {
        [FromQuery]
        public int PageIndex { get; set; }

        [FromQuery]
        public int PageSize { get; set; }
    }
}
