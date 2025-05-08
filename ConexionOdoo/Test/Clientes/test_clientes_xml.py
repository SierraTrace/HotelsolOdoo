
import unittest
import xml.etree.ElementTree as ET
import re

def es_email_valido(email):
    return re.match(r"[^@]+@[^@]+\.[^@]+", email)

class TestClientesXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("clientes_intercambio.xml")
        cls.root = tree.getroot()

    def test_clientes_tienen_nombre_y_apellido(self):
        for cliente in self.root.findall("cliente"):
            nombre = cliente.find("nombre").text or ""
            apellido = cliente.find("apellido").text or ""
            self.assertTrue(nombre.strip(), f"Cliente sin nombre: {ET.tostring(cliente, encoding='unicode')}")
            self.assertTrue(apellido.strip(), f"Cliente sin apellido: {ET.tostring(cliente, encoding='unicode')}")

    def test_clientes_email_valido(self):
        for cliente in self.root.findall("cliente"):
            email = cliente.find("email").text or ""
            self.assertTrue(es_email_valido(email), f"Email invalido: {email}")

if __name__ == "__main__":
    unittest.main()
