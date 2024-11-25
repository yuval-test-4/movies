using Movies.APIs.Dtos;
using Movies.Infrastructure.Models;

namespace Movies.APIs.Extensions;

public static class MoviesExtensions
{
    public static Movie ToDto(this MovieDbModel model)
    {
        return new Movie
        {
            Actor = model.ActorId,
            Comment = model.Comment,
            CreatedAt = model.CreatedAt,
            Director = model.DirectorId,
            Id = model.Id,
            ReleaseDate = model.ReleaseDate,
            Reviews = model.Reviews?.Select(x => x.Id).ToList(),
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MovieDbModel ToModel(
        this MovieUpdateInput updateDto,
        MovieWhereUniqueInput uniqueId
    )
    {
        var movie = new MovieDbModel
        {
            Id = uniqueId.Id,
            Comment = updateDto.Comment,
            ReleaseDate = updateDto.ReleaseDate,
            Title = updateDto.Title
        };

        if (updateDto.Actor != null)
        {
            movie.ActorId = updateDto.Actor;
        }
        if (updateDto.CreatedAt != null)
        {
            movie.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Director != null)
        {
            movie.DirectorId = updateDto.Director;
        }
        if (updateDto.UpdatedAt != null)
        {
            movie.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return movie;
    }
}
