using System;

namespace PushFive.Catalog.WebApi.Dtos
{
    public class SongDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArtistDto Artist { get; set; }

        public GenreDto Genre { get; set; }
    }
}
