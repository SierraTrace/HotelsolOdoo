import pyodbc
import xml.etree.ElementTree as ET

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
    root = ET.Element("reservas")

    for r in reservas:
        reserva = ET.SubElement(root, "reserva")
        ET.SubElement(reserva, "cliente").text = f"{r.Nombre} {r.Apellido}"
        ET.SubElement(reserva, "email").text = r.Email
        ET.SubElement(reserva, "habitacion").text = r.Habitacion
        ET.SubElement(reserva, "fecha_entrada").text = str(r.FechaEntrada.date())
        ET.SubElement(reserva, "fecha_salida").text = str(r.FechaSalida.date())
        ET.SubElement(reserva, "precio").text = str(r.PrecioPorNoche)

    tree = ET.ElementTree(root)
    tree.write("reservas.xml", encoding="utf-8", xml_declaration=True)
    print("Archivo 'reservas.xml' generado correctamente.")

except Exception as e:
    print("Error generando XML de reservas:", e)
finally:
    try:
        conn.close()
    except:
        pass
