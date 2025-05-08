import xmlrpc.client

def establecer():
    url = "http://127.0.0.1:8071"
    db = "hotelsol"
    username = "esierrasanch@uoc.edu"
    password = "HotelSol123"

    common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
    uid = common.authenticate(db, username, password, {})
    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

    cuenta_id = models.execute_kw(
        db, uid, password, 'account.account', 'search',
        [[['code', '=', '700000']]], {'limit': 1}
    )
    if not cuenta_id:
        raise Exception("No se encontro la cuenta 700000.")
    cuenta_ingresos_id = cuenta_id[0]
    print("Cuenta 700000 encontrada con ID:", cuenta_ingresos_id)

    categorias = models.execute_kw(
        db, uid, password, 'product.category', 'search_read',
        [[]], {'fields': ['id', 'name']}
    )

    for cat in categorias:
        models.execute_kw(
            db, uid, password, 'product.category', 'write',
            [[cat['id']], {'property_account_income_categ_id': cuenta_ingresos_id}]
        )
        print("Categoria actualizada:", cat['name'])

if __name__ == "__main__":
    establecer()
