
import unittest
import xml.etree.ElementTree as ET

class TestAlojamientosXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("alojamientos_intercambio.xml")
        cls.root = tree.getroot()

    def test_alojamientos_tienen_tipo_nombre(self):
        for aloj in self.root.findall("alojamiento"):
            tipo_nombre = aloj.find("tipo_nombre").text or ""
            self.assertTrue(tipo_nombre.strip(), f"Alojamiento sin tipo_nombre: {ET.tostring(aloj, encoding='unicode')}")

    def test_alojamientos_tienen_precio_valido(self):
        for aloj in self.root.findall("alojamiento"):
            precio = aloj.find("precio_base").text or ""
            try:
                precio_float = float(precio)
                self.assertGreaterEqual(precio_float, 0.0, f"Precio negativo: {precio}")
            except ValueError:
                self.fail(f"Precio no numerico: {precio}")

if __name__ == "__main__":
    unittest.main()
