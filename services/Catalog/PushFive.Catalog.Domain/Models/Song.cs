using PushFive.Core.DomainObjects;
using System;

namespace PushFive.Catalog.Domain.Models
{
    public class Song : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Artist Artist { get; private set; }
        public Guid ArtistId { get; private set; }

        public Genre Genre { get; private set; }
        public Guid GenreId { get; private set; }

        public Song() { }

        public Song(string name, Guid artistId, Guid genreId)
        {
            Name = name;
            ArtistId = artistId;
            GenreId = genreId;
        }
    }
}
