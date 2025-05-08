using System;
using System.Windows.Forms;
using HotelSOL.Client;

namespace HotelSOL
{
    public partial class FormCargaOdoo : Form
    {
        public FormCargaOdoo()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Clientes\migrar_clientes.py");
            ScriptRunner.EjecutarScriptPython(@"Clientes\carga_clientes.py");
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Empleados\migrar_empleados.py");
            ScriptRunner.EjecutarScriptPython(@"Empleados\carga_empleados.py");
        }

        private void btnAlojamientos_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Alojamientos\migrar_alojamientos.py");
            ScriptRunner.EjecutarScriptPython(@"Alojamientos\carga_alojamientos.py");
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Habitaciones\migrar_habitaciones.py");
            ScriptRunner.EjecutarScriptPython(@"Habitaciones\carga_habitaciones.py");
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Servicios\migrar_servicios.py");
            ScriptRunner.EjecutarScriptPython(@"Servicios\carga_servicios.py");
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Reservas\migrar_reservas.py");
            ScriptRunner.EjecutarScriptPython(@"Reservas\carga_reservas.py");
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            ScriptRunner.EjecutarScriptPython(@"Facturas\migracion_facturas.py");
            ScriptRunner.EjecutarScriptPython(@"Facturas\carga_facturas.py");
        }

        private void FormCargaOdoo_Load(object sender, EventArgs e)
        {

        }
    }
}
