using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Infrastructure.Helpers;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using AnniesTech.Models.DTOs;
using AutoMapper;


namespace AnniesTech.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void RegisterUser(RegisterDto userData)
        {
            var user = _mapper.Map<Usuario>(userData);
        }

        public async Task<int> ActivarUsuario(string token)
        {
            var userExist = _unitOfWork.Users.Exist(u =>u.Token == token);
            if(userExist)
            {
                var user =await _unitOfWork.Users.FindFirst(u =>u.Token == token);
                DateTime fechaActual = DateTime.UtcNow.AddMinutes(5);
                if(user.ExpirationDate < fechaActual)
                {
                    user.IsActive = true;
                    user.Token = null;
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.SaveAsync();
                }
                return 1;
            }
            else return 0;
        }

        public async Task ActualizarToken(string correo)
        {
            var user = await _unitOfWork.Users.FindFirst(u =>u.Email == correo);
            DateTime nuevaFechaExpiracion = DateTime.UtcNow.AddMinutes(5);
            Guid token = new Guid();

            user.Token = token.ToString();
            user.ExpirationDate = nuevaFechaExpiracion;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            Email email = new();
            if(correo is not null) 
                email.Enviar(correo,token.ToString());
        }

        public async Task ActualizarPerfil(Usuario model)
        {
            var user = await _unitOfWork.Users.FindFirst(u =>u.Id == model.Id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync(); 
        }
        
    }
}