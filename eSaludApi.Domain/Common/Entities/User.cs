using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSaludApi.Domain.Common.Entities;

namespace eSaludApi.Domain.Common.Entities
{
    public class User : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        public string Apellidos { get; private set; } = string.Empty;
        public string Correo { get; private set; } = string.Empty;
        public string Contrasena { get; private set; } = string.Empty;

        // Llave Foránea al Rol
        public int IdRol { get; private set; }
        public virtual Role Role { get; private set; } = null!;

        // Constructor para EF Core
        private User() { }

        public User(string nombre, string apellidos, string correo, string contrasena, int idRol)
        {
            // Validaciones (DDD Guard Clauses)
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellidos)) throw new ArgumentNullException(nameof(apellidos));
            if (string.IsNullOrWhiteSpace(correo)) throw new ArgumentNullException(nameof(correo));
            if (string.IsNullOrWhiteSpace(contrasena)) throw new ArgumentNullException(nameof(contrasena));

            Nombre = nombre;
            Apellidos = apellidos;
            Correo = correo;
            Contrasena = contrasena;
            IdRol = idRol;

            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        // Método para actualizar datos básicos
        public void UpdateProfile(string nombre, string apellidos, string correo)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Correo = correo;
            UpdateAt = DateTime.UtcNow;
        }

        // Método para cambiar estado
        public void SetStatus(bool status)
        {
            IsActive = status;
            UpdateAt = DateTime.UtcNow;
        }

        // Método para cambiar contraseña (Hashed)
        public void UpdatePassword(string passwordHashed)
        {
            Contrasena = passwordHashed;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
