import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    print("Administradores encontrados:")
    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil
        FROM Administradores a
        JOIN Usuarios u ON a.Id = u.Id
    """)
    for a in cursor.fetchall():
        print({
            "id": a.Id,
            "nombre": a.Nombre,
            "apellido": a.Apellido,
            "email": a.Email,
            "movil": a.Movil,
            "departamento": "Administradores"
        })

    print("\nRecepcionistas encontrados:")
    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil
        FROM Recepcionistas r
        JOIN Usuarios u ON r.Id = u.Id
    """)
    for r in cursor.fetchall():
        print({
            "id": r.Id,
            "nombre": r.Nombre,
            "apellido": r.Apellido,
            "email": r.Email,
            "movil": r.Movil,
            "departamento": "Recepcionistas"
        })

except Exception as e:
    print("Error al consultar SQL Server:", e)
finally:
    try:
        conn.close()
    except:
        pass
