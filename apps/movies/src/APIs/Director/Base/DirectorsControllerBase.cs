using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;

namespace Movies.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DirectorsControllerBase : ControllerBase
{
    protected readonly IDirectorsService _service;

    public DirectorsControllerBase(IDirectorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Director
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Director>> CreateDirector(DirectorCreateInput input)
    {
        var director = await _service.CreateDirector(input);

        return CreatedAtAction(nameof(Director), new { id = director.Id }, director);
    }

    /// <summary>
    /// Delete one Director
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteDirector([FromRoute()] DirectorWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDirector(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Directors
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Director>>> Directors(
        [FromQuery()] DirectorFindManyArgs filter
    )
    {
        return Ok(await _service.Directors(filter));
    }

    /// <summary>
    /// Meta data about Director records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DirectorsMeta(
        [FromQuery()] DirectorFindManyArgs filter
    )
    {
        return Ok(await _service.DirectorsMeta(filter));
    }

    /// <summary>
    /// Get one Director
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Director>> Director(
        [FromRoute()] DirectorWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Director(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Director
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateDirector(
        [FromRoute()] DirectorWhereUniqueInput uniqueId,
        [FromQuery()] DirectorUpdateInput directorUpdateDto
    )
    {
        try
        {
            await _service.UpdateDirector(uniqueId, directorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Movies records to Director
    /// </summary>
    [HttpPost("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectMovies(
        [FromRoute()] DirectorWhereUniqueInput uniqueId,
        [FromQuery()] MovieWhereUniqueInput[] moviesId
    )
    {
        try
        {
            await _service.ConnectMovies(uniqueId, moviesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Movies records from Director
    /// </summary>
    [HttpDelete("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectMovies(
        [FromRoute()] DirectorWhereUniqueInput uniqueId,
        [FromBody()] MovieWhereUniqueInput[] moviesId
    )
    {
        try
        {
            await _service.DisconnectMovies(uniqueId, moviesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Movies records for Director
    /// </summary>
    [HttpGet("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Movie>>> FindMovies(
        [FromRoute()] DirectorWhereUniqueInput uniqueId,
        [FromQuery()] MovieFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindMovies(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Movies records for Director
    /// </summary>
    [HttpPatch("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMovies(
        [FromRoute()] DirectorWhereUniqueInput uniqueId,
        [FromBody()] MovieWhereUniqueInput[] moviesId
    )
    {
        try
        {
            await _service.UpdateMovies(uniqueId, moviesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
