using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    // [Table("Administradores")]
    public class Administrador : Recepcionista
    {
        public Administrador() { }

        public Administrador(string nombre, string apellido, string email, string password, int movil)
            : base(nombre, apellido, email, password, movil) { }

        // Gestión de usuarios del sistema
        public virtual void GestionarUsuarios() { }

        // Configuración de parámetros del sistema
        public virtual void ConfigurarSistema() { }
    }
}
