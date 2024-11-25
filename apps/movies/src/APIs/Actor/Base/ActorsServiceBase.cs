using Microsoft.EntityFrameworkCore;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;
using Movies.APIs.Extensions;
using Movies.Infrastructure;
using Movies.Infrastructure.Models;

namespace Movies.APIs;

public abstract class ActorsServiceBase : IActorsService
{
    protected readonly MoviesDbContext _context;

    public ActorsServiceBase(MoviesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Actor
    /// </summary>
    public async Task<Actor> CreateActor(ActorCreateInput createDto)
    {
        var actor = new ActorDbModel
        {
            BirthDate = createDto.BirthDate,
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            actor.Id = createDto.Id;
        }
        if (createDto.Movies != null)
        {
            actor.Movies = await _context
                .Movies.Where(movie => createDto.Movies.Select(t => t.Id).Contains(movie.Id))
                .ToListAsync();
        }

        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ActorDbModel>(actor.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Actor
    /// </summary>
    public async Task DeleteActor(ActorWhereUniqueInput uniqueId)
    {
        var actor = await _context.Actors.FindAsync(uniqueId.Id);
        if (actor == null)
        {
            throw new NotFoundException();
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Actors
    /// </summary>
    public async Task<List<Actor>> Actors(ActorFindManyArgs findManyArgs)
    {
        var actors = await _context
            .Actors.Include(x => x.Movies)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return actors.ConvertAll(actor => actor.ToDto());
    }

    /// <summary>
    /// Meta data about Actor records
    /// </summary>
    public async Task<MetadataDto> ActorsMeta(ActorFindManyArgs findManyArgs)
    {
        var count = await _context.Actors.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Actor
    /// </summary>
    public async Task<Actor> Actor(ActorWhereUniqueInput uniqueId)
    {
        var actors = await this.Actors(
            new ActorFindManyArgs { Where = new ActorWhereInput { Id = uniqueId.Id } }
        );
        var actor = actors.FirstOrDefault();
        if (actor == null)
        {
            throw new NotFoundException();
        }

        return actor;
    }

    /// <summary>
    /// Update one Actor
    /// </summary>
    public async Task UpdateActor(ActorWhereUniqueInput uniqueId, ActorUpdateInput updateDto)
    {
        var actor = updateDto.ToModel(uniqueId);

        if (updateDto.Movies != null)
        {
            actor.Movies = await _context
                .Movies.Where(movie => updateDto.Movies.Select(t => t).Contains(movie.Id))
                .ToListAsync();
        }

        _context.Entry(actor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Actors.Any(e => e.Id == actor.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Movies records to Actor
    /// </summary>
    public async Task ConnectMovies(
        ActorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Actors.Include(x => x.Movies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Movies.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Movies);

        foreach (var child in childrenToConnect)
        {
            parent.Movies.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Movies records from Actor
    /// </summary>
    public async Task DisconnectMovies(
        ActorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Actors.Include(x => x.Movies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Movies.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Movies?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Movies records for Actor
    /// </summary>
    public async Task<List<Movie>> FindMovies(
        ActorWhereUniqueInput uniqueId,
        MovieFindManyArgs actorFindManyArgs
    )
    {
        var movies = await _context
            .Movies.Where(m => m.ActorId == uniqueId.Id)
            .ApplyWhere(actorFindManyArgs.Where)
            .ApplySkip(actorFindManyArgs.Skip)
            .ApplyTake(actorFindManyArgs.Take)
            .ApplyOrderBy(actorFindManyArgs.SortBy)
            .ToListAsync();

        return movies.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Movies records for Actor
    /// </summary>
    public async Task UpdateMovies(
        ActorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var actor = await _context
            .Actors.Include(t => t.Movies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (actor == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Movies.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        actor.Movies = children;
        await _context.SaveChangesAsync();
    }
}
