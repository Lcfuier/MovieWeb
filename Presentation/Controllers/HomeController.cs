using DataAccess.Data;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Entitys;
using Model.Query;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _data;
        private readonly MovieWebDbContext _context;

        public HomeController(IUnitOfWork data, MovieWebDbContext context)
        {
            _data = data;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres, Actors,Ratings"
            });
            foreach (var movie in movies)
            {
                var listRating = await _data.Rating.ListAllAsync(new QueryOptions<Rating>()
                {
                    Includes = "Movie",
                    Where = c => c.MovieId.Equals(movie.MovieId)
                });
                if (listRating.Count() >0)
                {
                    movie.AverageStar = listRating.Average(c => c.Stars);
                }
                else
                {
                    movie.AverageStar = 0;
                }
            }
            HomeDisplayDTO vm = new HomeDisplayDTO()
            {
                movies = movies
            };   

            return View(vm);
        }
        public IActionResult Privacy()
        {

            return View();
        }

    }
}
