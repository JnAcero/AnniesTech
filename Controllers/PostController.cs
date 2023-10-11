
using AnniesTech.Infrastructure.DTOs;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AnniesTech.Models.ViewModels;
using AnniesTech.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnniesTech.Controllers
{
    public class PostController : BaseController
    {
        private readonly PostService _postService;
        public PostController(IUnitOfWork unitOfWork, PostService postService, IMapper mapper) : base(unitOfWork, mapper)
        {
            _postService = new PostService(unitOfWork);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PostCreationDTO postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            _unitOfWork.Posts.Add(post);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            return View(post);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Post post)
        {
            _unitOfWork.Posts.Update(post);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            _unitOfWork.Posts.Remove(post);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = _unitOfWork.Posts.GetPostByIdWithAllComments(id);
            var allPosts = await _unitOfWork.Posts.GetAllAsync();
            var model = new PostDetallesViewModel
            {
                Post = post,
                ComentariosPrincipales = post.Comentarios.Where(c => c.ComentarioPadreId == null && c.ComentarioAbueloId == null).ToList(),
                ComentariosHijos = post.Comentarios.Where(c => c.ComentarioPadreId != null && c.ComentarioAbueloId == null).ToList(),
                ComentariosNietos = post.Comentarios.Where(c => c.ComentarioAbueloId != null).ToList(),
                PostsRecientes = allPosts.Take(10).ToList()
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> PublishComment(ComentarioCreationDTO comentarioDto)
        {
            try
            {
                if (string.IsNullOrEmpty(comentarioDto.Contenido))
                {
                    ViewBag.Error = "El comentario no puede estar vacio";
                    return RedirectToAction("Details", "Post", new { id = comentarioDto.PostId });
                }
                int? userId = null;
                var userIdClaim = User.FindFirst("IdUsuario");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int parsedIdUser)) userId = parsedIdUser;

                DateTime fechPublicacion = DateTime.UtcNow;
                var comment = _mapper.Map<Comentario>(comentarioDto);
                comment.UsuarioId = (int)userId;

                _unitOfWork.Comentarios.Add(comment);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Details", "Post", new { id = comentarioDto.PostId });

            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Details","Post",new {id = comentarioDto.PostId});
            }



        }


    }
}