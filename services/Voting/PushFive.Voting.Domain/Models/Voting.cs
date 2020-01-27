using PushFive.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PushFive.Voting.Domain.Models
{
    public class Voting : Entity, IAggregateRoot
    {
        public Voter Voter { get; private set; }
        public Guid VoterId { get; private set; }

        private readonly List<VotingItem> votingItems;
        public IReadOnlyCollection<VotingItem> VotingItems => votingItems;

        public DateTime Date { get; private set; }

        public Voting() { }

        public Voting(Voter voter)
        {
            Voter = voter;
            Date = DateTime.Now;
            votingItems = new List<VotingItem>();
        }

        public void AddVote(VotingItem votingItem)
        {
            if(votingItem.Order < 1 && votingItem.Order > 5)
            {
                throw new DomainException("Order should be between 1 and 5");
            }

            if(votingItems.Any(item => item.Order == votingItem.Order))
            {
                throw new DomainException("A vote for the same order was already added.");
            }

            if(votingItems.Any(item => item.SongId == votingItem.SongId))
            {
                throw new DomainException("A vote for the same song was already added");
            }

            votingItems.Add(votingItem);
        }
    }
}
