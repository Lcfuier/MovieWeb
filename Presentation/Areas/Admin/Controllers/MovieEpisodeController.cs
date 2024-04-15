using AutoMapper;
using DataAccess.IRepositories;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Constants;
using Model.DTOs;
using Model.Entitys;
using Model.Query;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class MovieEpisodeController : Controller
    {
        
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public MovieEpisodeController (IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(Guid? id)
        {
            UpdateEpisodeDTO vm = new()
            {
                MovieDetailDTO=new MovieDetailDTO(),
                movies= await _data.Movie.ListAllAsync(new QueryOptions<Movie>()
                {
                    Includes = "Actors,Genres"
                })
        };
            

            // for add
            if (id is null)
            {
                ViewBag.Action = "Add";
                return View(vm);
            }

            // for update
            ViewBag.Action = "Update";
            var movie = await _data.MovieDetail.GetAsync(id.GetValueOrDefault());
            
            if (movie is null)
            {
                return NotFound();
            }
            var movieDetailDTO = _mapper.Map<MovieDetailDTO>(movie);
            if (movieDetailDTO is null)
            {
                return NotFound();
            }
            vm.MovieDetailDTO = movieDetailDTO;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MovieDetailDTO movieDetailDTO)
        {
            var ExistEpisode = await _data.MovieDetail.GetByIdAsync(c => c.MovieDetailId.Equals(movieDetailDTO.MovieDetailId));
            var Episode = _mapper.Map<MovieDetail>(movieDetailDTO);
            if (ExistEpisode is null)
            {
                _data.MovieDetail.Add(Episode);
                await _data.SaveAsync();
                TempData["success"] = "Thêm thành công !";
            }
            else
            {

                _data.MovieDetail.Update(Episode);

                await _data.SaveAsync();
                TempData["success"] = "Cập nhật thành công !";
            }
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAllEpisode()
        {
            IEnumerable<MovieDetail> movies = await _data.MovieDetail.ListAllAsync(new QueryOptions<MovieDetail>()
            {
                Includes = "Movie"
            });
            return Json(new
            {
                data = movies.Select(b => new
                {
                    b.MovieDetailId,
                    b.MovieDetailName,
                    movieName=b.Movie.Title
                })
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEpisode(Guid id)
        {
            MovieDetail? movie = await _data.MovieDetail.GetAsync(id);
            if (movie is null)
            {
                return Json(new { success = false, message = "Không tìm thấy tập bạn muốn xóa." });
            }

            _data.MovieDetail.Remove(movie);
            await _data.SaveAsync();
            return Json(new { success = true, message = "Đã Xóa thành công." });
        }

        #endregion
    }
}
