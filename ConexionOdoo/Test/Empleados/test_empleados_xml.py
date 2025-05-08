
import unittest
import xml.etree.ElementTree as ET
import re

def es_email_valido(email):
    return re.match(r"[^@]+@[^@]+\.[^@]+", email)

class TestEmpleadosXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("empleados.xml")
        cls.root = tree.getroot()

    def test_empleados_tienen_nombre(self):
        for emp in self.root.findall("empleado"):
            nombre = emp.find("nombre").text or ""
            self.assertTrue(nombre.strip(), f"Empleado sin nombre: {ET.tostring(emp, encoding='unicode')}")

    def test_empleados_email_valido(self):
        for emp in self.root.findall("empleado"):
            email = emp.find("email").text or ""
            self.assertTrue(es_email_valido(email), f"Email invalido: {email}")

    def test_empleados_tienen_departamento(self):
        for emp in self.root.findall("empleado"):
            depto = emp.find("departamento").text or ""
            self.assertTrue(depto.strip(), f"Departamento vacio: {ET.tostring(emp, encoding='unicode')}")

if __name__ == "__main__":
    unittest.main()
