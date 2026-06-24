using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.GenreAggregate;

namespace Movies.Persistence.Postgres.Configurations;

internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("genres");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(g => g.Name)
            .IsUnique();

        // Seed the genres. Fixed ids are required by HasData so the seed is stable across runs.
        builder.HasData(
            new Genre { Id = new Guid("9d6c31eb-2c02-4a48-b4dc-24767675f82a"), Name = "Action" },
            new Genre { Id = new Guid("d7a0f348-959f-416b-8d50-8e35a0bdbb26"), Name = "Comedy" },
            new Genre { Id = new Guid("6deddeee-0f11-4d07-a7c2-b70229ab58de"), Name = "Drama" },
            new Genre { Id = new Guid("90837e33-57a3-4793-a07b-5c47a7daeff8"), Name = "Fantasy" },
            new Genre { Id = new Guid("65515a68-1b05-4d8b-94e0-5599d671a643"), Name = "Horror" },
            new Genre { Id = new Guid("80acabbb-c0f8-428e-bd12-23187789928f"), Name = "Romance" },
            new Genre { Id = new Guid("655b1256-859a-4c9f-bed2-31ac669d0f18"), Name = "SciFi" },
            new Genre { Id = new Guid("535c3954-aa92-4b97-8a75-92cd5ef6f8c6"), Name = "Thriller" },
            new Genre { Id = new Guid("f271eb24-ef49-4cfb-af1c-edf8c45a4a37"), Name = "Animation" },
            new Genre { Id = new Guid("3687d461-e5cb-447a-81f4-c82716f4feb1"), Name = "Adventure" },
            new Genre { Id = new Guid("aef33796-2504-403c-a2ac-99ff7ce966e8"), Name = "Crime" },
            new Genre { Id = new Guid("00482a38-516a-4e2b-a4c6-f0f6e6259d2a"), Name = "Documentary" },
            new Genre { Id = new Guid("03875139-b79e-4a8d-9053-3d937c8ae165"), Name = "Family" },
            new Genre { Id = new Guid("50267304-8ba6-495b-9ef4-806d2dff1656"), Name = "History" },
            new Genre { Id = new Guid("49ef5cc8-f94a-4066-ba86-0b27aa9bd642"), Name = "Musical" },
            new Genre { Id = new Guid("bba55f7e-81c0-4f11-a9c9-057f5ee74a05"), Name = "Mystery" },
            new Genre { Id = new Guid("c469256f-6f96-4602-b546-82be46807a6f"), Name = "Western" });
    }
}
