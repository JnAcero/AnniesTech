using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Usuario>,IUser
    {
        public UserRepository(AnnisTechDbContext context) : base(context)
        {
        }

        public bool ExistUserByUsername(string userName)
        {
            var exist = _context.Usuarios.Any(u =>u.UserName.ToLower() == userName.ToLower());
            return exist;
        }

    }


}