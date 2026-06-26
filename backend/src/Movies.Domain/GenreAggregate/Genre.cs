namespace Movies.Domain.GenreAggregate;

public class Genre
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public static Genre Create(string name)
    {
        return new Genre
        {
            Id = Guid.CreateVersion7(),
            Name = name,
        };
    }
}
