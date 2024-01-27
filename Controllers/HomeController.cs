
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using  AnniesTech.Models;
using X.PagedList;
namespace AnniesTech.Controllers
{
    public class HomeController : BaseController
    {
        private readonly PostService _postService;
        public HomeController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _postService = new PostService(unitOfWork);
        }

        public async Task<IActionResult> Index(string categoria, string buscar, int? pagina)
        {
            List<Post> posts = new(); 
            if(string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(buscar))
            {
                posts = await _postService.ObtenerPosts();
            }
            else if(!string.IsNullOrEmpty(categoria))
            {
                posts = await _unitOfWork.Posts.PostPorCategoria(categoria); 

                if(posts.Count == 0) 
                    ViewBag.Error = $"No se encontraron publicaciones en la categoria {categoria}";
            }
            else if(!string.IsNullOrEmpty(buscar))
            {
                posts =  await _unitOfWork.Posts.Find(p =>p.Titulo.ToLower() ==  buscar.ToLower());
                if(posts.Count == 0) 
                    ViewBag.Error = $"No se encontraron publicaciones con la descripcion:{buscar}";
            }
            int pageSize = 6;
            int pageNumber = pagina ?? 1;

            string descripcionCategoria = !string.IsNullOrEmpty(categoria) ? _unitOfWork.Categorias.ObtenerDescripcion(categoria) : "todas las categorias";

            ViewBag.CategoririaDescripcion = descripcionCategoria.ToLower();

            return View(posts.ToPagedList(pageNumber,pageSize));

        }
    }
}