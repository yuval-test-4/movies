namespace Movies.APIs.Dtos;

public class DirectorUpdateInput
{
    public DateTime? BirthDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public List<string>? Movies { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
