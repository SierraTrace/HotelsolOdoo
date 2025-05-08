using HotelSOL.Data;
using HotelSOL.Modelo;

public static class SeedData
{
    public static async Task InicializarAsync(HotelContext context)
    {
        if (!context.Estados.Any())
        {
            context.Estados.AddRange(
                new Estado { Nombre = "Pendiente" },
                new Estado { Nombre = "Confirmada" },
                new Estado { Nombre = "Cancelada" },
                new Estado { Nombre = "No presentado" }
            );
        }

        if (!context.TipoReservas.Any())
        {
            context.TipoReservas.AddRange(
                new TipoReserva { Nombre = "Previa" },
                new TipoReserva { Nombre = "No Previa" }
            );
        }

        if (!context.TipoAlojamientos.Any())
        {
            context.TipoAlojamientos.AddRange(
                new TipoAlojamiento { Nombre = "Media Pensión" },
                new TipoAlojamiento { Nombre = "Pensión Completa" },
                new TipoAlojamiento { Nombre = "Todo Incluido"}
            );
        }

        await context.SaveChangesAsync();
    }
}
