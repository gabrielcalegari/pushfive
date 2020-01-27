using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushFive.Catalog.Domain.Models;

namespace PushFive.Catalog.Data.Mappings
{
    internal class SongMapping : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(song => song.Id);
            builder.Property(song => song.Name).IsRequired();

            builder.ToTable("Songs");
        }
    }
}
