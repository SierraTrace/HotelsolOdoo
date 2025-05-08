
import unittest
import xmlrpc.client

class TestEmpleadosOdoo(unittest.TestCase):

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

    def test_empleados_tienen_departamento(self):
        empleados = self.models.execute_kw(
            self.db, self.uid, self.password,
            'hr.employee', 'search_read',
            [[]],
            {'fields': ['name', 'work_email', 'department_id']}
        )
        self.assertGreater(len(empleados), 0, "No se encontraron empleados en Odoo.")
        for emp in empleados:
            self.assertTrue(emp['department_id'], f"Empleado '{emp['name']}' sin departamento asignado.")

if __name__ == "__main__":
    unittest.main()
