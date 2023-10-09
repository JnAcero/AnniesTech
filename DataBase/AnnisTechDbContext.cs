
using System.Reflection;
using AnniesTech.Models;
using Microsoft.EntityFrameworkCore;

namespace AnniesTech.DataBase
{
    public class AnnisTechDbContext : DbContext
    {
        public AnnisTechDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Usuario> Usuarios =>Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
    }
}