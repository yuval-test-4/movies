using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Models;

namespace Movies.Infrastructure;

public class MoviesDbContext : IdentityDbContext<IdentityUser>
{
    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options) { }

    public DbSet<MovieDbModel> Movies { get; set; }

    public DbSet<ActorDbModel> Actors { get; set; }

    public DbSet<ReviewDbModel> Reviews { get; set; }

    public DbSet<DirectorDbModel> Directors { get; set; }
}
