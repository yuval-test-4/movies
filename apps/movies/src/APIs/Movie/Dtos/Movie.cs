namespace Movies.APIs.Dtos;

public class Movie
{
    public string? Actor { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Director { get; set; }

    public string Id { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<string>? Reviews { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
