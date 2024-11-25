using Movies.APIs.Common;
using Movies.APIs.Dtos;

namespace Movies.APIs;

public interface IDirectorsService
{
    /// <summary>
    /// Create one Director
    /// </summary>
    public Task<Director> CreateDirector(DirectorCreateInput director);

    /// <summary>
    /// Delete one Director
    /// </summary>
    public Task DeleteDirector(DirectorWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Directors
    /// </summary>
    public Task<List<Director>> Directors(DirectorFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Director records
    /// </summary>
    public Task<MetadataDto> DirectorsMeta(DirectorFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Director
    /// </summary>
    public Task<Director> Director(DirectorWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Director
    /// </summary>
    public Task UpdateDirector(DirectorWhereUniqueInput uniqueId, DirectorUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Movies records to Director
    /// </summary>
    public Task ConnectMovies(DirectorWhereUniqueInput uniqueId, MovieWhereUniqueInput[] moviesId);

    /// <summary>
    /// Disconnect multiple Movies records from Director
    /// </summary>
    public Task DisconnectMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] moviesId
    );

    /// <summary>
    /// Find multiple Movies records for Director
    /// </summary>
    public Task<List<Movie>> FindMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieFindManyArgs MovieFindManyArgs
    );

    /// <summary>
    /// Update multiple Movies records for Director
    /// </summary>
    public Task UpdateMovies(DirectorWhereUniqueInput uniqueId, MovieWhereUniqueInput[] moviesId);
}
