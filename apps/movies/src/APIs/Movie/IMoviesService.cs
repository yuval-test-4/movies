using Movies.APIs.Common;
using Movies.APIs.Dtos;

namespace Movies.APIs;

public interface IMoviesService
{
    /// <summary>
    /// Create one Movie
    /// </summary>
    public Task<Movie> CreateMovie(MovieCreateInput movie);

    /// <summary>
    /// Delete one Movie
    /// </summary>
    public Task DeleteMovie(MovieWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Movies
    /// </summary>
    public Task<List<Movie>> Movies(MovieFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Movie records
    /// </summary>
    public Task<MetadataDto> MoviesMeta(MovieFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Movie
    /// </summary>
    public Task<Movie> Movie(MovieWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Movie
    /// </summary>
    public Task UpdateMovie(MovieWhereUniqueInput uniqueId, MovieUpdateInput updateDto);

    /// <summary>
    /// Get a Actor record for Movie
    /// </summary>
    public Task<Actor> GetActor(MovieWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Director record for Movie
    /// </summary>
    public Task<Director> GetDirector(MovieWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Reviews records to Movie
    /// </summary>
    public Task ConnectReviews(MovieWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);

    /// <summary>
    /// Disconnect multiple Reviews records from Movie
    /// </summary>
    public Task DisconnectReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Find multiple Reviews records for Movie
    /// </summary>
    public Task<List<Review>> FindReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewFindManyArgs ReviewFindManyArgs
    );

    /// <summary>
    /// Update multiple Reviews records for Movie
    /// </summary>
    public Task UpdateReviews(MovieWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);
}
