using DataAccess.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Constants;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _data;
        public HomeController(IUnitOfWork data)
        {
            _data = data;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
