using PushFive.Core.DomainObjects;
using System;

namespace PushFive.Catalog.Domain.Models
{
    public class Song : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Artist Artist { get; private set; }
        public Guid ArtistId { get; private set; }

        public Genre Genre { get; private set; }
        public Guid GenreId { get; private set; }

        public Song() { }

        public Song(string code, string name, Guid artistId, Guid genreId)
        {
            Code = code;
            Name = name;
            ArtistId = artistId;
            GenreId = genreId;
        }
    }
}
