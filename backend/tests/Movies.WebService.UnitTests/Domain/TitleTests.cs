using Movies.Domain.Enums;
using Movies.Domain.TitleAggregate;

namespace Movies.WebService.UnitTests.Domain;

public sealed class TitleTests
{
    [Fact]
    public void Create_WithExplicitId_SetsPropertiesAndGenreIds()
    {
        var id = Guid.NewGuid();
        var actionGenreId = Guid.NewGuid();
        var dramaGenreId = Guid.NewGuid();

        var title = Title.Create(
            id,
            "Heat",
            TitleType.Movie,
            1995,
            "A crime drama.",
            [actionGenreId, dramaGenreId]);

        Assert.Equal(id, title.Id);
        Assert.Equal("Heat", title.Name);
        Assert.Equal(TitleType.Movie, title.Type);
        Assert.Equal(1995, title.YearOfRelease);
        Assert.Equal("A crime drama.", title.Description);
        Assert.True(title.GenreIds.SetEquals([actionGenreId, dramaGenreId]));
    }

    [Fact]
    public void Create_GeneratesIdAndSetsGenreIds()
    {
        var genreId = Guid.NewGuid();

        var title = Title.Create(
            "The Bear",
            TitleType.TvSeries,
            2022,
            null,
            [genreId]);

        Assert.NotEqual(Guid.Empty, title.Id);
        Assert.True(title.GenreIds.SetEquals([genreId]));
    }

    [Fact]
    public void AddGenre_WhenGenreIdAlreadyExists_KeepsSingleGenreId()
    {
        var genreId = Guid.NewGuid();
        var title = CreateTitleWithoutGenres();

        title.AddGenre(genreId);
        title.AddGenre(genreId);

        Assert.True(title.GenreIds.SetEquals([genreId]));
    }

    [Fact]
    public void AddGenre_WhenGenreIdIsEmpty_ThrowsArgumentException()
    {
        var title = CreateTitleWithoutGenres();

        var exception = Assert.Throws<ArgumentException>(() => title.AddGenre(Guid.Empty));

        Assert.Equal("genreId", exception.ParamName);
    }

    [Fact]
    public void RemoveGenre_WhenGenreIdExists_RemovesItAndReturnsTrue()
    {
        var actionGenreId = Guid.NewGuid();
        var dramaGenreId = Guid.NewGuid();
        var title = Title.Create(
            Guid.NewGuid(),
            "Heat",
            TitleType.Movie,
            1995,
            null,
            [actionGenreId, dramaGenreId]);

        var removed = title.RemoveGenre(actionGenreId);

        Assert.True(removed);
        Assert.True(title.GenreIds.SetEquals([dramaGenreId]));
    }

    [Fact]
    public void RemoveGenre_WhenGenreIdDoesNotExist_ReturnsFalse()
    {
        var title = CreateTitleWithoutGenres();

        var removed = title.RemoveGenre(Guid.NewGuid());

        Assert.False(removed);
        Assert.Empty(title.GenreIds);
    }

    [Fact]
    public void ReplaceGenres_ReplacesExistingGenreIds()
    {
        var originalGenreId = Guid.NewGuid();
        var replacementGenreId = Guid.NewGuid();
        var title = Title.Create(
            Guid.NewGuid(),
            "Heat",
            TitleType.Movie,
            1995,
            null,
            [originalGenreId]);

        title.ReplaceGenres([replacementGenreId]);

        Assert.True(title.GenreIds.SetEquals([replacementGenreId]));
    }

    private static Title CreateTitleWithoutGenres()
    {
        return Title.Create(
            Guid.NewGuid(),
            "Heat",
            TitleType.Movie,
            1995,
            null,
            []);
    }
}
