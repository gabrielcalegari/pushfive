using PushFive.Core.DomainObjects;
using System;

namespace PushFive.Voting.Domain.Models
{
    public class VotingItem : Entity
    {
        public Guid VotingId { get; private set; }
        public int Order { get; private set; }
        public Guid SongId { get; private set; }

        // EF Rel.
        public Voting Voting { get; private set; }

        public VotingItem() { }
        public VotingItem(int order, Guid songId)
        {
            Order = order;
            SongId = songId;
        }
    }
}
