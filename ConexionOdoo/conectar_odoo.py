import xmlrpc.client

# Datos de tu instancia Odoo
url = 'http://127.0.0.1:8071'   # Cambiar al puerto que se esté utilizando
db = 'hotelsol'
username = 'esierrasanch@uoc.edu' # Cambiar a tu coreo Uoc
password = 'HotelSol123'        # Cambiar a la contraseña que le hayas puesto al usuario.

# Conexión al servidor Odoo
common = xmlrpc.client.ServerProxy(f'{url}/xmlrpc/2/common')

# Autenticación
uid = common.authenticate(db, username, password, {})
if not uid:
    print("Error: no se pudo autenticar con Odoo.")
    exit()
print(f"Conexión exitosa - UID: {uid}")

# Conexión a los modelos
models = xmlrpc.client.ServerProxy(f'{url}/xmlrpc/2/object')

# Ejemplo: leer los primeros 5 contactos
resultados = models.execute_kw(
    db, uid, password,
    'res.partner', 'search_read',
    [[]],  # sin filtros
    {'fields': ['name', 'email'], 'limit': 5}
)

print("\n Contactos encontrados:")
for r in resultados:
    print(r)
