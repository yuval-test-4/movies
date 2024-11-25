using Microsoft.AspNetCore.Mvc;
using Movies.APIs.Common;
using Movies.Infrastructure.Models;

namespace Movies.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class MovieFindManyArgs : FindManyInput<Movie, MovieWhereInput> { }
