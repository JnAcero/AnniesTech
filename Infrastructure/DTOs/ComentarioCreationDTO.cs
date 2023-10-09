using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Infrastructure.DTOs
{
    public class ComentarioCreationDTO
    {
        public int PostId { get; set; }
        public string Contenido { get; set; } = null!;
        public int? ComentarioPadreId { get; set; }
       
    }
}