
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using Microsoft.EntityFrameworkCore;

namespace AnniesTech.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPost
    {
        public PostRepository(AnnisTechDbContext context) : base(context)
        {
        }

        public Post GetPostByIdWithAllComments(int id)
        {
           return _context.Posts
                .Include(p =>p.Comentarios)
                    .ThenInclude(c =>c.ComentariosHijos)
                .FirstOrDefault(p =>p.Id == id); 
        }

        public async Task<List<Post>> PostPorCategoria(string categ)
        {
            var query = 
                from post in _context.Posts
                join categoria in _context.Categorias on post.CategoriaId equals categoria.Id
                where categoria.Nombre.ToLower() == categ.ToLower()
                select post;

                return await query.ToListAsync();
        }
    }
}