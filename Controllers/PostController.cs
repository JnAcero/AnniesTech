
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnniesTech.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

         [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreatePost(Post post)
        {
            _unitOfWork.Posts.Add(post);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index","Home");
        }
    }
}