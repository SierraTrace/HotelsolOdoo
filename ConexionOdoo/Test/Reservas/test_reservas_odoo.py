
import unittest
import xmlrpc.client

class TestReservasOdoo(unittest.TestCase):

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

    def test_eventos_reserva_validos(self):
        eventos = self.models.execute_kw(
            self.db, self.uid, self.password,
            'calendar.event', 'search_read',
            [[['name', 'ilike', 'Reserva de']]],
            {'fields': ['name', 'start', 'stop', 'description']}
        )
        self.assertGreater(len(eventos), 0, "No hay eventos de reserva en Odoo.")
        for e in eventos:
            self.assertTrue(e['start'] and e['stop'], f"Evento sin fechas: {e['name']}")
            self.assertIn("Habitacion", e['description'], f"Descripcion incompleta: {e['name']}")

if __name__ == "__main__":
    unittest.main()
