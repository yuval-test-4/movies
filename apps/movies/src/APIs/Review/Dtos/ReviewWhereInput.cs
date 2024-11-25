namespace Movies.APIs.Dtos;

public class ReviewWhereInput
{
    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Movie { get; set; }

    public int? Rating { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
