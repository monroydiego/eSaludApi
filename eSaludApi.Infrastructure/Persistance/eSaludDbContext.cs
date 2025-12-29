using eSaludApi.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eSaludApi.Infrastructure.Persistance
{
    public class eSaludDbContext : DbContext

    {
        public eSaludDbContext(DbContextOptions<eSaludDbContext> options) : base(options) { }

        public DbSet<User> Usuarios => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Aplica las configuraciones específicas (como los nombres idRol, idUsuario)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // 2. AUTOMATIZACIÓN GLOBAL: Buscamos todas las entidades que heredan de BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Verificamos si la clase hereda de BaseEntity
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Forzamos el nombre de columna en minúsculas (camelCase) para SQL
                    modelBuilder.Entity(entityType.ClrType).Property("CreatedAt")
                        .HasColumnName("createdAt")
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                    modelBuilder.Entity(entityType.ClrType).Property("UpdateAt")
                        .HasColumnName("updateAt")
                        .IsRequired(false);

                    modelBuilder.Entity(entityType.ClrType).Property("IsActive")
                        .HasColumnName("isActive")
                        .HasDefaultValue(true);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
