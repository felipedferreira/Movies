using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Persistence.Postgres.Constants;
using Movies.Persistence.Postgres.Entities;

namespace Movies.Persistence.Postgres.Configurations;

internal sealed class TitleGenreConfiguration : IEntityTypeConfiguration<TitleGenre>
{
    public void Configure(EntityTypeBuilder<TitleGenre> builder)
    {
        builder.ToTable(DatabaseConstants.TitleGenre.Table);

        builder.HasKey(titleGenre => new { titleGenre.TitleId, titleGenre.GenreId })
            .HasName(DatabaseConstants.TitleGenre.PrimaryKey);

        builder.Property(titleGenre => titleGenre.TitleId)
            .HasColumnName("titleId")
            .IsRequired();

        builder.Property(titleGenre => titleGenre.GenreId)
            .HasColumnName("genreId")
            .IsRequired();

        builder.HasOne<Movies.Domain.TitleAggregate.Title>()
            .WithMany()
            .HasForeignKey(titleGenre => titleGenre.TitleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Movies.Domain.GenreAggregate.Genre>()
            .WithMany()
            .HasForeignKey(titleGenre => titleGenre.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(titleGenre => titleGenre.GenreId)
            .HasDatabaseName(DatabaseConstants.TitleGenre.GenreIndex);
    }
}
