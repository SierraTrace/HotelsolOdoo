import pyodbc

try:
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT 
            f.Id AS FacturaId, f.FechaEmision, f.Total, f.MetodoPago, f.Pagada,
            u.Nombre, u.Apellido, u.Email,
            s.Nombre AS Servicio, dfs.Cantidad, dfs.PrecioUnitario
        FROM Facturas f
        JOIN Reservas r ON f.ReservaId = r.Id
        JOIN Usuarios u ON r.ClienteId = u.Id
        LEFT JOIN DetalleFacturaServicios dfs ON dfs.FacturaId = f.Id
        LEFT JOIN Servicios s ON s.Id = dfs.ServicioId
        ORDER BY f.Id
    """)

    filas = cursor.fetchall()
    if not filas:
        print("No se encontraron facturas.")
    else:
        for row in filas:
            print({
                "factura_id": row.FacturaId,
                "cliente": f"{row.Nombre} {row.Apellido}",
                "email": row.Email,
                "fecha": str(row.FechaEmision),
                "servicio": row.Servicio,
                "cantidad": row.Cantidad,
                "precio": row.PrecioUnitario
            })

except Exception as e:
    print("Error consultando facturas:", e)
finally:
    try:
        conn.close()
    except:
        pass
