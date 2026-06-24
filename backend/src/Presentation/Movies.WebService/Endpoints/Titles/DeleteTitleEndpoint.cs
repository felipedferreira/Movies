using FastEndpoints;
using Movies.Application.Titles.DeleteTitle;

namespace Movies.WebService.Endpoints.Titles;

internal sealed class DeleteTitleEndpoint(IDeleteTitleHandler handler) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("titles/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");
        await handler.Handle(new DeleteTitleCommand(id), cancellationToken);
        await Send.NoContentAsync(cancellationToken);
    }
}