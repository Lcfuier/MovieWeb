using DataAccess.Data;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Constants;
using Model.Entitys;
using System.Net;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class RatingController : Controller
    {
        private readonly IUnitOfWork _data;
        private readonly MovieWebDbContext _db;
        public RatingController(IUnitOfWork data, MovieWebDbContext db)
        {
            _data = data;
            _db = db;
        }
        [Authorize]
        public async Task<IActionResult> PostRating(int stars,string? comment,Guid MovieId)
        {
            ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _db.User.FindAsync(claim?.Value)
                ?? throw new Exception("User not found.");
            
            var ratings = new Rating()
            {
                MovieId = MovieId,
                Stars = stars,
                CreateDate = DateTime.Now,
                UserId=user.Id
            };
            if (comment is null)
            {
                ratings.comment = "";
             }
            else
                ratings.comment = comment;
            _data.Rating.Add(ratings);
            await _data.SaveAsync();
            return RedirectToAction("Detail","Movie",new { id=MovieId });
        }
        public async Task<IActionResult> DeleteRating(Guid id,Guid MovieId)
        {
            ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _db.User.FindAsync(claim?.Value)
                ?? throw new Exception("User not found.");
            var rating= await _data.Rating.GetAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            if (rating.UserId.Equals(user.Id)||user.Role.Equals(Roles.Admin))
            {
                _data.Rating.Remove(rating);
               await _data.SaveAsync();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Detail", "Movie", new { id = MovieId });
        }
    }
}
