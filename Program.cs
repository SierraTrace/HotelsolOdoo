using HotelSOL.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace HotelSOL.Client
{
    static class Program
    {
        public static HotelContext Context;

        [STAThread]
        static void Main()
        {
            AppContext.SetSwitch("System.Net.Security.SslStreamCertificateValidationIgnoreErrors", true);

            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseSqlServer("Server=localhost;Database=hotelsol;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True")
                .Options;

            Context = new HotelContext(options);

            try
            {
                
                Context.Database.EnsureCreated();
                PoblarDB.InicializarAsync(Context).Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error conectando o creando la base de datos:\n{ex.Message}", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());


        }
    }
}
