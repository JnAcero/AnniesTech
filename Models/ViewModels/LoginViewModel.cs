
using System.ComponentModel.DataAnnotations;

namespace AnniesTech.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="El Email es requerido")]
        [EmailAddress(ErrorMessage ="Porfavor,introduce un correo email valido")]
        [StringLength(100,ErrorMessage ="El campo Email debe tener maximo 100 caracteres")]
        public string Email { get; set; } =null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } =null!;
        [Display(Name ="Mantener sesion activa")]
        public bool KeepActive { get; set; }
        
    }
}