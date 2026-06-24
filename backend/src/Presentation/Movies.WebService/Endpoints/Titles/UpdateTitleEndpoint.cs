using FastEndpoints;
using Movies.Application.Titles.UpdateTitle;
using Movies.WebService.Contracts.Requests;

namespace Movies.WebService.Endpoints.Titles;

internal sealed class UpdateTitleEndpoint(IUpdateTitleHandler handler) : Endpoint<UpdateTitlesRequest>
{
    public override void Configure()
    {
        Put("titles/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateTitlesRequest request, CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");
        await handler.Handle(request.ToCommand(id), cancellationToken);
        await Send.AcceptedAtAsync("GetTitleById", new { id }, cancellation: cancellationToken);
    }
}
