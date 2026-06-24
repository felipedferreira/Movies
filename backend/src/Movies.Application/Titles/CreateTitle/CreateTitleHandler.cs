using FluentValidation;
using Microsoft.Extensions.Logging;
using Movies.Application.Abstractions;
using Movies.Domain.TitleAggregate;

namespace Movies.Application.Titles.CreateTitle;

internal sealed class CreateTitleHandler(
    ITitleRepository repository,
    IGenreRepository genreRepository,
    IValidator<CreateTitleCommand> validator,
    ILogger<CreateTitleHandler> logger) : ICreateTitleHandler
{
    public async Task<TitleDetailsDto> Handle(CreateTitleCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating title {Title} ({YearOfRelease}).", command.Title, command.YearOfRelease);

        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var genres = await genreRepository.GetByIdsAsync(command.GenreIds, cancellationToken);
        GenreLinking.EnsureAllExist(command.GenreIds, genres);

        var title = new Title
        {
            Name = command.Title,
            Type = command.Type,
            YearOfRelease = command.YearOfRelease,
            Description = command.Description,
            GenreIds = command.GenreIds.Distinct().ToList(),
        };

        var created = await repository.CreateAsync(title, cancellationToken);

        logger.LogInformation("Created title {TitleId} ({Title}).", created.Id, created.Name);

        return created.ToDetailsDto(genres);
    }
}
