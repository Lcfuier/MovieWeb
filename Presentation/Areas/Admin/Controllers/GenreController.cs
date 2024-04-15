using AutoMapper;
using DataAccess.IRepositories;
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
    public class GenreController : Controller
    {
        
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public GenreController(IUnitOfWork data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            GenreDTO? genreDTO = new();

            // for add
            if (id is null)
            {
                ViewBag.Action = "Add";
                return View(genreDTO);
            }

            // for update
            ViewBag.Action = "Update";
            genreDTO = _mapper.Map<GenreDTO>(await _data.Genre.GetAsync(id.GetValueOrDefault()));
            if (genreDTO is null)
            {
                return NotFound();
            }
            return View(genreDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(GenreDTO genreDTO)
        {
            var ExistCategory = await _data.Genre.GetByIdAsync(c => c.GenreId.Equals(genreDTO.GenreId) );
            var genre = _mapper.Map<Genre>(genreDTO);
            if (ExistCategory is null)
            {
                 _data.Genre.Add(genre);
              await  _data.SaveAsync();
                TempData["success"] = "Thêm thành công !";
            }
            else
            {
                
                 _data.Genre.Update(genre);

               await _data.SaveAsync();
                TempData["success"] = "Cập nhật thành công !";
            }
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAllGenre()
        {
            return Json(new { data = await _data.Genre.ListAllAsync(new QueryOptions<Genre>()
            {
            })
        });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            Genre? genre = await _data.Genre.GetAsync(id);
            if (genre is null)
            {
                return Json(new { success = false, message = "Không tìm thấy thể loại bạn muốn xóa." });
            }

             _data.Genre.Remove(genre);
           await _data.SaveAsync();
            return Json(new { success = true, message = "Đã Xóa thành công." });
        }

        #endregion
    }
}
