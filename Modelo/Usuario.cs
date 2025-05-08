using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public abstract class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Movil { get; set; }

        [NotMapped]
        public string Tipo => GetType().Name;


        public Usuario() { }

        public Usuario(string nombre, string apellido, string email, string password, int movil)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Password = password;
            Movil = movil;
        }

        public virtual void CerrarSesion()
        {
            // Este método podría registrar un log de cierre de sesión o invalidar un token
        }

        public virtual void SolicitarAyuda(string mensaje)
        {
            // Este método podría enviar un mensaje a soporte o registrar un incidente
        }
    }
}
