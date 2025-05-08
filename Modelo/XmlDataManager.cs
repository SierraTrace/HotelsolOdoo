using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace HotelSOL.Data
{
    public class XmlDataManager<T>
    {
        private string filePath;

        public XmlDataManager(string path)
        {
            filePath = path;
        }

        // Método para guardar datos en XML
        public void GuardarDatos(List<T> datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, datos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar datos en XML: {ex.Message}");
            }
        }

        // Método para leer datos desde XML
        public List<T> CargarDatos()
        {
            try
            {
                if (!File.Exists(filePath)) return new List<T>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (List<T>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer datos desde XML: {ex.Message}");
                return new List<T>();
            }
        }
    }
}

