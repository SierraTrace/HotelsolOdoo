
import unittest
import xmlrpc.client

class TestClientesOdoo(unittest.TestCase):

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

    def test_clientes_vip_tienen_categoria(self):
        partners = self.models.execute_kw(
            self.db, self.uid, self.password,
            'res.partner', 'search_read',
            [[['type', '=', 'contact']]],
            {'fields': ['name', 'email', 'type', 'category_id']}
        )
        self.assertGreater(len(partners), 0, "No se encontraron contactos en Odoo.")
        for p in partners:
            if "vip" in p['name'].lower():
                self.assertTrue(p['category_id'], f"Cliente VIP sin categoria: {p['name']}")

if __name__ == "__main__":
    unittest.main()
