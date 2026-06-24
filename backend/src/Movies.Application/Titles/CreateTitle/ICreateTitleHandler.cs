namespace Movies.Application.Titles.CreateTitle;

public interface ICreateTitleHandler
{
    Task<TitleDetailsDto> Handle(CreateTitleCommand command, CancellationToken cancellationToken);
}