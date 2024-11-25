namespace Movies.APIs.Dtos;

public class MovieCreateInput
{
    public Actor? Actor { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public Director? Director { get; set; }

    public string? Id { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<Review>? Reviews { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
