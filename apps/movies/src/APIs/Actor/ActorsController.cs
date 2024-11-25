using Microsoft.AspNetCore.Mvc;

namespace Movies.APIs;

[ApiController()]
public class ActorsController : ActorsControllerBase
{
    public ActorsController(IActorsService service)
        : base(service) { }
}
