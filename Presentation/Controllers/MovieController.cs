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
        private readonly MovieWebDbContext _db;
        public MovieController(IUnitOfWork data, MovieWebDbContext db)
        {
            _data = data;
            _db = db;
        }
        public async Task<IActionResult> Detail(Guid? id)
        {
            var movies = await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes = "Genres, Actors, Ratings"
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
            List<DisplayRating> ratings =new List<DisplayRating>();
            var users = _db.User.ToList();
            var movieee = (from a in movies
                        where a.MovieId.Equals(id)
                        select a).FirstOrDefault();
            if(movieee is null)
            {
                return NotFound();
            }
            if (users.Count() > 1)
            {
                foreach (var rate in movieee.Ratings)
                {
                    var rating = new DisplayRating();
                    var user = (from a in users
                                       where a.Id.Equals(rate.UserId)
                                       select a).FirstOrDefault();
                    if (user is null)
                        rating.fullName = "";
                    else
                        rating.fullName = (user.FirstName is null && user.LastName is null) ? user.UserName : user.FullName;
                    rating.rating = rate.Stars;
                    rating.Comment = rate.comment;
                    rating.RatingId=rate.RatingId;
                    ratings.Add(rating);
                }
            }
            DetailViewDTO vm = new DetailViewDTO()
            {
                movie = movieee,
                movies = movies,
                ratings = ratings
            };
            return View(vm);
        }
        public async Task<IActionResult> PlayMovie(Guid? Movieid, Guid? episodesId)
        {
            MoviePlayViewModel vm = new MoviePlayViewModel()
            {
                movies= await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
                {
                    Includes = "Genres,Actors,Ratings",
                })
            };
            IEnumerable<MovieDetail> Listmovie = new List<MovieDetail>();
            if (Movieid.HasValue)
            {
                Listmovie = await _data.MovieDetail.ListAllAsync(new QueryOptions<MovieDetail>()
                {
                    Includes = "Movie",
                    Where = x => x.MovieId.Equals(Movieid),
                    OrderBy = x => x.MovieDetailName

                });
            }
             
            if (Listmovie.Count() <= 0)
                return NotFound();
            vm.movieDetails = Listmovie;
            if (episodesId != null)
            {
                var movie = await _data.MovieDetail.GetAsync(new QueryOptions<MovieDetail>()
                {
                    Includes = "Movie",
                    Where = x => x.MovieDetailId.Equals(episodesId),

                });
                vm.movieDetail = movie;
                
            }
            else
            {
                vm.movieDetail = vm.movieDetails.FirstOrDefault();
            }
            
            return View(vm);
        }
    }
}
