using Microsoft.EntityFrameworkCore;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;
using Movies.APIs.Extensions;
using Movies.Infrastructure;
using Movies.Infrastructure.Models;

namespace Movies.APIs;

public abstract class MoviesServiceBase : IMoviesService
{
    protected readonly MoviesDbContext _context;

    public MoviesServiceBase(MoviesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Movie
    /// </summary>
    public async Task<Movie> CreateMovie(MovieCreateInput createDto)
    {
        var movie = new MovieDbModel
        {
            Comment = createDto.Comment,
            CreatedAt = createDto.CreatedAt,
            ReleaseDate = createDto.ReleaseDate,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            movie.Id = createDto.Id;
        }
        if (createDto.Actor != null)
        {
            movie.Actor = await _context
                .Actors.Where(actor => createDto.Actor.Id == actor.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Director != null)
        {
            movie.Director = await _context
                .Directors.Where(director => createDto.Director.Id == director.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Reviews != null)
        {
            movie.Reviews = await _context
                .Reviews.Where(review => createDto.Reviews.Select(t => t.Id).Contains(review.Id))
                .ToListAsync();
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MovieDbModel>(movie.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Movie
    /// </summary>
    public async Task DeleteMovie(MovieWhereUniqueInput uniqueId)
    {
        var movie = await _context.Movies.FindAsync(uniqueId.Id);
        if (movie == null)
        {
            throw new NotFoundException();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Movies
    /// </summary>
    public async Task<List<Movie>> Movies(MovieFindManyArgs findManyArgs)
    {
        var movies = await _context
            .Movies.Include(x => x.Actor)
            .Include(x => x.Reviews)
            .Include(x => x.Director)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return movies.ConvertAll(movie => movie.ToDto());
    }

    /// <summary>
    /// Meta data about Movie records
    /// </summary>
    public async Task<MetadataDto> MoviesMeta(MovieFindManyArgs findManyArgs)
    {
        var count = await _context.Movies.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Movie
    /// </summary>
    public async Task<Movie> Movie(MovieWhereUniqueInput uniqueId)
    {
        var movies = await this.Movies(
            new MovieFindManyArgs { Where = new MovieWhereInput { Id = uniqueId.Id } }
        );
        var movie = movies.FirstOrDefault();
        if (movie == null)
        {
            throw new NotFoundException();
        }

        return movie;
    }

    /// <summary>
    /// Update one Movie
    /// </summary>
    public async Task UpdateMovie(MovieWhereUniqueInput uniqueId, MovieUpdateInput updateDto)
    {
        var movie = updateDto.ToModel(uniqueId);

        if (updateDto.Actor != null)
        {
            movie.Actor = await _context
                .Actors.Where(actor => updateDto.Actor == actor.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Director != null)
        {
            movie.Director = await _context
                .Directors.Where(director => updateDto.Director == director.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Reviews != null)
        {
            movie.Reviews = await _context
                .Reviews.Where(review => updateDto.Reviews.Select(t => t).Contains(review.Id))
                .ToListAsync();
        }

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Movies.Any(e => e.Id == movie.Id))
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
    /// Get a Actor record for Movie
    /// </summary>
    public async Task<Actor> GetActor(MovieWhereUniqueInput uniqueId)
    {
        var movie = await _context
            .Movies.Where(movie => movie.Id == uniqueId.Id)
            .Include(movie => movie.Actor)
            .FirstOrDefaultAsync();
        if (movie == null)
        {
            throw new NotFoundException();
        }
        return movie.Actor.ToDto();
    }

    /// <summary>
    /// Get a Director record for Movie
    /// </summary>
    public async Task<Director> GetDirector(MovieWhereUniqueInput uniqueId)
    {
        var movie = await _context
            .Movies.Where(movie => movie.Id == uniqueId.Id)
            .Include(movie => movie.Director)
            .FirstOrDefaultAsync();
        if (movie == null)
        {
            throw new NotFoundException();
        }
        return movie.Director.ToDto();
    }

    /// <summary>
    /// Connect multiple Reviews records to Movie
    /// </summary>
    public async Task ConnectReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Movies.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reviews);

        foreach (var child in childrenToConnect)
        {
            parent.Reviews.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from Movie
    /// </summary>
    public async Task DisconnectReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Movies.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reviews?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reviews records for Movie
    /// </summary>
    public async Task<List<Review>> FindReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewFindManyArgs movieFindManyArgs
    )
    {
        var reviews = await _context
            .Reviews.Where(m => m.MovieId == uniqueId.Id)
            .ApplyWhere(movieFindManyArgs.Where)
            .ApplySkip(movieFindManyArgs.Skip)
            .ApplyTake(movieFindManyArgs.Take)
            .ApplyOrderBy(movieFindManyArgs.SortBy)
            .ToListAsync();

        return reviews.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reviews records for Movie
    /// </summary>
    public async Task UpdateReviews(
        MovieWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var movie = await _context
            .Movies.Include(t => t.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (movie == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        movie.Reviews = children;
        await _context.SaveChangesAsync();
    }

    public async Task<string> FreezeMovie(string data)
    {
        throw new NotImplementedException();
    }
}
