using Microsoft.AspNetCore.Mvc;
using System;
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

    public class SongsGet
    {
        [FromQuery]
        public int PageIndex { get; set; }

        [FromQuery]
        public int PageSize { get; set; }
    }

    public class SongDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }
    }
}
