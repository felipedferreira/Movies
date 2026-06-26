using Movies.Domain.GenreAggregate;

namespace Movies.WebService.UnitTests.Domain;

public sealed class GenreTests
{
    [Fact]
    public void Create_WithExplicitId_SetsIdAndName()
    {
        var id = Guid.NewGuid();

        var genre = Genre.Create(id, "Noir");

        Assert.Equal(id, genre.Id);
        Assert.Equal("Noir", genre.Name);
    }

    [Fact]
    public void Create_GeneratesIdAndSetsName()
    {
        var genre = Genre.Create("Drama");

        Assert.NotEqual(Guid.Empty, genre.Id);
        Assert.Equal("Drama", genre.Name);
    }
}
