using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Repositories
{
    public class RolRepository : GenericRepository<Rol>,IRol
    {
        public RolRepository(AnnisTechDbContext context) : base(context)
        {
        }
    }
}