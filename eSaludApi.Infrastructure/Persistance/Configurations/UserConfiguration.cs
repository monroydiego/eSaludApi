using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSaludApi.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace eSaludApi.Infrastructure.Persistance.Configurations
{
   public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
          
            builder.ToTable("usuarios"); // Asi se llamará en sql

            builder.HasKey(u => u.Id);

            // Mapeamos 'Id' a 'idUsuario'
            builder.Property(u => u.Id)
                .HasColumnName("idUsuario")
                .UseIdentityColumn();

            builder.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Apellidos).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Correo).HasMaxLength(150).IsRequired();
            builder.Property(u => u.Contrasena).HasColumnName("contraseña").IsRequired();

            // Configuración de la Llave Foránea
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict); // Mantiene integridad referencial
        }
    }
   
}
