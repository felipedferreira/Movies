using FastEndpoints;
using Movies.Application.Titles.GetTitleById;
using Movies.WebService.Contracts.Responses;

namespace Movies.WebService.Endpoints.Titles;

internal sealed class GetTitleByIdEndpoint(IGetTitleByIdHandler handler) : EndpointWithoutRequest<TitleDetailsResponse>
{
    public override void Configure()
    {
        Get("titles/{id:guid}");
        AllowAnonymous();
        Description(b => b.WithName("GetTitleById"));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");
        var title = await handler.Handle(new GetTitleByIdQuery(id), cancellationToken);
        await Send.OkAsync(title.ToResponse(), cancellationToken);
    }
}
