
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;

namespace AnniesTech.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPost
    {
        public PostRepository(AnnisTechDbContext context) : base(context)
        {
        }
    }
}