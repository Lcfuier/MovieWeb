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
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public ActorController(IUnitOfWork data, IMapper mapper)
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
            ActorDTO? actorDTO = new();

            // for add
            if (id is null)
            {
                ViewBag.Action = "Add";
                return View(actorDTO);
            }

            // for update
            ViewBag.Action = "Update";
            actorDTO = _mapper.Map<ActorDTO>(await _data.Actor.GetAsync(id.GetValueOrDefault()));
            if (actorDTO is null)
            {
                return NotFound();
            }
            return View(actorDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ActorDTO actorDTO)
        {
            var ExistCategory = await _data.Actor.GetByIdAsync(c => c.ActorId.Equals(actorDTO.ActorId));
            var actor = _mapper.Map<Actor>(actorDTO);
            if (ExistCategory is null)
            {
                _data.Actor.Add(actor);
                await _data.SaveAsync();
                TempData["success"] = "Thêm thành công !";
            }
            else
            {

                _data.Actor.Update(actor);

                await _data.SaveAsync();
                TempData["success"] = "Cập nhật thành công !";
            }
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAllActor()
        {
            return Json(new
            {
                data = await _data.Actor.ListAllAsync(new QueryOptions<Actor>()
                {
                })
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            Actor? actor = await _data.Actor.GetAsync(id);
            if (actor is null)
            {
                return Json(new { success = false, message = "Không tìm thấy diễn viên bạn muốn xóa." });
            }

            _data.Actor.Remove(actor);
            await _data.SaveAsync();
            return Json(new { success = true, message = "Đã Xóa thành công." });
        }

        #endregion
    }
}
