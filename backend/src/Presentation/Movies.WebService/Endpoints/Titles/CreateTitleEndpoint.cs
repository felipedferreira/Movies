using FastEndpoints;
using Movies.Application.Titles.CreateTitle;
using Movies.WebService.Contracts.Requests;

namespace Movies.WebService.Endpoints.Titles;

internal sealed class CreateTitleEndpoint(ICreateTitleHandler handler) : Endpoint<CreateTitlesRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("titles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTitlesRequest request, CancellationToken cancellationToken)
    {
        var title = await handler.Handle(request.ToCommand(), cancellationToken);

        await Send.CreatedAtAsync("GetTitleById", new { id = title.Id }, default!, cancellation: cancellationToken);
    }
}
