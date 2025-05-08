
import unittest
import os
import xml.etree.ElementTree as ET

class TestValidacionXMLs(unittest.TestCase):

    def validar_archivo_xml(self, ruta):
        try:
            tree = ET.parse(ruta)
            tree.getroot()
        except Exception as e:
            self.fail(f"Error en XML '{ruta}': {e}")

    def test_xmls_generados_son_validos(self):
        rutas = [
            "clientes_intercambio.xml",
            "empleados.xml",
            "habitaciones.xml",
            "alojamientos_intercambio.xml",
            "facturas2.xml",
            "reservas.xml",
            "servicios_intercambio.xml"
        ]
        for ruta in rutas:
            with self.subTest(xml=ruta):
                self.assertTrue(os.path.exists(ruta), f"Archivo no encontrado: {ruta}")
                self.validar_archivo_xml(ruta)

if __name__ == "__main__":
    unittest.main()
