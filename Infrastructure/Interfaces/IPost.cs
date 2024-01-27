using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Interfaces
{
    public interface IPost:IGenericRepository<Post>
    {
        Post GetPostByIdWithAllComments(int id);
        Task<List<Post>> PostPorCategoria(string categ);
    }
}