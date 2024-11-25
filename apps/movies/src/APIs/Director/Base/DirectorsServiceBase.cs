using Microsoft.EntityFrameworkCore;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;
using Movies.APIs.Extensions;
using Movies.Infrastructure;
using Movies.Infrastructure.Models;

namespace Movies.APIs;

public abstract class DirectorsServiceBase : IDirectorsService
{
    protected readonly MoviesDbContext _context;

    public DirectorsServiceBase(MoviesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Director
    /// </summary>
    public async Task<Director> CreateDirector(DirectorCreateInput createDto)
    {
        var director = new DirectorDbModel
        {
            BirthDate = createDto.BirthDate,
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            director.Id = createDto.Id;
        }
        if (createDto.Movies != null)
        {
            director.Movies = await _context
                .Movies.Where(movie => createDto.Movies.Select(t => t.Id).Contains(movie.Id))
                .ToListAsync();
        }

        _context.Directors.Add(director);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DirectorDbModel>(director.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Director
    /// </summary>
    public async Task DeleteDirector(DirectorWhereUniqueInput uniqueId)
    {
        var director = await _context.Directors.FindAsync(uniqueId.Id);
        if (director == null)
        {
            throw new NotFoundException();
        }

        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Directors
    /// </summary>
    public async Task<List<Director>> Directors(DirectorFindManyArgs findManyArgs)
    {
        var directors = await _context
            .Directors.Include(x => x.Movies)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return directors.ConvertAll(director => director.ToDto());
    }

    /// <summary>
    /// Meta data about Director records
    /// </summary>
    public async Task<MetadataDto> DirectorsMeta(DirectorFindManyArgs findManyArgs)
    {
        var count = await _context.Directors.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Director
    /// </summary>
    public async Task<Director> Director(DirectorWhereUniqueInput uniqueId)
    {
        var directors = await this.Directors(
            new DirectorFindManyArgs { Where = new DirectorWhereInput { Id = uniqueId.Id } }
        );
        var director = directors.FirstOrDefault();
        if (director == null)
        {
            throw new NotFoundException();
        }

        return director;
    }

    /// <summary>
    /// Update one Director
    /// </summary>
    public async Task UpdateDirector(
        DirectorWhereUniqueInput uniqueId,
        DirectorUpdateInput updateDto
    )
    {
        var director = updateDto.ToModel(uniqueId);

        if (updateDto.Movies != null)
        {
            director.Movies = await _context
                .Movies.Where(movie => updateDto.Movies.Select(t => t).Contains(movie.Id))
                .ToListAsync();
        }

        _context.Entry(director).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Directors.Any(e => e.Id == director.Id))
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
    /// Connect multiple Movies records to Director
    /// </summary>
    public async Task ConnectMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Directors.Include(x => x.Movies)
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
    /// Disconnect multiple Movies records from Director
    /// </summary>
    public async Task DisconnectMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Directors.Include(x => x.Movies)
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
    /// Find multiple Movies records for Director
    /// </summary>
    public async Task<List<Movie>> FindMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieFindManyArgs directorFindManyArgs
    )
    {
        var movies = await _context
            .Movies.Where(m => m.DirectorId == uniqueId.Id)
            .ApplyWhere(directorFindManyArgs.Where)
            .ApplySkip(directorFindManyArgs.Skip)
            .ApplyTake(directorFindManyArgs.Take)
            .ApplyOrderBy(directorFindManyArgs.SortBy)
            .ToListAsync();

        return movies.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Movies records for Director
    /// </summary>
    public async Task UpdateMovies(
        DirectorWhereUniqueInput uniqueId,
        MovieWhereUniqueInput[] childrenIds
    )
    {
        var director = await _context
            .Directors.Include(t => t.Movies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (director == null)
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

        director.Movies = children;
        await _context.SaveChangesAsync();
    }
}
