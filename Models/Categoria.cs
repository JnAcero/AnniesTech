using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Models
{
    public class Categoria:BaseEntity
    {
        [MaxLength(100),Required]
        public string Nombre { get; set; }=null!;
        public string ? Descripcion {get; set;}
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
    }
}