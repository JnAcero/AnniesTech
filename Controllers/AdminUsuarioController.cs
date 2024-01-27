
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace AnniesTech.Controllers
{
    [Route("[controller]")]
    public class AdminUsuarioController : BaseController
    {
        public AdminUsuarioController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> Index(string buscar, int? pagina)
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if (!string.IsNullOrEmpty(buscar))
                users = users
                    .Where(u => u.Email != null && u.Email.Contains(buscar) || u.UserName != null && u.UserName.Contains(buscar))
                    .OrderBy(u => u.UserName)
                    .ToList();
            var roles = await _unitOfWork.Roles.GetAllAsync();

            List<SelectListItem> rolesItems = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nombre
            }).ToList();
            ViewBag.Roles = rolesItems;

            int pageSize = 10;
            int pageNumber = pagina ?? 1;
            var usuarioPaginados = users.ToPagedList(pageNumber, pageSize);

            return View(usuarioPaginados);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();

            List<SelectListItem> rolesItems = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nombre
            }).ToList();
            ViewBag.Roles = rolesItems;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            try
            {
                _unitOfWork.Users.Add(usuario);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuario);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if(user is null) return NotFound();
                
             var roles = await _unitOfWork.Roles.GetAllAsync();

            List<SelectListItem> rolesItems = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nombre
            }).ToList();
            ViewBag.Roles = rolesItems;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
             if(id != usuario.Id) return NotFound();
             try
             {
                _unitOfWork.Users.Update(usuario);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index");
             }
             catch(Exception ex)
             {
                ViewBag.Error = ex.Message;
                return View(usuario);
             }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _unitOfWork.Users.GetByIdAsync(id);
            if(usuario is null) return NotFound();

           return View(usuario);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             var usuario = await _unitOfWork.Users.GetByIdAsync(id);
            try
            {
            _unitOfWork.Users.Remove(usuario);
            await _unitOfWork.SaveAsync();
            
            return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                
                return View(usuario);
            }
            
        }
    }
}