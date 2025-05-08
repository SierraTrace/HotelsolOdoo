import xmlrpc.client

# Configura conexion
url = "http://127.0.0.1:8071"
db = "hotelsol"
username = "esierrasanch@uoc.edu"
password = "HotelSol123"

common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
uid = common.authenticate(db, username, password, {})
models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

# Buscar empresas
empresas = models.execute_kw(
    db, uid, password,
    'res.company', 'search_read',
    [[]], {'fields': ['id', 'name']}
)

for e in empresas:
    print(f"Empresa: {e['name']} - ID: {e['id']}")
