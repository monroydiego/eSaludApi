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
    public class RoleConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("rol"); // Nombre exacto de la tabla

            builder.HasKey(r => r.Id); // Declaramos nuestra llave primaria (estandarizada)

            // Mapeamos la propiedad 'Id' heredada a la columna 'idRol'
            builder.Property(r => r.Id) // construimos un mapeo para la propiedad Id
                .HasColumnName("idRol") // este será el nombre exacto en la base de datos
                .UseIdentityColumn(1, 1); // será autoincrementable

            builder.Property(r => r.NombreRol) // construimos un mapeo para la propiedad NombreRol
                .HasColumnName("nombreRol") // este será el nombre exacto en la db
                .HasMaxLength(55) // tendrá un tamaño de 55 char
                .IsRequired(); // no nulo

            // Los campos de BaseEntity también se pueden mapear específicamente
            builder.Property(r => r.CreatedAt).HasColumnName("createdAt")
                .IsRequired() // indicamos que es NOT NULL
                .HasDefaultValueSql("GETUTCDATE()"); // valor por defecto de la fecha actual

            builder.Property(r => r.UpdateAt).HasColumnName("updateAt")
                .HasColumnName("updateAt")
                .IsRequired(false);

            builder.Property(r => r.IsActive).HasColumnName("isActive")
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
