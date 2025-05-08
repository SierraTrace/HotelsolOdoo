
import unittest
import xmlrpc.client

class TestConexionOdoo(unittest.TestCase):

    def test_conexion_odoo_exitosa(self):
        url = "http://127.0.0.1:8071"
        db = "hotelsol"
        username = "esierrasanch@uoc.edu"
        password = "HotelSol123"

        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        uid = common.authenticate(db, username, password, {})
        self.assertIsNotNone(uid, "La autenticacion a Odoo fallo.")

if __name__ == "__main__":
    unittest.main()
