import pyodbc
import xml.etree.ElementTree as ET

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    root = ET.Element("empleados")

    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil
        FROM Administradores a
        JOIN Usuarios u ON a.Id = u.Id
    """)
    for a in cursor.fetchall():
        e = ET.SubElement(root, "empleado")
        ET.SubElement(e, "id").text = str(a.Id)
        ET.SubElement(e, "nombre").text = f"{a.Nombre} {a.Apellido}"
        ET.SubElement(e, "email").text = a.Email
        ET.SubElement(e, "movil").text = str(a.Movil)
        ET.SubElement(e, "departamento").text = "Administradores"

    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil
        FROM Recepcionistas r
        JOIN Usuarios u ON r.Id = u.Id
    """)
    for r in cursor.fetchall():
        e = ET.SubElement(root, "empleado")
        ET.SubElement(e, "id").text = str(r.Id)
        ET.SubElement(e, "nombre").text = f"{r.Nombre} {r.Apellido}"
        ET.SubElement(e, "email").text = r.Email
        ET.SubElement(e, "movil").text = str(r.Movil)
        ET.SubElement(e, "departamento").text = "Recepcionistas"

    tree = ET.ElementTree(root)
    tree.write("empleados.xml", encoding="utf-8", xml_declaration=True)
    print("Archivo 'empleados.xml' generado correctamente.")

except Exception as e:
    print("Error generando el archivo empleados.xml:", e)
finally:
    try:
        conn.close()
    except:
        pass
