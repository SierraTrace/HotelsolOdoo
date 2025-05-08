import xml.etree.ElementTree as ET
import xmlrpc.client
from sincronizar_disponibilidad import sincronizar

try:
    tree = ET.parse("habitaciones.xml")
    root = tree.getroot()
except Exception as e:
    print("Error leyendo habitaciones.xml:", e)
    exit(1)

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

    cuentas = models.execute_kw(
        db, uid, password, 'account.account', 'search_read',
        [[['account_type', '=', 'income']]],
        {'fields': ['id'], 'limit': 1}
    )
    if not cuentas:
        raise Exception("No se encontro cuenta contable de ingresos")
    cuenta_ingresos_id = cuentas[0]['id']

    for h in root.findall("habitacion"):
        numero = h.findtext("numero", "").strip()
        tipo = h.findtext("tipo", "").strip()
        capacidad = h.findtext("capacidad", "0").strip()
        precio = float(h.findtext("precio", "0"))
        disponible = h.findtext("disponibilidad", "0").strip() == "1"

        if not numero or not tipo:
            print("Habitacion con datos incompletos. Omitida.")
            continue

        nombre = f"{numero} - {tipo}"
        descripcion = f"Capacidad: {capacidad} personas. Disponible: {'Si' if disponible else 'No'}"

        existe = models.execute_kw(
            db, uid, password, 'product.template', 'search',
            [[['default_code', '=', numero]]]
        )
        if existe:
            print("Habitacion ya existe:", nombre)
            continue

        valores = {
            'name': nombre,
            'type': 'service',
            'list_price': precio,
            'default_code': numero,
            'description': descripcion,
            'active': disponible,
            'property_account_income_id': cuenta_ingresos_id
        }

        product_id = models.execute_kw(db, uid, password, 'product.template', 'create', [valores])
        print("Habitacion insertada:", nombre, "ID:", product_id)

    print("Habitaciones cargadas correctamente.")
    print("Sincronizando disponibilidad...")
    sincronizar()
    print("Proceso completo.")

except Exception as e:
    print("Error cargando habitaciones en Odoo:", e)
