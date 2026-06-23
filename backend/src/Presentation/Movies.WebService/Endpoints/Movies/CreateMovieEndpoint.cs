using FastEndpoints;
using Movies.Application.Movies.CreateMovie;
using Movies.WebService.Contracts.Requests;

namespace Movies.WebService.Endpoints.Movies;

internal sealed class CreateMovieEndpoint(ICreateMovieHandler handler) : Endpoint<CreateMoviesRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMoviesRequest request, CancellationToken cancellationToken)
    {
        var movie = await handler.Handle(request.ToCommand(), cancellationToken);

        Response = new EmptyResponse();
        HttpContext.Response.StatusCode = 201;
        HttpContext.Response.Headers.Location = $"/movies/{movie.Id}";
    }
}