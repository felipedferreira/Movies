using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.TitleAggregate;

namespace Movies.Persistence.Postgres.Configurations;

internal sealed class TitleConfiguration : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.ToTable("titles");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(m => m.Name)
            .HasColumnName("title")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(m => m.Type)
            .HasColumnName("title_type")
            .HasMaxLength(50)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(m => m.YearOfRelease)
            .HasColumnName("year_of_release")
            .IsRequired();

        builder.Property(m => m.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

        // Genre is a separate aggregate, referenced by identity only. The ids are stored as a
        // Postgres uuid[] column on the titles table — no join table, no cross-aggregate FK.
        builder.Property(m => m.GenreIds)
            .HasColumnName("genre_ids")
            .HasColumnType("uuid[]");
    }
}
