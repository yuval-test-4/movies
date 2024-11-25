using Microsoft.AspNetCore.Mvc;

namespace Movies.APIs;

[ApiController()]
public class DirectorsController : DirectorsControllerBase
{
    public DirectorsController(IDirectorsService service)
        : base(service) { }
}
