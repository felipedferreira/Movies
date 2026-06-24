namespace Movies.Application.Titles.GetTitleById;

public interface IGetTitleByIdHandler
{
    Task<TitleDetailsDto> Handle(GetTitleByIdQuery query, CancellationToken cancellationToken);
}