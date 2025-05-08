// HotelSOL.Data/HotelContext.cs
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        // Usuarios y roles
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        // Reservas e intermedias
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaHabitacion> ReservaHabitaciones { get; set; }
        public DbSet<ReservaServicio> ReservaServicios { get; set; }

        // Habitaciones y tipologías
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<TipoHabitacion> TipoHabitaciones { get; set; }
        public DbSet<Alojamiento> Alojamientos { get; set; }
        public DbSet<TipoAlojamiento> TipoAlojamientos { get; set; }

        // Calendarios y temporadas
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<TipoTemporada> TipoTemporadas { get; set; }
        public DbSet<Temporada> Temporadas { get; set; }

        // Tipos de reserva y estados
        public DbSet<TipoReserva> TipoReservas { get; set; }
        public DbSet<Estado> Estados { get; set; }

        // Facturación
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFacturaServicio> DetalleFacturaServicios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        // Histórico
        public DbSet<HistoricoEstancia> HistoricoEstancias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Reserva–Habitacion (m:n)
            modelBuilder.Entity<ReservaHabitacion>()
                .HasKey(rh => new { rh.ReservaId, rh.HabitacionId });
            modelBuilder.Entity<ReservaHabitacion>()
                .HasOne(rh => rh.Reserva)
                .WithMany(r => r.ReservaHabitaciones)
                .HasForeignKey(rh => rh.ReservaId);
            modelBuilder.Entity<ReservaHabitacion>()
                .HasOne(rh => rh.Habitacion)
                .WithMany(h => h.ReservaHabitaciones)
                .HasForeignKey(rh => rh.HabitacionId);

            // Reserva–Servicio (m:n)
            modelBuilder.Entity<ReservaServicio>()
                .HasKey(rs => new { rs.ReservaId, rs.ServicioId });
            modelBuilder.Entity<ReservaServicio>()
                .HasOne(rs => rs.Reserva)
                .WithMany(r => r.Servicios)
                .HasForeignKey(rs => rs.ReservaId);
            modelBuilder.Entity<ReservaServicio>()
                .HasOne(rs => rs.Servicio)
                .WithMany()
                .HasForeignKey(rs => rs.ServicioId);

            // Reserva–Factura (1:1)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Factura)
                .WithOne(f => f.Reserva)
                .HasForeignKey<Factura>(f => f.ReservaId);

            // Factura–DetalleFacturaServicio (1:n) usando 'Detalles'
            modelBuilder.Entity<DetalleFacturaServicio>()
                .HasOne(d => d.Factura)
                .WithMany(f => f.Detalles)
                .HasForeignKey(d => d.FacturaId);
            modelBuilder.Entity<DetalleFacturaServicio>()
                .HasOne(d => d.Servicio)
                .WithMany()
                .HasForeignKey(d => d.ServicioId);

            // **NUEVO**: HistoricoEstancia–Factura (n:1) con NO ACTION en delete
            modelBuilder.Entity<HistoricoEstancia>()
                .HasOne(h => h.Factura)
                .WithMany()                      // sin propiedad de navegación inversa
                .HasForeignKey(h => h.FacturaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mapear tablas de Usuario
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Recepcionista>().ToTable("Recepcionistas");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");
        }
    }
}
