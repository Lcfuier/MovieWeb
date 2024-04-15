using DataAccess.Data;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task <IActionResult> SeriesMovie()
        {
            var movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres, Actors,Ratings",
                Where =c=>c.IsSeriesMovie

            });
            foreach (var movie in movies)
            {
                var listRating = await _data.Rating.ListAllAsync(new QueryOptions<Rating>()
                {
                    Includes = "Movie",
                    Where = c => c.MovieId.Equals(movie.MovieId)
                });
                if (listRating.Count() > 0)
                {
                    movie.AverageStar = listRating.Average(c => c.Stars);
                }
                else
                {
                    movie.AverageStar = 0;
                }
            }
            movies=movies.OrderByDescending(x => x.AverageStar);
            HomeDisplayDTO vm = new HomeDisplayDTO()
            {
                movies = movies
            };
            return View(vm);
        }
        public async Task<IActionResult> SingleMovie()
        {
            var movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres, Actors,Ratings",
                Where = c => !c.IsSeriesMovie

            });
            foreach (var movie in movies)
            {
                var listRating = await _data.Rating.ListAllAsync(new QueryOptions<Rating>()
                {
                    Includes = "Movie",
                    Where = c => c.MovieId.Equals(movie.MovieId)
                });
                if (listRating.Count() > 0)
                {
                    movie.AverageStar = listRating.Average(c => c.Stars);
                }
                else
                {
                    movie.AverageStar = 0;
                }
            }
            movies=movies.OrderByDescending(x => x.AverageStar);
            HomeDisplayDTO vm = new HomeDisplayDTO()
            {
                movies = movies
            };
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> ListAllMovie(string? SearchString,int page=1)
        {
            IEnumerable<Movie> movies=new List<Movie>();
            if (SearchString is not null)
            {
                movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
                {
                    Includes = "Genres, Actors,Ratings",
                    Where = c => c.Title.Contains(SearchString)

                });
            }
            else
            {
                movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
                {
                    Includes = "Genres, Actors,Ratings",

                });
            }
            foreach (var movie in movies)
            {
                var listRating = await _data.Rating.ListAllAsync(new QueryOptions<Rating>()
                {
                    Includes = "Movie",
                    Where = c => c.MovieId.Equals(movie.MovieId)
                });
                if (listRating.Count() > 0)
                {
                    movie.AverageStar = listRating.Average(c => c.Stars);
                }
                else
                {
                    movie.AverageStar = 0;
                }
            }
            movies = movies.OrderByDescending(x => x.AverageStar);
            const int pageSize = 10;
            if (page < 1)
            {
                page = 1;
            }
            int recsCount=movies.Count();
            var pager = new Pager(recsCount,page,pageSize);
            int recSkip = (page - 1) * pageSize;
            var data=movies.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager=pager;
            HomeDisplayDTO vm = new HomeDisplayDTO()
            {
                movies = data
            };
            return View(vm);
        }
        public IActionResult Privacy()
        {

            return View();
        }

    }
}
