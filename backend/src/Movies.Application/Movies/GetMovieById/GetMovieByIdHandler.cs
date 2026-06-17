using Microsoft.Extensions.Logging;
using Movies.Application.Abstractions;
using Movies.Application.Exceptions;
using Movies.Domain;

namespace Movies.Application.Movies.GetMovieById;

internal sealed class GetMovieByIdHandler(
    IMovieRepository repository,
    ILogger<GetMovieByIdHandler> logger) : IGetMovieByIdHandler
{
    public async Task<MovieDto> Handle(GetMovieByIdQuery query, CancellationToken cancellationToken)
    {
        var movie = await repository.GetByIdAsync(query.Id, cancellationToken);

        if (movie is null)
        {
            logger.LogWarning("Movie {MovieId} was not found.", query.Id);
            throw new EntityNotFoundException(nameof(Movie), query.Id);
        }

        logger.LogInformation("Retrieved movie {MovieId}.", movie.Id);

        return movie.ToDto();
    }
}
