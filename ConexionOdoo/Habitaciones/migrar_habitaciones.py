import pyodbc
import xml.etree.ElementTree as ET

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
    root = ET.Element("habitaciones")

    for h in habitaciones:
        hab_elem = ET.SubElement(root, "habitacion")
        ET.SubElement(hab_elem, "id").text = str(h.Id)
        ET.SubElement(hab_elem, "numero").text = h.Numero
        ET.SubElement(hab_elem, "tipo").text = h.Tipo
        ET.SubElement(hab_elem, "capacidad").text = str(h.Capacidad)
        ET.SubElement(hab_elem, "precio").text = str(h.Precio)
        ET.SubElement(hab_elem, "disponibilidad").text = str(int(h.Disponibilidad))

    tree = ET.ElementTree(root)
    tree.write("habitaciones.xml", encoding="utf-8", xml_declaration=True)
    print("Archivo 'habitaciones.xml' generado correctamente.")

except Exception as e:
    print("Error generando habitaciones.xml:", e)
finally:
    try:
        conn.close()
    except:
        pass
