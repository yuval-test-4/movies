using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure;

public class MoviesDbContext : IdentityDbContext<IdentityUser>
{
    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options) { }
}
