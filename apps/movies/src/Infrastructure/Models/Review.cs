using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Infrastructure.Models;

[Table("Reviews")]
public class ReviewDbModel
{
    [StringLength(1000)]
    public string? Comment { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public MovieDbModel? Movie { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Rating { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
