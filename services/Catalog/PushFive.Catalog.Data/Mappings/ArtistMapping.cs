using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushFive.Catalog.Domain.Models;

namespace PushFive.Catalog.Data.Mappings
{
    internal class ArtistMapping : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(artist => artist.Id);
            builder.Property(artist => artist.Name)
                .IsRequired();

            // 1 : N => Artist : Songs
            builder.HasMany(artist => artist.Songs)
                .WithOne(song => song.Artist)
                .HasForeignKey(song => song.ArtistId);

            builder.ToTable("Artists");
        }
    }
}
