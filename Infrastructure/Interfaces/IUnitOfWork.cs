using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRol Roles {get;}
        IUser Users {get;}
        IPost Posts {get;}
        IComentario Comentarios {get;}
        ICategoria Categorias { get;}
        Task<int> SaveAsync();
    }
}