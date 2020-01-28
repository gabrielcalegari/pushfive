using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushFive.Catalog.Domain.Models;

namespace PushFive.Catalog.Data.Mappings
{
    internal class GenreMapping : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(genre => genre.Id);
            builder.Property(genre => genre.Name)
                .IsRequired();

            // 1 : N => Genre : Songs
            builder.HasMany(genre => genre.Songs)
                .WithOne(song => song.Genre)
                .HasForeignKey(song => song.GenreId);

            builder.ToTable("Genres");
        }
    }
}
