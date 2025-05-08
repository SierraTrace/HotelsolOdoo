// AdministradorRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorio
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly HotelContext _context;
        public AdministradorRepository(HotelContext ctx) => _context = ctx;

        public async Task<Administrador?> AutenticarAdministradorAsync(string em, string pw)
            => await _context.Usuarios.OfType<Administrador>()
                .FirstOrDefaultAsync(a => a.Email == em && a.Password == pw);

        public async Task<List<Usuario>> ObtenerTodosLosUsuariosAsync()
            => await _context.Usuarios.ToListAsync();

        public async Task<bool> CrearUsuarioAsync(Usuario u)
        {
            try { _context.Usuarios.Add(u); await _context.SaveChangesAsync(); return true; }
            catch { return false; }
        }
        public async Task<bool> ActualizarUsuarioAsync(Usuario u)
        {
            var e = await _context.Usuarios.FindAsync(u.Id);
            if (e == null) return false;
            e.Nombre = u.Nombre; e.Apellido = u.Apellido; e.Email = u.Email; e.Password = u.Password; e.Movil = u.Movil;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return false;
            _context.Usuarios.Remove(u);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> GuardarConfiguracionSistemaAsync(Dictionary<string, string> cfg)
        {
            await Task.CompletedTask; return true;
        }
    }
}
