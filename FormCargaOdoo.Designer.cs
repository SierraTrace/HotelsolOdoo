namespace HotelSOL
{
    partial class FormCargaOdoo
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnAlojamientos;
        private System.Windows.Forms.Button btnHabitaciones;
        private System.Windows.Forms.Button btnServicios;
        private System.Windows.Forms.Button btnReservas;
        private System.Windows.Forms.Button btnFacturas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnClientes = new Button();
            btnEmpleados = new Button();
            btnAlojamientos = new Button();
            btnHabitaciones = new Button();
            btnServicios = new Button();
            btnReservas = new Button();
            btnFacturas = new Button();
            SuspendLayout();
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(340, 69);
            btnClientes.Margin = new Padding(3, 2, 3, 2);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(175, 22);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "Cargar Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnEmpleados
            // 
            btnEmpleados.Location = new Point(340, 104);
            btnEmpleados.Margin = new Padding(3, 2, 3, 2);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.Size = new Size(175, 22);
            btnEmpleados.TabIndex = 1;
            btnEmpleados.Text = "Cargar Empleados";
            btnEmpleados.UseVisualStyleBackColor = true;
            btnEmpleados.Click += btnEmpleados_Click;
            // 
            // btnAlojamientos
            // 
            btnAlojamientos.Location = new Point(340, 144);
            btnAlojamientos.Margin = new Padding(3, 2, 3, 2);
            btnAlojamientos.Name = "btnAlojamientos";
            btnAlojamientos.Size = new Size(175, 22);
            btnAlojamientos.TabIndex = 2;
            btnAlojamientos.Text = "Cargar Alojamientos";
            btnAlojamientos.UseVisualStyleBackColor = true;
            btnAlojamientos.Click += btnAlojamientos_Click;
            // 
            // btnHabitaciones
            // 
            btnHabitaciones.Location = new Point(340, 184);
            btnHabitaciones.Margin = new Padding(3, 2, 3, 2);
            btnHabitaciones.Name = "btnHabitaciones";
            btnHabitaciones.Size = new Size(175, 22);
            btnHabitaciones.TabIndex = 3;
            btnHabitaciones.Text = "Cargar Habitaciones";
            btnHabitaciones.UseVisualStyleBackColor = true;
            btnHabitaciones.Click += btnHabitaciones_Click;
            // 
            // btnServicios
            // 
            btnServicios.Location = new Point(340, 227);
            btnServicios.Margin = new Padding(3, 2, 3, 2);
            btnServicios.Name = "btnServicios";
            btnServicios.Size = new Size(175, 22);
            btnServicios.TabIndex = 4;
            btnServicios.Text = "Cargar Servicios";
            btnServicios.UseVisualStyleBackColor = true;
            btnServicios.Click += btnServicios_Click;
            // 
            // btnReservas
            // 
            btnReservas.Location = new Point(340, 267);
            btnReservas.Margin = new Padding(3, 2, 3, 2);
            btnReservas.Name = "btnReservas";
            btnReservas.Size = new Size(175, 22);
            btnReservas.TabIndex = 5;
            btnReservas.Text = "Cargar Reservas";
            btnReservas.UseVisualStyleBackColor = true;
            btnReservas.Click += btnReservas_Click;
            // 
            // btnFacturas
            // 
            btnFacturas.Location = new Point(340, 308);
            btnFacturas.Margin = new Padding(3, 2, 3, 2);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.Size = new Size(175, 22);
            btnFacturas.TabIndex = 6;
            btnFacturas.Text = "Cargar Facturas";
            btnFacturas.UseVisualStyleBackColor = true;
            btnFacturas.Click += btnFacturas_Click;
            // 
            // FormCargaOdoo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(875, 525);
            Controls.Add(btnClientes);
            Controls.Add(btnEmpleados);
            Controls.Add(btnAlojamientos);
            Controls.Add(btnHabitaciones);
            Controls.Add(btnServicios);
            Controls.Add(btnReservas);
            Controls.Add(btnFacturas);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormCargaOdoo";
            Text = "Carga de Datos a Odoo";
            Load += FormCargaOdoo_Load;
            ResumeLayout(false);
        }
    }
}
