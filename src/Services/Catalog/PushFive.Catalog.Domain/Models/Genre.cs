using PushFive.Core.DomainObjects;
using System.Collections.Generic;

namespace PushFive.Catalog.Domain.Models
{
    public class Genre : Entity
    {
        public string Name { get; private set; }

        // EF Relation
        public ICollection<Song> Songs { get; set; }

        public Genre() { }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
