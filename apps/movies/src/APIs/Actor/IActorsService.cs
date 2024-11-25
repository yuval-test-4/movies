using Movies.APIs.Common;
using Movies.APIs.Dtos;

namespace Movies.APIs;

public interface IActorsService
{
    /// <summary>
    /// Create one Actor
    /// </summary>
    public Task<Actor> CreateActor(ActorCreateInput actor);

    /// <summary>
    /// Delete one Actor
    /// </summary>
    public Task DeleteActor(ActorWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Actors
    /// </summary>
    public Task<List<Actor>> Actors(ActorFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Actor records
    /// </summary>
    public Task<MetadataDto> ActorsMeta(ActorFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Actor
    /// </summary>
    public Task<Actor> Actor(ActorWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Actor
    /// </summary>
    public Task UpdateActor(ActorWhereUniqueInput uniqueId, ActorUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Movies records to Actor
    /// </summary>
    public Task ConnectMovies(ActorWhereUniqueInput uniqueId, MovieWhereUniqueInput[] moviesId);

    /// <summary>
    /// Disconnect multiple Movies records from Actor
    /// </summary>
    public Task DisconnectMovies(ActorWhereUniqueInput uniqueId, MovieWhereUniqueInput[] moviesId);

    /// <summary>
    /// Find multiple Movies records for Actor
    /// </summary>
    public Task<List<Movie>> FindMovies(
        ActorWhereUniqueInput uniqueId,
        MovieFindManyArgs MovieFindManyArgs
    );

    /// <summary>
    /// Update multiple Movies records for Actor
    /// </summary>
    public Task UpdateMovies(ActorWhereUniqueInput uniqueId, MovieWhereUniqueInput[] moviesId);
}
