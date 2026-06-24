namespace Movies.Application.Titles.ListTitles;

public interface IListTitlesHandler
{
    Task<IReadOnlyList<TitleDto>> Handle(ListTitlesQuery query, CancellationToken cancellationToken);
}