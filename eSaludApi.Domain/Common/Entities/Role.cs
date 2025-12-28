using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaludApi.Domain.Common.Entities
{
    public class Role : BaseEntity // Hereda de BaseEntity
    {
        public string NombreRol { get; private set; } = string.Empty; // Inicializando  una cadena vacia

        public virtual ICollection<User> Usuarios { get; private set; } = new List<User>();

        private Role() { } // Constructor para EF Core

        public Role(string nombreRol)
        {
            if (string.IsNullOrWhiteSpace(nombreRol))

            throw new ArgumentNullException(nameof(nombreRol), "El nombre del rol es requerido");

            NombreRol = nombreRol;
        }
        public void UpdateNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            NombreRol = nuevoNombre;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
