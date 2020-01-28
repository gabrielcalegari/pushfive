using AutoMapper;
using PushFive.Catalog.Domain.Models;
using PushFive.Catalog.WebApi.Dtos;

namespace PushFive.Catalog.WebApi.AutoMapper
{
    public class SongToSongDtoConverter : ITypeConverter<Song, SongDto>
    {
        public SongDto Convert(Song source, SongDto destination, ResolutionContext context)
        {
            var songDto = new SongDto
            {
                Id = source.Id,
                Name = source.Name,
                Artist = new ArtistDto { Id = source.Artist.Id, Name = source.Artist.Name },
                Genre = new GenreDto { Id = source.Genre.Id, Name = source.Genre.Name }
            };

            return songDto;
        }
    }
}
