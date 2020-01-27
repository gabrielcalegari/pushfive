using PushFive.Core.DomainObjects;
using System;

namespace PushFive.Voting.Domain.Models
{
    public class Voter : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public Guid VotingId { get; private set; }

        /// EL. Rel.
        public Voting Voting { get; private set; }

        public Voter() { }

        public Voter(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
