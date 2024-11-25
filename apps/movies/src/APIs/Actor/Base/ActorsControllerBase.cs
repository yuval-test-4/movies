using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.APIs;
using Movies.APIs.Common;
using Movies.APIs.Dtos;
using Movies.APIs.Errors;

namespace Movies.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ActorsControllerBase : ControllerBase
{
    protected readonly IActorsService _service;

    public ActorsControllerBase(IActorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Actor
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Actor>> CreateActor(ActorCreateInput input)
    {
        var actor = await _service.CreateActor(input);

        return CreatedAtAction(nameof(Actor), new { id = actor.Id }, actor);
    }

    /// <summary>
    /// Delete one Actor
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteActor([FromRoute()] ActorWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteActor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Actors
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Actor>>> Actors([FromQuery()] ActorFindManyArgs filter)
    {
        return Ok(await _service.Actors(filter));
    }

    /// <summary>
    /// Meta data about Actor records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ActorsMeta([FromQuery()] ActorFindManyArgs filter)
    {
        return Ok(await _service.ActorsMeta(filter));
    }

    /// <summary>
    /// Get one Actor
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Actor>> Actor([FromRoute()] ActorWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Actor(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Actor
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateActor(
        [FromRoute()] ActorWhereUniqueInput uniqueId,
        [FromQuery()] ActorUpdateInput actorUpdateDto
    )
    {
        try
        {
            await _service.UpdateActor(uniqueId, actorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Movies records to Actor
    /// </summary>
    [HttpPost("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectMovies(
        [FromRoute()] ActorWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Movies records from Actor
    /// </summary>
    [HttpDelete("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectMovies(
        [FromRoute()] ActorWhereUniqueInput uniqueId,
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
    /// Find multiple Movies records for Actor
    /// </summary>
    [HttpGet("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Movie>>> FindMovies(
        [FromRoute()] ActorWhereUniqueInput uniqueId,
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
    /// Update multiple Movies records for Actor
    /// </summary>
    [HttpPatch("{Id}/movies")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateMovies(
        [FromRoute()] ActorWhereUniqueInput uniqueId,
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
