import pyodbc
import xml.etree.ElementTree as ET

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()
    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil, c.EsVIP
        FROM Clientes c
        JOIN Usuarios u ON c.Id = u.Id
        WHERE u.Email LIKE '%@%'
    """)
    clientes = cursor.fetchall()

    root = ET.Element("clientes")

    for cliente in clientes:
        c_elem = ET.SubElement(root, "cliente")
        ET.SubElement(c_elem, "id").text = str(cliente.Id)
        ET.SubElement(c_elem, "nombre").text = str(cliente.Nombre)
        ET.SubElement(c_elem, "apellido").text = str(cliente.Apellido)
        ET.SubElement(c_elem, "email").text = str(cliente.Email)
        ET.SubElement(c_elem, "movil").text = str(cliente.Movil)
        ET.SubElement(c_elem, "es_vip").text = str(cliente.EsVIP)

    tree = ET.ElementTree(root)
    tree.write("clientes_intercambio.xml", encoding="utf-8", xml_declaration=True)

    print("Archivo XML 'clientes_intercambio.xml' generado correctamente.")

except Exception as e:
    print("Error al generar XML de clientes:", e)
finally:
    try:
        conn.close()
    except:
        pass
