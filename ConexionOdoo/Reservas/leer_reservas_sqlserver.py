import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT 
            rh.ReservaId,
            r.ClienteId,
            u.Nombre,
            u.Apellido,
            u.Email,
            h.Numero AS Habitacion,
            r.FechaEntrada,
            r.FechaSalida,
            rh.PrecioPorNoche
        FROM ReservaHabitaciones rh
        JOIN Reservas r ON rh.ReservaId = r.Id
        JOIN Usuarios u ON r.ClienteId = u.Id
        JOIN Habitaciones h ON rh.HabitacionId = h.Id
    """)

    reservas = cursor.fetchall()

    if not reservas:
        print("No se encontraron reservas.")
    else:
        print("Reservas encontradas:")
        for r in reservas:
            print({
                "reserva_id": r.ReservaId,
                "cliente": f"{r.Nombre} {r.Apellido}",
                "email": r.Email,
                "habitacion": r.Habitacion,
                "entrada": r.FechaEntrada,
                "salida": r.FechaSalida,
                "precio": r.PrecioPorNoche
            })

except Exception as e:
    print("Error consultando reservas:", e)
finally:
    try:
        conn.close()
    except:
        pass
