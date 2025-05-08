import pyodbc
import re

def es_email_valido(email):
    return re.match(r"[^@]+@[^@]+\\.[^@]+", email)

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()
    cursor.execute("""
        SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Movil, c.EsVIP
        FROM Clientes c
        JOIN Usuarios u ON c.Id = u.Id
    """)

    clientes = cursor.fetchall()

    print("Clientes validos encontrados:")
    for cliente in clientes:
        if not es_email_valido(cliente.Email):
            print("Email invalido, omitido:", cliente.Email)
            continue
        if not cliente.Nombre or not cliente.Apellido:
            print("Nombre o apellido incompletos, omitido ID", cliente.Id)
            continue
        print({
            "id": cliente.Id,
            "nombre": cliente.Nombre,
            "apellido": cliente.Apellido,
            "email": cliente.Email,
            "movil": cliente.Movil,
            "es_vip": cliente.EsVIP
        })

except Exception as e:
    print("Error de conexion o consulta SQL:", e)
finally:
    try:
        conn.close()
    except:
        pass
