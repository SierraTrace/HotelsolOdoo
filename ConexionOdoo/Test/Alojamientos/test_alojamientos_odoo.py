
import unittest
import xml.etree.ElementTree as ET
import xmlrpc.client

class TestAlojamientosOdoo(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        # Cargar nombres desde el XML
        tree = ET.parse("alojamientos_intercambio.xml")
        root = tree.getroot()
        cls.nombres_xml = set(aloj.find("tipo_nombre").text.strip() for aloj in root.findall("alojamiento"))

        # Conexión a Odoo
        url = "http://127.0.0.1:8071"
        db = "hotelsol"
        username = "esierrasanch@uoc.edu"
        password = "HotelSol123"
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        cls.uid = common.authenticate(db, username, password, {})
        cls.models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")
        cls.db = db
        cls.password = password

    def test_alojamientos_existen_en_odoo(self):
        productos = self.models.execute_kw(
            self.db, self.uid, self.password,
            'product.template', 'search_read',
            [[('type', '=', 'service')]],
            {'fields': ['name']}
        )
        nombres_odoo = [p['name'].strip() for p in productos]

        for nombre in self.nombres_xml:
            coincidencias = [n for n in nombres_odoo if nombre in n]
            self.assertGreaterEqual(len(coincidencias), 1, f"Alojamiento '{nombre}' no encontrado en Odoo.")

if __name__ == "__main__":
    unittest.main()
