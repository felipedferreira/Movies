using FastEndpoints;
using Movies.Application.Titles.ListTitles;
using Movies.WebService.Contracts.Responses;

namespace Movies.WebService.Endpoints.Titles;

internal sealed class GetAllTitlesEndpoint(IListTitlesHandler handler) : EndpointWithoutRequest<TitlesResponse>
{
    public override void Configure()
    {
        Get("titles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var titles = await handler.Handle(new ListTitlesQuery(), cancellationToken);

        await Send.OkAsync(titles.ToResponse(), cancellationToken);
    }
}