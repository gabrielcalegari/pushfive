﻿using Microsoft.AspNetCore.Mvc;
using PushFive.Catalog.Domain.Repository;
using PushFive.Catalog.WebApi.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PushFive.Catalog.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongRepository songRepository;

        public SongsController(ISongRepository songRepository)
        {
            this.songRepository = songRepository ?? throw new ArgumentNullException(nameof(songRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs([FromQuery] SongsGet songsGet)
        {
            songsGet.PageIndex = songsGet.PageIndex > 0 ? songsGet.PageIndex : 1;
            songsGet.PageSize = songsGet.PageSize <= 20 && songsGet.PageSize > 0 ? songsGet.PageSize : 20;

            var songs = await songRepository.GetSongs(songsGet.PageIndex, songsGet.PageSize);
            var songsDto = songs.Select(song => new SongDto
            {
                Id = song.Id,
                Code = song.Code,
                Name = song.Name,
                Artist = song.Artist.Name,
                Genre = song.Genre.Name
            });

            var countSongs = await songRepository.CountSongs();
            var result = new SongsGetResult(songsGet.PageIndex, songsGet.PageSize, countSongs, songsDto);

            return Ok(result);
        }
    }
}