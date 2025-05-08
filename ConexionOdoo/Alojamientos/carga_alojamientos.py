import xml.etree.ElementTree as ET
import xmlrpc.client

try:
    tree = ET.parse("alojamientos_intercambio.xml")
    root = tree.getroot()
except Exception as e:
    print(f"Error al leer el archivo XML: {e}")
    exit(1)

try:
    url = "http://127.0.0.1:8071"
    db = "hotelsol"
    username = "esierrasanch@uoc.edu"
    password = "HotelSol123"

    common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
    uid = common.authenticate(db, username, password, {})
    if not uid:
        raise Exception("Autenticacion fallida en Odoo.")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

    cuentas = models.execute_kw(
        db, uid, password, 'account.account', 'search_read',
        [[['account_type', '=', 'income']]],
        {'fields': ['id', 'name'], 'limit': 1}
    )
    if not cuentas:
        raise Exception("No se encontro cuenta contable de ingresos.")
    cuenta_ingresos_id = cuentas[0]['id']

    for aloj_elem in root.findall("alojamiento"):
        aloj_id = aloj_elem.findtext("id", "").strip()
        tipo_nombre = aloj_elem.findtext("tipo_nombre", "").strip()
        precio_text = aloj_elem.findtext("precio_base", "0").strip()

        if not aloj_id or not tipo_nombre:
            print(f"Alojamiento incompleto, omitido: {ET.tostring(aloj_elem, encoding='unicode')}")
            continue

        try:
            precio = float(precio_text)
        except ValueError:
            print(f"Precio invalido para alojamiento '{aloj_id}': {precio_text}")
            continue

        nombre_producto = f"{aloj_id} - {tipo_nombre}"
        existe = models.execute_kw(
            db, uid, password, 'product.template', 'search',
            [[['name', '=', nombre_producto]]]
        )
        if existe:
            print(f"Alojamiento '{nombre_producto}' ya existe. Omitido.")
            continue

        valores = {
            'name': nombre_producto,
            'type': 'service',
            'list_price': precio,
            'default_code': aloj_id,
            'property_account_income_id': cuenta_ingresos_id
        }

        product_id = models.execute_kw(db, uid, password, 'product.template', 'create', [valores])
        print(f"Alojamiento '{nombre_producto}' insertado con ID: {product_id}")

except Exception as e:
    print(f"Error general en conexion o insercion en Odoo: {e}")
