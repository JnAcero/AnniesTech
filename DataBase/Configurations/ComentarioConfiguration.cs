
using AnniesTech.Models;
using Microsoft.EntityFrameworkCore;

namespace AnniesTech.DataBase.Configurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comentario> builder)
        {
            builder.HasMany(c =>c.ComentariosHijos)
                    .WithOne(c =>c.ComentarioPadre)
                    .HasForeignKey(c =>c.ComentarioPadreId);
        }
    }
}