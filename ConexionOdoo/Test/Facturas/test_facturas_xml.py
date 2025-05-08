
import unittest
import xml.etree.ElementTree as ET

class TestFacturasXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("facturas2.xml")
        cls.root = tree.getroot()

    def test_facturas_validas(self):
        for f in self.root.findall("factura"):
            cliente = f.find("cliente").text or ""
            email = f.find("email").text or ""
            fecha = f.find("fecha").text or ""
            lineas = f.find("lineas").findall("linea")

            self.assertTrue(cliente.strip(), "Factura sin cliente.")
            self.assertTrue(email.strip(), "Factura sin email.")
            self.assertTrue(fecha.strip(), "Factura sin fecha.")
            self.assertGreaterEqual(len(lineas), 1, f"Factura sin lineas: {f.find('id').text}")

            for l in lineas:
                servicio = l.find("servicio").text or ""
                cantidad = l.find("cantidad").text or ""
                precio = l.find("precio").text or ""

                self.assertTrue(servicio.strip(), "Linea sin nombre de servicio.")
                try:
                    float(cantidad)
                    float(precio)
                except ValueError:
                    self.fail(f"Linea con cantidad o precio invalido: {cantidad}, {precio}")

if __name__ == "__main__":
    unittest.main()
