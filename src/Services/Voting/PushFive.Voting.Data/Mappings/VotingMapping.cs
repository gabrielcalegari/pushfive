using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PushFive.Voting.Data.Mappings
{
    public class VotingMapping : IEntityTypeConfiguration<Domain.Models.Voting>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Voting> builder)
        {
            builder.HasKey(voting => voting.Id);
            builder.Property(voting => voting.Date);

            // 1 : N => Voting : VotingItems
            builder.HasMany(voting => voting.VotingItems)
                .WithOne(votingItem => votingItem.Voting)
                .HasForeignKey(votingItem => votingItem.VotingId);

            builder.ToTable("Votings");
        }
    }
}
