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
    }
}