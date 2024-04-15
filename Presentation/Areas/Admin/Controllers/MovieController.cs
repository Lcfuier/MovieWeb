using AutoMapper;
using DataAccess.Data;
using DataAccess.IRepositories;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Constants;
using Model.DTOs;
using Model.Entitys;
using Model.Query;
using System.Diagnostics.Eventing.Reader;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly MovieWebDbContext _db;
        public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment,IMapper mapper,MovieWebDbContext db)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? Id)
        {
            UpdateMovieDTO vm = new()
            {
                movieDto = new MovieDTO(),
                actors = await _unitOfWork.Actor.ListAllAsync(new QueryOptions<Actor>()
                {
                    Includes = "Movies",
                }),
                genres = await _unitOfWork.Genre.ListAllAsync(new QueryOptions<Genre>()
                {
                    Includes = "Movies",
                }),
            };
            if (Id is null)
            {
                ViewBag.Action = "Add";
                return View(vm);
            }
            ViewBag.Action = "Update";
            Movie? movie = await _unitOfWork.Movie.GetAsync(new QueryOptions<Movie>()
            {
                Where=c=>c.MovieId.Equals(Id),
                Includes = "Actors,Genres"
            });
            if (movie is null)
            {
                return NotFound();
            }
            MovieDTO? movieDto = _mapper.Map<MovieDTO>(movie);

            if (movieDto is null)
            {
                return NotFound();
            }
            vm.movieDto = movieDto;


            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MovieDTO movieDTO)
        {
            string WebRootPath = _hostEnvironment.WebRootPath;
            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                string pathUploads = Path.Combine(WebRootPath, @"assets\images");
                string fileExtension = Path.GetExtension(files[0].FileName);
                if (movieDTO.ImageURL is not null)
                {
                    // this is an update
                    string imagePath = Path.Combine(WebRootPath, movieDTO.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                using (FileStream fs = new(
                    Path.Combine(pathUploads, fileName + fileExtension), FileMode.Create))
                {
                    await files[0].CopyToAsync(fs);
                }

                movieDTO.ImageURL = fileName + fileExtension;
            }
            if (movieDTO.MovieId== Guid.Empty)
            {
                Movie movie = _mapper.Map<Movie>(movieDTO);
                await _unitOfWork.Movie.AddNewGenreAsync(movie, movieDTO.GenreIds, _unitOfWork, _db);
                await _unitOfWork.Movie.AddNewActorAsync(movie, movieDTO.ActorIds, _unitOfWork, _db);

                _unitOfWork.Movie.Add(movie);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Thêm thành công !";
            }
            else
            {
                var movie = await _unitOfWork.Movie.GetAsync(new QueryOptions<Movie>()
                {
                    Where = c => c.MovieId.Equals(movieDTO.MovieId),
                    Includes = "Genres,Actors",
                }) ;
                movie.Title=movieDTO.Title;
                movie.Description=movieDTO.Description;
                movie.Language=movieDTO.Language;
                movie.ReleaseYear=movieDTO.ReleaseYear;
                movie.Director=movieDTO.Director;
                movie.ImageURL = movieDTO.ImageURL;
                movie.PosterURL= movieDTO.PosterURL;
                movie.TrailerURL= movieDTO.TrailerURL;
                movie.quality=movieDTO.quality;
                movie.MPAA=movieDTO.MPAA;
                movie.CreateDate=movieDTO.CreateDate;
                movie.MovieDuration=movieDTO.MovieDuration;
                movie.Released=movieDTO.Released;
                movie.IsSeriesMovie=movieDTO.IsSeriesMovie;

                await _unitOfWork.Movie.AddNewGenreAsync(movie, movieDTO.GenreIds, _unitOfWork,_db);
                await _unitOfWork.Movie.AddNewActorAsync(movie, movieDTO.ActorIds, _unitOfWork,_db);

                _unitOfWork.Movie.Update(movie);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Cập nhật thành công !";
            }
            return RedirectToAction("Index");
        }
        
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAllMovie()
        {
            IEnumerable<Movie> movies = await _unitOfWork.Movie.ListAllAsync(new QueryOptions<Movie>()
            {
                Includes="Actors,Genres"
            });

            return Json(new
            {
                data = movies.Select(b => new
                {
                    b.MovieId,
                    b.Title,
                    Genres = b.Genres.Select(c=>c.GenreName),
                    Actors = b.Actors.Select(c => c.ActorName)
                })
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            Movie? movie = await _unitOfWork.Movie.GetAsync(id);
            if (movie is null)
            {
                return Json(new { success = false, message = "Lỗi khi xóa!" });
            }

            if (movie.ImageURL is not null)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, movie.ImageURL.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

             _unitOfWork.Movie.Remove(movie);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            Movie? movie = await _unitOfWork.Movie.GetAsync(id);
            if (movie is null)
            {
                return Json(new { success = false, message = "không tìm thấy phim!" });
            }

            if (movie.ImageURL is null)
            {
                return Json(new { success = false, message = "Không có ảnh!" });
            }

            string webRootPath = _hostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, movie.ImageURL.TrimStart('\\'));
            if (!System.IO.File.Exists(imagePath))
            {
                return Json(new { success = false, message = "Ảnh url Không còn tồn tại!" });
            }

            System.IO.File.Delete(imagePath);
            movie.ImageURL = null;
            _unitOfWork.Movie.Update(movie);


            return Json(new { success = true, message = "Xóa thành công!" });
        }

        #endregion
    }
}
