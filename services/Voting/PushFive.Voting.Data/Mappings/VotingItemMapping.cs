using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushFive.Voting.Domain.Models;

namespace PushFive.Voting.Data.Mappings
{
    public class VotingItemMapping : IEntityTypeConfiguration<VotingItem>
    {
        public void Configure(EntityTypeBuilder<VotingItem> builder)
        {
            builder.HasKey(item => item.Id);
            builder.Property(item => item.Order).IsRequired();
            builder.Property(item => item.SongId).IsRequired();

            builder.ToTable("VotingItems");
        }
    }
}
