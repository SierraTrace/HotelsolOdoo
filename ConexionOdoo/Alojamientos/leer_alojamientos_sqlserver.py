import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()
    cursor.execute("""
        SELECT a.Id, a.TipoId, t.Nombre AS TipoNombre, a.Precio
        FROM Alojamientos a
        JOIN TipoAlojamientos t ON a.TipoId = t.Id
    """)

    alojamientos = cursor.fetchall()
    if not alojamientos:
        print("No se encontraron alojamientos en la base de datos.")
    else:
        print("Alojamientos encontrados:")
        for a in alojamientos:
            print({
                "id": a.Id,
                "tipo_id": a.TipoId,
                "tipo_nombre": a.TipoNombre,
                "precio_base": a.Precio
            })

except Exception as e:
    print(f"Error de conexion o consulta a SQL Server: {e}")
finally:
    try:
        conn.close()
    except:
        pass
