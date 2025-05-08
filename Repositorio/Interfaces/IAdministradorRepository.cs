// IAdministradorRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSOL.Modelo;

namespace HotelSOL.Repositorio.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Administrador?> AutenticarAdministradorAsync(string email, string password);
        Task<List<Usuario>> ObtenerTodosLosUsuariosAsync();
        Task<bool> CrearUsuarioAsync(Usuario usuario);
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        Task<bool> EliminarUsuarioAsync(int id);
        Task<bool> GuardarConfiguracionSistemaAsync(Dictionary<string, string> configuraciones);
    }
}
