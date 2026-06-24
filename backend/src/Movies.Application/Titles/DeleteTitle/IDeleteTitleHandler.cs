namespace Movies.Application.Titles.DeleteTitle;

public interface IDeleteTitleHandler
{
    Task Handle(DeleteTitleCommand command, CancellationToken cancellationToken);
}