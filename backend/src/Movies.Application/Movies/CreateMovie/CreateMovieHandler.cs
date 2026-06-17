using FluentValidation;
using Microsoft.Extensions.Logging;
using Movies.Application.Abstractions;
using Movies.Domain;

namespace Movies.Application.Movies.CreateMovie;

internal sealed class CreateMovieHandler(
    IMovieRepository repository,
    IValidator<CreateMovieCommand> validator,
    ILogger<CreateMovieHandler> logger) : ICreateMovieHandler
{
    public async Task<MovieDto> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating movie {Title} ({YearOfRelease}).", command.Title, command.YearOfRelease);

        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var movie = new Movie
        {
            Title = command.Title,
            YearOfRelease = command.YearOfRelease,
            Description = command.Description,
        };

        var created = await repository.CreateAsync(movie, cancellationToken);

        logger.LogInformation("Created movie {MovieId} ({Title}).", created.Id, created.Title);

        return created.ToDto();
    }
}
