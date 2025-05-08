import xml.etree.ElementTree as ET
import xmlrpc.client

try:
    tree = ET.parse("servicios_intercambio.xml")
    root = tree.getroot()
except Exception as e:
    print("Error leyendo servicios_intercambio.xml:", e)
    exit(1)

try:
    url = "http://127.0.0.1:8071"
    db = "hotelsol"
    username = "esierrasanch@uoc.edu"
    password = "HotelSol123"

    common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
    uid = common.authenticate(db, username, password, {})
    if not uid:
        raise Exception("Autenticacion fallida en Odoo")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

    cuentas_ingreso = models.execute_kw(
        db, uid, password, 'account.account', 'search_read',
        [[['account_type', '=', 'income']]],
        {'fields': ['id'], 'limit': 1}
    )
    if not cuentas_ingreso:
        raise Exception("No se encontro cuenta contable de ingresos.")
    cuenta_ingresos_id = cuentas_ingreso[0]['id']

    for servicio in root.findall("servicio"):
        nombre = servicio.findtext("nombre", "").strip()
        precio = float(servicio.findtext("precio", "0"))
        descripcion = servicio.findtext("descripcion", "").strip()

        existe = models.execute_kw(
            db, uid, password, 'product.template', 'search',
            [[['name', '=', nombre]]]
        )
        if existe:
            print("Servicio ya existe:", nombre)
            continue

        valores = {
            'name': nombre,
            'type': 'service',
            'list_price': precio,
            'description': descripcion,
            'property_account_income_id': cuenta_ingresos_id
        }

        template_id = models.execute_kw(db, uid, password, 'product.template', 'create', [valores])
        print("Servicio insertado:", nombre, "ID:", template_id)

        product_ids = models.execute_kw(
            db, uid, password, 'product.product', 'search',
            [[['product_tmpl_id', '=', template_id]]]
        )
        if product_ids:
            models.execute_kw(
                db, uid, password, 'product.product', 'write',
                [[product_ids[0]], {'property_account_income_id': cuenta_ingresos_id}]
            )

except Exception as e:
    print("Error insertando servicios en Odoo:", e)
