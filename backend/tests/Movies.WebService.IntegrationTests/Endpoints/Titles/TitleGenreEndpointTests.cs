using System.Net;
using System.Net.Http.Json;
using Movies.WebService.Contracts.Enums;
using Movies.WebService.Contracts.Requests;
using Movies.WebService.Contracts.Responses;

namespace Movies.WebService.IntegrationTests.Titles;

public sealed class TitleGenreEndpointTests(WebApplicationFixture fixture) : IClassFixture<WebApplicationFixture>
{
    private const string TitlesEndpoint = "/movies-svc/titles";
    private const string GenresEndpoint = "/movies-svc/genres";

    [Fact]
    public async Task CreateTitle_WithDescription_PersistsDescription()
    {
        var request = new CreateTitlesRequest
        {
            Title = "Blade Runner 2049",
            Type = TitleType.Movie,
            YearOfRelease = 2017,
            Description = "A young blade runner discovers a long-buried secret.",
        };

        var createResponse = await fixture.Client.PostAsJsonAsync(TitlesEndpoint, request);
        Assert.NotNull(createResponse.Headers.Location);

        var locationUri = createResponse.Headers.Location.ToString();
        var idString = locationUri.Substring("/titles/".Length);
        var id = Guid.Parse(idString);

        var fetched = await fixture.Client.GetFromJsonAsync<TitleResponse>($"{TitlesEndpoint}/{id}");

        Assert.NotNull(fetched);
        Assert.Equal(request.Description, fetched.Description);
    }

    [Fact]
    public async Task CreateTitle_WithGenreIds_LinksGenres()
    {
        var genres = await GetSeededGenresAsync();
        var sciFi = genres.Single(genre => genre.Name == "SciFi");
        var thriller = genres.Single(genre => genre.Name == "Thriller");

        var request = new CreateTitlesRequest
        {
            Title = "Minority Report",
            Type = TitleType.Movie,
            YearOfRelease = 2002,
            GenreIds = [sciFi.Id, thriller.Id],
        };

        var createResponse = await fixture.Client.PostAsJsonAsync(TitlesEndpoint, request);
        Assert.NotNull(createResponse.Headers.Location);

        var locationUri = createResponse.Headers.Location.ToString();
        var idString = locationUri.Substring("/titles/".Length);
        var id = Guid.Parse(idString);

        var fetched = await fixture.Client.GetFromJsonAsync<TitleDetailsResponse>($"{TitlesEndpoint}/{id}");

        Assert.NotNull(fetched);
        Assert.Equal(2, fetched.Genres.Count());
        Assert.Contains(fetched.Genres, genre => genre.Id == sciFi.Id && genre.Name == "SciFi");
        Assert.Contains(fetched.Genres, genre => genre.Id == thriller.Id);
    }

    [Fact]
    public async Task UpdateTitle_ReplacesLinkedGenres()
    {
        var genres = await GetSeededGenresAsync();
        var action = genres.Single(genre => genre.Name == "Action");
        var drama = genres.Single(genre => genre.Name == "Drama");
        var crime = genres.Single(genre => genre.Name == "Crime");

        var createResponse = await fixture.Client.PostAsJsonAsync(TitlesEndpoint, new CreateTitlesRequest
        {
            Title = "Heat",
            Type = TitleType.Movie,
            YearOfRelease = 1995,
            GenreIds = [action.Id, drama.Id],
        });
        Assert.NotNull(createResponse.Headers.Location);

        var locationUri = createResponse.Headers.Location.ToString();
        var idString = locationUri.Substring("/titles/".Length);
        var id = Guid.Parse(idString);

        var updateResponse = await fixture.Client.PutAsJsonAsync($"{TitlesEndpoint}/{id}", new UpdateTitlesRequest
        {
            Title = "Heat",
            Type = TitleType.Movie,
            YearOfRelease = 1995,
            GenreIds = [crime.Id],
        });
        Assert.Equal(HttpStatusCode.Accepted, updateResponse.StatusCode);

        var fetched = await fixture.Client.GetFromJsonAsync<TitleDetailsResponse>($"{TitlesEndpoint}/{id}");

        Assert.NotNull(fetched);
        var genre = Assert.Single(fetched.Genres);
        Assert.Equal(crime.Id, genre.Id);
    }

    [Fact]
    public async Task CreateTitle_WithUnknownGenreId_Returns404()
    {
        var request = new CreateTitlesRequest
        {
            Title = "Ghost Genre",
            Type = TitleType.Movie,
            YearOfRelease = 2020,
            GenreIds = [Guid.NewGuid()],
        };

        var response = await fixture.Client.PostAsJsonAsync(TitlesEndpoint, request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<IReadOnlyList<GenreResponse>> GetSeededGenresAsync()
    {
        var response = await fixture.Client.GetFromJsonAsync<GenresResponse>(GenresEndpoint);
        Assert.NotNull(response);
        return response.Genres.ToList();
    }
}
