using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Infrastructure.Models;

[Table("Directors")]
public class DirectorDbModel
{
    public DateTime? BirthDate { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public List<MovieDbModel>? Movies { get; set; } = new List<MovieDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
