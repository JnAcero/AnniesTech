using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AnniesTech.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnniesTech.Controllers
{
    //[Route("[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly UserService _userService;
        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userService = new UserService(unitOfWork,mapper);

        }

        [Authorize]
        public async Task<ActionResult> Perfil()
        {
            int userId = 0;
            var userIdClaim = User.FindFirst("IdUsuario");
            if(userIdClaim is not null && int.TryParse(userIdClaim.Value, out int parsedUserId))
                userId = parsedUserId;
            
            Usuario usuario = await _unitOfWork.Users.FindFirst(u =>u.Id == userId);

            return View(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> ActualizarPerfil(Usuario model)
        {
            await _userService.ActualizarPerfil(model);

            return RedirectToAction("Perfil");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarCuenta()
        {
            int userId = 0;
            var userIdClaim = User.FindFirst("IdUsuario");
            if(userIdClaim is not null && int.TryParse(userIdClaim.Value, out int parsedUserId))
                userId = parsedUserId;
            
            Usuario usuario = await _unitOfWork.Users.GetByIdAsync(userId);
            _unitOfWork.Users.Remove(usuario);
            await _unitOfWork.SaveAsync();
 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home"); 
        }
    }
}