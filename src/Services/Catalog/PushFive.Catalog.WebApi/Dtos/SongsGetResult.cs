using System.Collections.Generic;

namespace PushFive.Catalog.WebApi.Dtos
{
    public class SongsGetResult
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<SongDto> Songs { get; private set; }

        public SongsGetResult(int pageIndex, int pageSize, long count, IEnumerable<SongDto> songs)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Songs = songs;
        }
    }
}
