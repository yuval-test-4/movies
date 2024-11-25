namespace Movies.APIs.Dtos;

public class ActorCreateInput
{
    public DateTime? BirthDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public List<Movie>? Movies { get; set; }

    public DateTime UpdatedAt { get; set; }
}
