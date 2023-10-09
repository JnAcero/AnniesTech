
using System.ComponentModel.DataAnnotations.Schema;

namespace AnniesTech.Models
{
    public class Comentario:BaseEntity
    {
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public int ? ComentarioPadreId { get; set; }
        [ForeignKey("ComentarioPadreId")]
        public Comentario ComentarioPadre { get; set; }
        public ICollection<Comentario> ? ComentariosHijos {get; set;}
        [NotMapped]
        public int ? ComentarioAbueloId { get; set; }
    }
}