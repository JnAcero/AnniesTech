using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Models
{
    public class Rol:BaseEntity
    {
        [MaxLength(50),Required]
        public string Nombre { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}