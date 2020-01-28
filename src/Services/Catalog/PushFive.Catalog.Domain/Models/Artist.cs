using PushFive.Core.DomainObjects;
using System.Collections.Generic;

namespace PushFive.Catalog.Domain.Models
{
    public class Artist : Entity
    {
        public string Name { get; private set; }

        // EF Relation
        public ICollection<Song> Songs { get; set; }

        public Artist() { }

        public Artist(string name)
        {
            Name = name;
        }
    }
}
