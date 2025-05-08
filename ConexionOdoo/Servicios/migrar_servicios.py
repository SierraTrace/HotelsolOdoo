import pyodbc
import xml.etree.ElementTree as ET

try:
    conn_sql = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn_sql.cursor()
    cursor.execute("SELECT Id, Nombre, Precio, Descripcion FROM Servicios")
    servicios = cursor.fetchall()

    root = ET.Element("servicios")
    nombres_agregados = set()
    duplicados_omitidos = 0

    for servicio in servicios:
        nombre = str(servicio.Nombre)
        if nombre in nombres_agregados:
            duplicados_omitidos += 1
            continue

        nombres_agregados.add(nombre)

        s_elem = ET.SubElement(root, "servicio")
        ET.SubElement(s_elem, "id").text = str(servicio.Id)
        ET.SubElement(s_elem, "nombre").text = nombre
        ET.SubElement(s_elem, "precio").text = str(servicio.Precio)
        ET.SubElement(s_elem, "descripcion").text = str(servicio.Descripcion)

    tree = ET.ElementTree(root)
    tree.write("servicios_intercambio.xml", encoding="utf-8", xml_declaration=True)

    print("Archivo 'servicios_intercambio.xml' generado correctamente.")
    print("Servicios duplicados omitidos:", duplicados_omitidos)

except Exception as e:
    print("Error generando el XML de servicios:", e)
finally:
    try:
        conn_sql.close()
    except:
        pass
