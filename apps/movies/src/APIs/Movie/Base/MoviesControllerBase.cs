using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;

namespace Movies.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MoviesControllerBase : ControllerBase
{
    protected readonly IMoviesService _service;

    public MoviesControllerBase(IMoviesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Movie
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Movie>> CreateMovie(MovieCreateInput input)
    {
        var movie = await _service.CreateMovie(input);

        return CreatedAtAction(nameof(Movie), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Delete one Movie
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteMovie([FromRoute()] MovieWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMovie(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Movies
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Movie>>> Movies([FromQuery()] MovieFindManyArgs filter)
    {
        return Ok(await _service.Movies(filter));
    }

    /// <summary>
    /// Meta data about Movie records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MoviesMeta([FromQuery()] MovieFindManyArgs filter)
    {
        return Ok(await _service.MoviesMeta(filter));
    }

    /// <summary>
    /// Get one Movie
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Movie>> Movie([FromRoute()] MovieWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Movie(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Movie
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMovie(
        [FromRoute()] MovieWhereUniqueInput uniqueId,
        [FromQuery()] MovieUpdateInput movieUpdateDto
    )
    {
        try
        {
            await _service.UpdateMovie(uniqueId, movieUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Actor record for Movie
    /// </summary>
    [HttpGet("{Id}/actor")]
    public async Task<ActionResult<List<Actor>>> GetActor(
        [FromRoute()] MovieWhereUniqueInput uniqueId
    )
    {
        var actor = await _service.GetActor(uniqueId);
        return Ok(actor);
    }

    /// <summary>
    /// Get a Director record for Movie
    /// </summary>
    [HttpGet("{Id}/director")]
    public async Task<ActionResult<List<Director>>> GetDirector(
        [FromRoute()] MovieWhereUniqueInput uniqueId
    )
    {
        var director = await _service.GetDirector(uniqueId);
        return Ok(director);
    }

    /// <summary>
    /// Connect multiple Reviews records to Movie
    /// </summary>
    [HttpPost("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectReviews(
        [FromRoute()] MovieWhereUniqueInput uniqueId,
        [FromQuery()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.ConnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from Movie
    /// </summary>
    [HttpDelete("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectReviews(
        [FromRoute()] MovieWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.DisconnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Reviews records for Movie
    /// </summary>
    [HttpGet("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Review>>> FindReviews(
        [FromRoute()] MovieWhereUniqueInput uniqueId,
        [FromQuery()] ReviewFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindReviews(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Reviews records for Movie
    /// </summary>
    [HttpPatch("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateReviews(
        [FromRoute()] MovieWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.UpdateReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
