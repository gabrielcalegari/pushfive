using PushFive.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace PushFive.Voting.Domain
{
    public class Voter : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public Voter(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    public class VotingItem : Entity
    {
        public Guid VotingId { get; private set; }
        public int Order { get; private set; }
        public Guid SongId { get; private set; }

        public VotingItem(int order, Guid songId)
        {
            Order = order;
            SongId = songId;
        }
    }

    public class Voting : Entity, IAggregateRoot
    {
        public Voter Voter { get; private set; }
        public Guid VoterId { get; private set; }

        private readonly List<VotingItem> votingItems;
        public IReadOnlyCollection<VotingItem> VotingItems => votingItems;

        public DateTime Date { get; private set; }

        public Voting(Guid voterId)
        {
            VoterId = voterId;
            Date = DateTime.Now;
            votingItems = new List<VotingItem>();
        }

        public void AddVote(VotingItem votingItem)
        {
            
        }
    }
}
