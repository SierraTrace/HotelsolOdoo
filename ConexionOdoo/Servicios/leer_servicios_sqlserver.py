import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()
    cursor.execute("SELECT Id, Nombre, Precio, Descripcion FROM Servicios")
    servicios = cursor.fetchall()

    print("Servicios encontrados:")
    for s in servicios:
        print({
            "id": s.Id,
            "nombre": s.Nombre,
            "precio": s.Precio,
            "descripcion": s.Descripcion
        })

except Exception as e:
    print("Error consultando servicios:", e)
finally:
    try:
        conn.close()
    except:
        pass
