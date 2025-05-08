
import unittest
import xml.etree.ElementTree as ET
from datetime import datetime

class TestReservasXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("reservas.xml")
        cls.root = tree.getroot()

    def test_reservas_campos_obligatorios(self):
        for r in self.root.findall("reserva"):
            cliente = r.find("cliente").text or ""
            email = r.find("email").text or ""
            habitacion = r.find("habitacion").text or ""

            self.assertTrue(cliente.strip(), "Reserva sin cliente.")
            self.assertTrue(email.strip(), "Reserva sin email.")
            self.assertTrue(habitacion.strip(), "Reserva sin habitacion.")

    def test_reservas_fechas_validas(self):
        for r in self.root.findall("reserva"):
            entrada = r.find("fecha_entrada").text or ""
            salida = r.find("fecha_salida").text or ""
            try:
                datetime.strptime(entrada, "%Y-%m-%d")
                datetime.strptime(salida, "%Y-%m-%d")
            except ValueError:
                self.fail(f"Fechas invalidas: entrada={entrada}, salida={salida}")

    def test_reservas_precio_valido(self):
        for r in self.root.findall("reserva"):
            precio = r.find("precio").text or ""
            try:
                self.assertGreaterEqual(float(precio), 0.0, f"Precio negativo o invalido: {precio}")
            except ValueError:
                self.fail(f"Precio invalido: {precio}")

if __name__ == "__main__":
    unittest.main()
