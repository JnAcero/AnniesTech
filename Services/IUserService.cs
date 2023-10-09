using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Models.DTOs;

namespace AnniesTech.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterDto userData);
    }
}