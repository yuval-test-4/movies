using Movies.APIs.Dtos;
using Movies.Infrastructure.Models;

namespace Movies.APIs.Extensions;

public static class ReviewsExtensions
{
    public static Review ToDto(this ReviewDbModel model)
    {
        return new Review
        {
            Comment = model.Comment,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Movie = model.MovieId,
            Rating = model.Rating,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReviewDbModel ToModel(
        this ReviewUpdateInput updateDto,
        ReviewWhereUniqueInput uniqueId
    )
    {
        var review = new ReviewDbModel
        {
            Id = uniqueId.Id,
            Comment = updateDto.Comment,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            review.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Movie != null)
        {
            review.MovieId = updateDto.Movie;
        }
        if (updateDto.UpdatedAt != null)
        {
            review.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return review;
    }
}
