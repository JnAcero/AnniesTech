using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Interfaces
{
    public interface ICategoria:IGenericRepository<Categoria>
    {
        string ObtenerDescripcion(string categoria);
    }
}