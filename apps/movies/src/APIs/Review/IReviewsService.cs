using Movies.APIs.Common;
using Movies.APIs.Dtos;

namespace Movies.APIs;

public interface IReviewsService
{
    /// <summary>
    /// Create one Review
    /// </summary>
    public Task<Review> CreateReview(ReviewCreateInput review);

    /// <summary>
    /// Delete one Review
    /// </summary>
    public Task DeleteReview(ReviewWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Reviews
    /// </summary>
    public Task<List<Review>> Reviews(ReviewFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Review records
    /// </summary>
    public Task<MetadataDto> ReviewsMeta(ReviewFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Review
    /// </summary>
    public Task<Review> Review(ReviewWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Review
    /// </summary>
    public Task UpdateReview(ReviewWhereUniqueInput uniqueId, ReviewUpdateInput updateDto);

    /// <summary>
    /// Get a Movie record for Review
    /// </summary>
    public Task<Movie> GetMovie(ReviewWhereUniqueInput uniqueId);
}
