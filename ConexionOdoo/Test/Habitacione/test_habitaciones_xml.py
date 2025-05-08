
import unittest
import xml.etree.ElementTree as ET

class TestHabitacionesXML(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        tree = ET.parse("habitaciones.xml")
        cls.root = tree.getroot()

    def test_habitaciones_campos_requeridos(self):
        for hab in self.root.findall("habitacion"):
            numero = hab.find("numero").text or ""
            tipo = hab.find("tipo").text or ""
            capacidad = hab.find("capacidad").text or ""
            precio = hab.find("precio").text or ""
            disponibilidad = hab.find("disponibilidad").text or ""

            self.assertTrue(numero.strip(), "Habitacion sin numero.")
            self.assertTrue(tipo.strip(), f"Habitacion {numero} sin tipo.")
            self.assertTrue(capacidad.strip().isdigit(), f"Habitacion {numero} con capacidad invalida: {capacidad}")
            try:
                float(precio)
            except ValueError:
                self.fail(f"Habitacion {numero} con precio inválido: {precio}")
            self.assertIn(disponibilidad, ("True", "False"), f"Habitacion {numero} con disponibilidad invalida: {disponibilidad}")

if __name__ == "__main__":
    unittest.main()
