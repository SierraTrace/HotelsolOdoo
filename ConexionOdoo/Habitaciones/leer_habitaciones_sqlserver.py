import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT h.Id, h.Numero, th.Nombre AS Tipo, h.Capacidad, h.Precio, h.Disponibilidad
        FROM Habitaciones h
        JOIN TipoHabitaciones th ON h.TipoHabitacionId = th.Id
    """)

    habitaciones = cursor.fetchall()
    if not habitaciones:
        print("No se encontraron habitaciones.")
    else:
        print("Habitaciones registradas en el sistema:")
        for h in habitaciones:
            print({
                "id": h.Id,
                "numero": h.Numero,
                "tipo": h.Tipo,
                "capacidad": h.Capacidad,
                "precio": h.Precio,
                "disponibilidad": "Si" if h.Disponibilidad else "No"
            })

except Exception as e:
    print("Error consultando habitaciones:", e)
finally:
    try:
        conn.close()
    except:
        pass
