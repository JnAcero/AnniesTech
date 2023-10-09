using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Infrastructure.Repositories;

namespace AnniesTech.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserRepository _usuarios;
        private RolRepository _roles;
        private PostRepository _posts;
        private CometarioRepository _comentarios;
        private CategoriaRepository _categorias;


        private AnnisTechDbContext _context { get; }

        public UnitOfWork(AnnisTechDbContext context)
        {
            _context = context;
        }

        public IUser Users
        {
            get
            {
                _usuarios ??= new UserRepository(_context);
                return _usuarios;
            }
        }
        public IRol Roles
        {
            get
            {
                _roles ??= new RolRepository(_context);
                return _roles;
            }
        }

        public IPost Posts
        {
            get{
                _posts ??=new PostRepository(_context);
                return _posts;
            }
        }

        public IComentario Comentarios  
        {
            get{
                _comentarios ??=new CometarioRepository(_context);
                return _comentarios;
            }
        }

        public ICategoria Categorias
         {
            get{
                _categorias ??=new CategoriaRepository(_context);
                return _categorias;
            }
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}