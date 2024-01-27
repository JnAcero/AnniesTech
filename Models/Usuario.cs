using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Models
{
    public class Usuario:BaseEntity
    {
        [MaxLength(50)]
        [Required(ErrorMessage ="El campo Nombre es obligatorio")]
        public string Name { get; set; }
        
        [MaxLength(50)]
        [Required(ErrorMessage ="El campo Apellido es obligatorio")]
        public string Surname {get; set;} 
        [MaxLength(100)]
        [Required(ErrorMessage ="El campo Email es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage ="El campo Contraseña es obligatorio")]
        public string Password { get; set; }
        public int ? RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
        [MaxLength(50),Required]
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string ? Token { get; set; }
        public DateTime ? ExpirationDate { get; set; }
         public ICollection<Post> Posts {get; set;} = new List<Post>();
        
        
    }
}