import xml.etree.ElementTree as ET
import xmlrpc.client

try:
    tree = ET.parse("reservas.xml")
    root = tree.getroot()
except Exception as e:
    print("Error leyendo reservas.xml:", e)
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

    for r in root.findall("reserva"):
        cliente = r.find("cliente").text
        email = r.find("email").text
        habitacion = r.find("habitacion").text
        entrada = r.find("fecha_entrada").text
        salida = r.find("fecha_salida").text
        precio = r.find("precio").text

        name = f"Reserva de {habitacion} para {cliente}"
        description = (
            f"Cliente: {cliente} ({email})\n"
            f"Habitacion: {habitacion}\n"
            f"Precio por noche: {precio}"
        )

        eventos = models.execute_kw(
            db, uid, password, 'calendar.event', 'search',
            [[['name', '=', name], ['start', '=', entrada]]]
        )
        if eventos:
            print("Evento ya existe:", name)
            continue

        evento_id = models.execute_kw(
            db, uid, password, 'calendar.event', 'create',
            [{
                'name': name,
                'start': entrada,
                'stop': salida,
                'description': description
            }]
        )
        print("Evento creado:", name, "ID:", evento_id)

except Exception as e:
    print("Error creando reservas en Odoo:", e)
