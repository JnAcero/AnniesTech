
using System.Security.Claims;
using AnniesTech.Infrastructure.DTOs;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AnniesTech.Models.ViewModels;
using AnniesTech.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnniesTech.Controllers
{
    public class CuentaController : BaseController
    {
        private readonly UserService _userService;
        public CuentaController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userService = new UserService(unitOfWork, mapper);
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Registrar(UsuarioCreationDTO userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existUser = _unitOfWork.Users.ExistUserByUsername(userDto.UserName);
                    if (existUser) throw new Exception("El usuario ya existe en la base de datos");

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                    DateTime fechaExpiracion = DateTime.UtcNow.AddMinutes(5);
                    int defaultRolId = 2;
                    var token = Guid.NewGuid().ToString();

                    userDto.Password = hashedPassword;
                    userDto.ExpirationDate = fechaExpiracion;
                    userDto.IsActive = false;
                    userDto.RolId = defaultRolId;
                    userDto.Token = token;

                    var usuario = _mapper.Map<Usuario>(userDto);
                    //PENDIENTE PROGRAMAR ENVIO POR CORREO 

                    return RedirectToAction("Token");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View(userDto);
        }

        public async Task<IActionResult> Token()
        {
            string Token = Request.Query["valor"];
            if (Token is not null)
            {
                try
                {
                    DateTime fechaActual = DateTime.UtcNow;
                    var result = await _userService.ActivarUsuario(Token);
                    if (result == 1)
                    {
                        ViewData["mensaje"] = "Su cuenta ha sido activada";
                    }
                    else ViewData["mensaje"] = "Su enlace de activacion ha expirado";
                }
                catch(Exception ex)
                {
                    ViewData["mensaje"] = ex.Message;
                    return View();
                }
            }
            else
            {
                 ViewData["mensaje"] = "Verifique su correo para activar su cuenta";
                 return View();
            }
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if(c.Identity is not null)
            {
                if(c.Identity.IsAuthenticated)
                    return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if(ModelState.IsValid)
                return View(viewModel);
            try
            {
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}