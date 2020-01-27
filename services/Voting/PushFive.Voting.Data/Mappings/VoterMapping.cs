using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushFive.Voting.Domain.Models;

namespace PushFive.Voting.Data.Mappings
{
    public class VoterMapping : IEntityTypeConfiguration<Voter>
    {
        public void Configure(EntityTypeBuilder<Voter> builder)
        {
            builder.HasKey(voter => voter.Id);
            builder.Property(voter => voter.Name).IsRequired();
            builder.Property(voter => voter.Email)
                .HasColumnType("varchar(128)")
                .IsRequired();

            builder.HasOne(voter => voter.Voting)
                .WithOne(voting => voting.Voter)
                .HasForeignKey<Domain.Models.Voting>(voting => voting.VoterId);

            builder.ToTable("Voters");
        }
    }
}
