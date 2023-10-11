using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                DateTime fechaActual = DateTime.UtcNow;
                if(user.ExpirationDate < fechaActual)
                {
                    user.IsActive = true;
                    user.Token = null;
                    await _unitOfWork.SaveAsync();
                }
                return 1;
            }
            else return 0;
        }
    }
}