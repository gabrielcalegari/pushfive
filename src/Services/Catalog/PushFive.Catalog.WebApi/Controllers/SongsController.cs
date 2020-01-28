using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PushFive.Catalog.Domain.Repository;
using PushFive.Catalog.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PushFive.Catalog.WebApi.Controllers
{
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongRepository songRepository;
        private readonly IMapper mapper;

        public SongsController(ISongRepository songRepository, IMapper mapper)
        {
            this.songRepository = songRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SongsGetResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSongs([FromQuery] SongsGet songsGet)
        {
            songsGet.PageIndex = songsGet.PageIndex > 0 ? songsGet.PageIndex : 1;
            songsGet.PageSize = songsGet.PageSize <= 20 && songsGet.PageSize > 0 ? songsGet.PageSize : 20;

            var songs = await songRepository.GetSongs(songsGet.PageIndex, songsGet.PageSize);
            var songsDto = mapper.Map<IEnumerable<SongDto>>(songs);

            var countSongs = await songRepository.CountSongs();
            var result = new SongsGetResult(songsGet.PageIndex, songsGet.PageSize, countSongs, songsDto);

            return Ok(result);
        }

        [HttpGet("ids")]
        [ProducesResponseType(typeof(SongsGetResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSongsById([FromQuery] string[] songIds)
        {
            if (songIds.Length == 0 || songIds.Length > 20)
                return BadRequest();

            var songIdGuids = songIds.Select(i => Guid.Parse(i)).ToArray();
            var songs = await songRepository.GetSongsByIds(songIdGuids);

            var songsDto = mapper.Map<IEnumerable<SongDto>>(songs);
            var result = new SongsGetResult(1, 1, songsDto.LongCount(), songsDto);

            return Ok(result);
        }
    }
}