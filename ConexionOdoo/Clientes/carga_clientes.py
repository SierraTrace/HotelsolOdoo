import xml.etree.ElementTree as ET
import xmlrpc.client

try:
    tree = ET.parse("clientes_intercambio.xml")
    root = tree.getroot()
except Exception as e:
    print("Error al leer el archivo XML:", e)
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

    vip_category_id = models.execute_kw(
        db, uid, password, 'res.partner.category', 'search',
        [[['name', '=', 'VIP']]]
    )
    if not vip_category_id:
        vip_category_id = models.execute_kw(
            db, uid, password, 'res.partner.category', 'create',
            [{'name': 'VIP'}]
        )
    else:
        vip_category_id = vip_category_id[0]

    for cliente_elem in root.findall("cliente"):
        nombre = cliente_elem.findtext("nombre", "").strip()
        apellido = cliente_elem.findtext("apellido", "").strip()
        email = cliente_elem.findtext("email", "").strip()
        movil = cliente_elem.findtext("movil", "").strip()
        es_vip = cliente_elem.findtext("es_vip", "") == "1"

        if not nombre or not apellido or not email or not movil:
            print("Datos incompletos para un cliente. Omitido.")
            continue

        existe = models.execute_kw(
            db, uid, password, 'res.partner', 'search',
            [[['phone', '=', movil]]]
        )
        if existe:
            print("Cliente con movil", movil, "ya existe. Omitido.")
            continue

        valores = {
            'name': f"{nombre} {apellido}",
            'email': email,
            'phone': movil,
            'is_company': False,
            'type': 'contact'
        }

        if es_vip:
            valores['category_id'] = [(6, 0, [vip_category_id])]

        partner_id = models.execute_kw(db, uid, password, 'res.partner', 'create', [valores])
        print("Cliente", valores['name'], "insertado con ID:", partner_id)

except Exception as e:
    print("Error de conexion o insercion en Odoo:", e)
