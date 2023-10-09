using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnniesTech.Models.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public string UserName { get; set; }
    }
}