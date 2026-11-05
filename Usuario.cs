using System;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaValdivia.Models
{
    // CLASE BASE
    public class Usuario
    {
        [Key] // <-- LE INDICA A EF CORE QUE ESTA ES LA LLAVE PRIMARIA
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; }                 
        public string Username { get; set; }
        public bool Activo { get; set; }
        protected string PasswordHash { get; set; }

        // Constructor vacío (obligatorio para Entity Framework Core)
        public Usuario()
        {
        }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombreCompleto, string username, string passwordHash, bool activo)
        {
            IdUsuario = idUsuario;
            NombreCompleto = nombreCompleto;
            Username = username;
            PasswordHash = passwordHash;
            Activo = activo;
        }
    }
}