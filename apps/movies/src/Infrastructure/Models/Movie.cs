using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Infrastructure.Models;

[Table("Movies")]
public class MovieDbModel
{
    public string? ActorId { get; set; }

    [ForeignKey(nameof(ActorId))]
    public ActorDbModel? Actor { get; set; } = null;

    [StringLength(1000)]
    public string? Comment { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? DirectorId { get; set; }

    [ForeignKey(nameof(DirectorId))]
    public DirectorDbModel? Director { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<ReviewDbModel>? Reviews { get; set; } = new List<ReviewDbModel>();

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
