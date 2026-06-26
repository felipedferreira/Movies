using Movies.Application.Abstractions;
using Movies.Domain.TitleAggregate;

namespace Movies.WebService.UnitTests.TestDoubles;

internal sealed class InMemoryTitleRepository : ITitleRepository
{
    private readonly List<Title> _titles = [];

    public InMemoryTitleRepository(params Title[] titles)
    {
        _titles.AddRange(titles);
    }

    public int CreateCallCount { get; private set; }

    public int UpdateCallCount { get; private set; }

    public int DeleteCallCount { get; private set; }

    public bool UpdateResult { get; set; } = true;

    public bool DeleteResult { get; set; } = true;

    public Title? LastCreated { get; private set; }

    public Title? LastUpdated { get; private set; }

    public Task<IReadOnlyList<Title>> GetAllAsync(CancellationToken cancellationToken) =>
        Task.FromResult<IReadOnlyList<Title>>(_titles.ToList());

    public Task<Title?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Task.FromResult(_titles.FirstOrDefault(title => title.Id == id));

    public Task CreateAsync(Title title, CancellationToken cancellationToken)
    {
        CreateCallCount++;
        LastCreated = title;
        _titles.Add(title);

        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(Title title, CancellationToken cancellationToken)
    {
        UpdateCallCount++;
        LastUpdated = title;

        if (!UpdateResult)
        {
            return Task.FromResult(false);
        }

        _titles.RemoveAll(existing => existing.Id == title.Id);
        _titles.Add(title);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        DeleteCallCount++;

        return Task.FromResult(DeleteResult);
    }
}
