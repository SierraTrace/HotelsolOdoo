import unittest
import pyodbc

class TestSQLServerConnection(unittest.TestCase):

    def test_conexion_sql_server(self):
        try:
            conn = pyodbc.connect(
                "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=hotelsol;Trusted_Connection=yes;"
            )
            cursor = conn.cursor()
            cursor.execute("SELECT 1")
            result = cursor.fetchone()
            self.assertIsNotNone(result, "No se obtuvo ningun resultado del SELECT.")
            self.assertEqual(result[0], 1, "El resultado del SELECT no fue 1.")
        except Exception as e:
            self.fail(f"Error de conexion a SQL Server: {e}")
        finally:
            try:
                conn.close()
            except:
                pass

if __name__ == '__main__':
    unittest.main()
