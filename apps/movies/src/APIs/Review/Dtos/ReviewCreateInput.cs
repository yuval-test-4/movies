namespace Movies.APIs.Dtos;

public class ReviewCreateInput
{
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Movie? Movie { get; set; }

    public int? Rating { get; set; }

    public DateTime UpdatedAt { get; set; }
}
