import pyodbc
import xmlrpc.client

def sincronizar():
    try:
        conn = pyodbc.connect(
            "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
        )
        cursor = conn.cursor()
        cursor.execute("SELECT Numero, Disponibilidad FROM Habitaciones")
        habitaciones = cursor.fetchall()
    except Exception as e:
        print("Error al conectar a SQL Server:", e)
        return

    try:
        url = "http://127.0.0.1:8071"
        db = "hotelsol"
        username = "esierrasanch@uoc.edu"
        password = "HotelSol123"

        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        uid = common.authenticate(db, username, password, {})
        if not uid:
            raise Exception("Autenticacion fallida")

        models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

        actualizadas = 0
        for habitacion in habitaciones:
            numero = habitacion.Numero
            disponible = habitacion.Disponibilidad == 1

            product_variant_ids = models.execute_kw(
                db, uid, password, 'product.product', 'search',
                [[['default_code', '=', numero]]],
                {'context': {'active_test': False}}
            )

            if not product_variant_ids:
                print("Habitacion no encontrada:", numero)
                continue

            plantilla_id = models.execute_kw(
                db, uid, password, 'product.product', 'read',
                [product_variant_ids], {'fields': ['product_tmpl_id']}
            )[0]['product_tmpl_id'][0]

            models.execute_kw(
                db, uid, password, 'product.template', 'write',
                [[plantilla_id], {'active': disponible}]
            )

            print("Habitacion", numero, "marcada como", "activa" if disponible else "inactiva")
            actualizadas += 1

        print("\nSincronizacion completada. Habitaciones actualizadas:", actualizadas)

    except Exception as e:
        print("Error sincronizando disponibilidad en Odoo:", e)

    finally:
        try:
            conn.close()
        except:
            pass

if __name__ == "__main__":
    sincronizar()
