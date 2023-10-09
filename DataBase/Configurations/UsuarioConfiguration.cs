using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnniesTech.DataBase.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(u =>u.Email).IsUnique();
        }
    }
}