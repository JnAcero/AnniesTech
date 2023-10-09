using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoria
    {
        public CategoriaRepository(AnnisTechDbContext context) : base(context)
        {
        }
    }
}