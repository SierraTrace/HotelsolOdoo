
import unittest
import pyodbc
import xmlrpc.client

class TestHabitacionesOdooVsSQL(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        # SQL Server
        conn = pyodbc.connect(
            "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
        )
        cursor = conn.cursor()
        cursor.execute("SELECT Numero, Disponibilidad FROM Habitaciones")
        cls.habitaciones_sql = {row.Numero: row.Disponibilidad == 1 for row in cursor.fetchall()}
        conn.close()

        # Odoo
        url = "http://127.0.0.1:8071"
        db = "hotelsol"
        username = "esierrasanch@uoc.edu"
        password = "HotelSol123"
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        cls.uid = common.authenticate(db, username, password, {})
        cls.models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")
        cls.db = db
        cls.password = password

    def test_disponibilidad_y_cuenta_en_odoo(self):
        productos = self.models.execute_kw(
            self.db, self.uid, self.password,
            'product.template', 'search_read',
            [[('default_code', '!=', False), ('type', '=', 'service')]],
            {'fields': ['name', 'default_code', 'active', 'property_account_income_id']}
        )
        for p in productos:
            codigo = p['default_code']
            if not codigo.startswith("H"):  
                continue

            activo = p['active']
            cuenta = p['property_account_income_id']

            self.assertIn(codigo, self.habitaciones_sql, f"Codigo '{codigo}' no encontrado en SQL Server.")
            if codigo in self.habitaciones_sql:
                disponible_sql = self.habitaciones_sql[codigo]
                self.assertEqual(disponible_sql, activo, f"Disponibilidad no coincide para '{codigo}'")
            self.assertTrue(cuenta, f"Habitacion '{codigo}' sin cuenta contable en Odoo.")


if __name__ == "__main__":
    unittest.main()
