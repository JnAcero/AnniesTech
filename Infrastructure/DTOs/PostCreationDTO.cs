using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Infrastructure.DTOs
{
    public struct PostCreationDTO
    {
         public string Titulo { get; set; }
        public string  Contenido { get; set; }
        public int CategoriaId { get; set; }
        public DateTime FechaCreacion {get; set;}
    }
}