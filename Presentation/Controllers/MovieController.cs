using DataAccess.Data;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entitys;
using Model.Query;

namespace Presentation.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _data;
        public MovieController(IUnitOfWork data)
        {
            _data = data;

        }
        public async Task<IActionResult> Detail(Guid? id)
        {
            var movie = await _data.Movie.GetAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres,Actors,Ratings",
                Where= x=>x.MovieId.Equals(id)
            });
            var movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres,Actors,Ratings"
            });
            DetailViewDTO vm = new DetailViewDTO()
            {
                movie=movie,
                movies=movies
            };
            return View(vm);
        }
    }
}
