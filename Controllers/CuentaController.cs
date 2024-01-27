
using System.Security.Claims;
using AnniesTech.Infrastructure.DTOs;
using AnniesTech.Infrastructure.Helpers;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AnniesTech.Models.ViewModels;
using AnniesTech.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Registrar(Usuario userDto)
        {
            //if (ModelState.IsValid)
            //{
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

                    _unitOfWork.Users.Add(userDto);
                    await _unitOfWork.SaveAsync();
                
                    Email email = new();
                    if(userDto.Email is not null) email.Enviar(userDto.Email, token);

                    return RedirectToAction("Token");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            //}
            ViewBag.Error = "Por aca";
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
                catch (Exception ex)
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
            if (c.Identity is not null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid){
                 var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(viewModel);
            }
                
            try
            {
                var usuario = await _unitOfWork.Users.FindFirst(u => u.Email == viewModel.Email);
                bool passwordMatch = BCrypt.Net.BCrypt.Verify(viewModel.Password, usuario.Password.ToString());
 
                if (passwordMatch)
                {
                    DateTime fechaExpiracion = DateTime.UtcNow;

                        if (!usuario.IsActive && usuario.ExpirationDate.ToString() != fechaExpiracion.ToString())
                        {
                            if (viewModel.Email is not null)
                            {
                                _userService.ActualizarToken(viewModel.Email);

                                ViewBag.Error = " 1 Su cuenta no ha sido activada. Se ha reenviado un correo de activación, porfavor verifique su bandeja de entrada";
                            }
                        }
                        else if (!usuario.IsActive)
                        { // No estaba aca el metodo actulizar , es solo una prueba
                             //_userService.ActualizarToken(viewModel.Email);

                            ViewBag.Error = "2 Su cuenta no ha sido activada. Se ha reenviado un correo de activación, porfavor verifique su bandeja de entrada";
                        }
                        else
                        {
                            if (usuario.Name is not null)
                            {
                                var claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, usuario.UserName),
                                    new Claim("IdUsuario", usuario.Id.ToString())
                                };

                                 string nombreRol = usuario.RolId == 1 ? "Administrador" : "Usuario";
                                claims.Add(new Claim(ClaimTypes.Role, nombreRol));

                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var propiedades = new AuthenticationProperties()
                                {
                                    AllowRefresh = true,
                                    IsPersistent = viewModel.KeepActive,
                                    ExpiresUtc = DateTimeOffset.UtcNow.Add(viewModel.KeepActive ? TimeSpan.FromDays(1) : TimeSpan.FromMinutes(1))
                                };

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), propiedades);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Correo no se encuentra registrado";
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            } 
            return View(viewModel); 
        }

        public async Task<ActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }


        

        
    }
}