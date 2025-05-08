
import unittest
import xmlrpc.client

class TestFacturasOdoo(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        url = "http://127.0.0.1:8071"
        db = "hotelsol"
        username = "esierrasanch@uoc.edu"
        password = "HotelSol123"
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        cls.uid = common.authenticate(db, username, password, {})
        cls.models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")
        cls.db = db
        cls.password = password

    def test_facturas_tienen_lineas_y_cliente(self):
        facturas = self.models.execute_kw(
            self.db, self.uid, self.password,
            'account.move', 'search_read',
            [[('move_type', '=', 'out_invoice')]],
            {'fields': ['name', 'invoice_line_ids', 'partner_id']}
        )
        self.assertGreater(len(facturas), 0, "No hay facturas en Odoo.")
        for f in facturas:
            self.assertTrue(f['invoice_line_ids'], f"Factura sin lineas: {f['name']}")
            self.assertTrue(f['partner_id'], f"Factura sin cliente: {f['name']}")

if __name__ == "__main__":
    unittest.main()
