using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Models
{
    public class Post:BaseEntity
    {
        [MaxLength(500),MinLength(5)]
        [Required(ErrorMessage ="El titulo es requerido")]
        public string Titulo { get; set; }
        public string  Contenido { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set;}
        public DateTime FechaCreacion {get; set;} = DateTime.Now;
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public ICollection<Comentario> Comentarios {get; set;} = new List<Comentario>();
    }
}