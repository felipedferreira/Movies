namespace Movies.Application.Titles.UpdateTitle;

public interface IUpdateTitleHandler
{
    Task<TitleDetailsDto> Handle(UpdateTitleCommand command, CancellationToken cancellationToken);
}