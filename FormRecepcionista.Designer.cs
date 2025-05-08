// FormRecepcionista.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSOL.Client
{
    partial class FormRecepcionista
    {
        private IContainer components = null;
        private TabControl tabControl;
        private TabPage tabClientes, tabReservas, tabDisponibilidad, tabFacturas, tabServicios;

        // CLIENTES
        private Label lblNombreCliente, lblApellidoCliente, lblEmailCliente,
                      lblMovilCliente, lblPasswordCliente, lblEsVipCliente;
        private TextBox txtNombreCliente, txtApellidoCliente, txtEmailCliente,
                        txtMovilCliente, txtPasswordCliente;
        private ComboBox cmbEsVip;
        private Button btnCrearCliente, btnActualizarCliente, btnRefrescarClientes;
        private DataGridView dgvClientes;

        // RESERVAS
        private Label lblClienteReserva, lblTipoReserva, lblHabitacionReserva,
                      lblEstadoReserva, lblFechaEntrada, lblFechaSalida;
        private ComboBox cmbClienteId, cmbTipoReserva, cmbHabitacion, cmbEstado;
        private DateTimePicker dtpEntrada, dtpSalida;
        private Button btnCrearReserva, btnModificarReserva, btnAnularReserva;
        private DataGridView dgvReservas;

        // DISPONIBILIDAD
        private Label lblDisponEntrada, lblDisponSalida;
        private DateTimePicker dtpDisponEntrada, dtpDisponSalida;
        private Button btnConsultarDispon;
        private DataGridView dgvDisponibilidad;

        // FACTURACIÓN
        private Label lblClientesFacturaList, lblFacturas, lblDetalleFactura;
        private DataGridView dgvClientesFacturas, dgvFacturas;
        private Button btnCrearFactura, btnImprimirFactura;
        private TextBox txtDetalleFactura;

        // SERVICIOS
        private Label lblReservaServicio, lblServicio;
        private ComboBox cmbReservaServicio, cmbServicios;
        private Button btnSolicitarServicio;

        // CARGA ODOO
        private TabPage tabOdoo;
        private Panel pnlOdooContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabClientes = new TabPage();
            lblNombreCliente = new Label();
            txtNombreCliente = new TextBox();
            lblApellidoCliente = new Label();
            txtApellidoCliente = new TextBox();
            lblEmailCliente = new Label();
            txtEmailCliente = new TextBox();
            lblMovilCliente = new Label();
            txtMovilCliente = new TextBox();
            lblPasswordCliente = new Label();
            txtPasswordCliente = new TextBox();
            lblEsVipCliente = new Label();
            cmbEsVip = new ComboBox();
            btnCrearCliente = new Button();
            btnActualizarCliente = new Button();
            btnRefrescarClientes = new Button();
            dgvClientes = new DataGridView();
            tabReservas = new TabPage();
            lblClienteReserva = new Label();
            cmbClienteId = new ComboBox();
            lblTipoReserva = new Label();
            cmbTipoReserva = new ComboBox();
            lblHabitacionReserva = new Label();
            cmbHabitacion = new ComboBox();
            lblEstadoReserva = new Label();
            cmbEstado = new ComboBox();
            lblFechaEntrada = new Label();
            dtpEntrada = new DateTimePicker();
            lblFechaSalida = new Label();
            dtpSalida = new DateTimePicker();
            btnCrearReserva = new Button();
            btnModificarReserva = new Button();
            btnAnularReserva = new Button();
            dgvReservas = new DataGridView();
            tabDisponibilidad = new TabPage();
            lblDisponEntrada = new Label();
            dtpDisponEntrada = new DateTimePicker();
            lblDisponSalida = new Label();
            dtpDisponSalida = new DateTimePicker();
            btnConsultarDispon = new Button();
            dgvDisponibilidad = new DataGridView();
            tabFacturas = new TabPage();
            lblClientesFacturaList = new Label();
            dgvClientesFacturas = new DataGridView();
            lblFacturas = new Label();
            dgvFacturas = new DataGridView();
            btnCrearFactura = new Button();
            btnImprimirFactura = new Button();
            lblDetalleFactura = new Label();
            txtDetalleFactura = new TextBox();
            tabServicios = new TabPage();
            lblReservaServicio = new Label();
            cmbReservaServicio = new ComboBox();
            lblServicio = new Label();
            cmbServicios = new ComboBox();
            btnSolicitarServicio = new Button();
            tabOdoo = new TabPage();
            pnlOdooContainer = new Panel();
            tabControl.SuspendLayout();
            tabClientes.SuspendLayout();
            ((ISupportInitialize)dgvClientes).BeginInit();
            tabReservas.SuspendLayout();
            ((ISupportInitialize)dgvReservas).BeginInit();
            tabDisponibilidad.SuspendLayout();
            ((ISupportInitialize)dgvDisponibilidad).BeginInit();
            tabFacturas.SuspendLayout();
            ((ISupportInitialize)dgvClientesFacturas).BeginInit();
            ((ISupportInitialize)dgvFacturas).BeginInit();
            tabServicios.SuspendLayout();
            tabOdoo.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabClientes);
            tabControl.Controls.Add(tabReservas);
            tabControl.Controls.Add(tabDisponibilidad);
            tabControl.Controls.Add(tabFacturas);
            tabControl.Controls.Add(tabServicios);
            tabControl.Controls.Add(tabOdoo);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(800, 600);
            tabControl.TabIndex = 0;
            // 
            // tabClientes
            // 
            tabClientes.Controls.Add(lblNombreCliente);
            tabClientes.Controls.Add(txtNombreCliente);
            tabClientes.Controls.Add(lblApellidoCliente);
            tabClientes.Controls.Add(txtApellidoCliente);
            tabClientes.Controls.Add(lblEmailCliente);
            tabClientes.Controls.Add(txtEmailCliente);
            tabClientes.Controls.Add(lblMovilCliente);
            tabClientes.Controls.Add(txtMovilCliente);
            tabClientes.Controls.Add(lblPasswordCliente);
            tabClientes.Controls.Add(txtPasswordCliente);
            tabClientes.Controls.Add(lblEsVipCliente);
            tabClientes.Controls.Add(cmbEsVip);
            tabClientes.Controls.Add(btnCrearCliente);
            tabClientes.Controls.Add(btnActualizarCliente);
            tabClientes.Controls.Add(btnRefrescarClientes);
            tabClientes.Controls.Add(dgvClientes);
            tabClientes.Location = new Point(4, 26);
            tabClientes.Name = "tabClientes";
            tabClientes.Size = new Size(792, 570);
            tabClientes.TabIndex = 0;
            tabClientes.Text = "Clientes";
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.Location = new Point(225, 3);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Size = new Size(100, 23);
            lblNombreCliente.TabIndex = 0;
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Location = new Point(355, 3);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(100, 25);
            txtNombreCliente.TabIndex = 1;
            // 
            // lblApellidoCliente
            // 
            lblApellidoCliente.Location = new Point(225, 42);
            lblApellidoCliente.Name = "lblApellidoCliente";
            lblApellidoCliente.Size = new Size(100, 23);
            lblApellidoCliente.TabIndex = 2;
            // 
            // txtApellidoCliente
            // 
            txtApellidoCliente.Location = new Point(355, 42);
            txtApellidoCliente.Name = "txtApellidoCliente";
            txtApellidoCliente.Size = new Size(100, 25);
            txtApellidoCliente.TabIndex = 3;
            // 
            // lblEmailCliente
            // 
            lblEmailCliente.Location = new Point(225, 85);
            lblEmailCliente.Name = "lblEmailCliente";
            lblEmailCliente.Size = new Size(100, 23);
            lblEmailCliente.TabIndex = 4;
            // 
            // txtEmailCliente
            // 
            txtEmailCliente.Location = new Point(355, 85);
            txtEmailCliente.Name = "txtEmailCliente";
            txtEmailCliente.Size = new Size(100, 25);
            txtEmailCliente.TabIndex = 5;
            // 
            // lblMovilCliente
            // 
            lblMovilCliente.Location = new Point(225, 127);
            lblMovilCliente.Name = "lblMovilCliente";
            lblMovilCliente.Size = new Size(100, 23);
            lblMovilCliente.TabIndex = 6;
            // 
            // txtMovilCliente
            // 
            txtMovilCliente.Location = new Point(355, 127);
            txtMovilCliente.Name = "txtMovilCliente";
            txtMovilCliente.Size = new Size(100, 25);
            txtMovilCliente.TabIndex = 7;
            // 
            // lblPasswordCliente
            // 
            lblPasswordCliente.Location = new Point(225, 178);
            lblPasswordCliente.Name = "lblPasswordCliente";
            lblPasswordCliente.Size = new Size(100, 23);
            lblPasswordCliente.TabIndex = 8;
            // 
            // txtPasswordCliente
            // 
            txtPasswordCliente.Location = new Point(355, 178);
            txtPasswordCliente.Name = "txtPasswordCliente";
            txtPasswordCliente.Size = new Size(100, 25);
            txtPasswordCliente.TabIndex = 9;
            // 
            // lblEsVipCliente
            // 
            lblEsVipCliente.Location = new Point(225, 224);
            lblEsVipCliente.Name = "lblEsVipCliente";
            lblEsVipCliente.Size = new Size(100, 23);
            lblEsVipCliente.TabIndex = 10;
            // 
            // cmbEsVip
            // 
            cmbEsVip.Items.AddRange(new object[] { "Sí", "No" });
            cmbEsVip.Location = new Point(346, 224);
            cmbEsVip.Name = "cmbEsVip";
            cmbEsVip.Size = new Size(121, 25);
            cmbEsVip.TabIndex = 11;
            // 
            // btnCrearCliente
            // 
            btnCrearCliente.Location = new Point(142, 313);
            btnCrearCliente.Name = "btnCrearCliente";
            btnCrearCliente.Size = new Size(75, 23);
            btnCrearCliente.TabIndex = 12;
            // 
            // btnActualizarCliente
            // 
            btnActualizarCliente.Location = new Point(264, 313);
            btnActualizarCliente.Name = "btnActualizarCliente";
            btnActualizarCliente.Size = new Size(75, 23);
            btnActualizarCliente.TabIndex = 13;
            // 
            // btnRefrescarClientes
            // 
            btnRefrescarClientes.Location = new Point(380, 313);
            btnRefrescarClientes.Name = "btnRefrescarClientes";
            btnRefrescarClientes.Size = new Size(75, 23);
            btnRefrescarClientes.TabIndex = 14;
            // 
            // dgvClientes
            // 
            dgvClientes.Location = new Point(0, 0);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(240, 150);
            dgvClientes.TabIndex = 15;
            // 
            // tabReservas
            // 
            tabReservas.Controls.Add(lblClienteReserva);
            tabReservas.Controls.Add(cmbClienteId);
            tabReservas.Controls.Add(lblTipoReserva);
            tabReservas.Controls.Add(cmbTipoReserva);
            tabReservas.Controls.Add(lblHabitacionReserva);
            tabReservas.Controls.Add(cmbHabitacion);
            tabReservas.Controls.Add(lblEstadoReserva);
            tabReservas.Controls.Add(cmbEstado);
            tabReservas.Controls.Add(lblFechaEntrada);
            tabReservas.Controls.Add(dtpEntrada);
            tabReservas.Controls.Add(lblFechaSalida);
            tabReservas.Controls.Add(dtpSalida);
            tabReservas.Controls.Add(btnCrearReserva);
            tabReservas.Controls.Add(btnModificarReserva);
            tabReservas.Controls.Add(btnAnularReserva);
            tabReservas.Controls.Add(dgvReservas);
            tabReservas.Location = new Point(4, 26);
            tabReservas.Name = "tabReservas";
            tabReservas.Size = new Size(792, 570);
            tabReservas.TabIndex = 1;
            tabReservas.Text = "Reservas";
            // 
            // lblClienteReserva
            // 
            lblClienteReserva.Location = new Point(219, 6);
            lblClienteReserva.Name = "lblClienteReserva";
            lblClienteReserva.Size = new Size(100, 23);
            lblClienteReserva.TabIndex = 0;
            // 
            // cmbClienteId
            // 
            cmbClienteId.Location = new Point(378, 49);
            cmbClienteId.Name = "cmbClienteId";
            cmbClienteId.Size = new Size(121, 25);
            cmbClienteId.TabIndex = 1;
            // 
            // lblTipoReserva
            // 
            lblTipoReserva.Location = new Point(219, 52);
            lblTipoReserva.Name = "lblTipoReserva";
            lblTipoReserva.Size = new Size(100, 23);
            lblTipoReserva.TabIndex = 2;
            // 
            // cmbTipoReserva
            // 
            cmbTipoReserva.Location = new Point(246, 99);
            cmbTipoReserva.Name = "cmbTipoReserva";
            cmbTipoReserva.Size = new Size(121, 25);
            cmbTipoReserva.TabIndex = 3;
            // 
            // lblHabitacionReserva
            // 
            lblHabitacionReserva.Location = new Point(399, 99);
            lblHabitacionReserva.Name = "lblHabitacionReserva";
            lblHabitacionReserva.Size = new Size(100, 23);
            lblHabitacionReserva.TabIndex = 4;
            // 
            // cmbHabitacion
            // 
            cmbHabitacion.Location = new Point(246, 153);
            cmbHabitacion.Name = "cmbHabitacion";
            cmbHabitacion.Size = new Size(121, 25);
            cmbHabitacion.TabIndex = 5;
            // 
            // lblEstadoReserva
            // 
            lblEstadoReserva.Location = new Point(409, 153);
            lblEstadoReserva.Name = "lblEstadoReserva";
            lblEstadoReserva.Size = new Size(100, 23);
            lblEstadoReserva.TabIndex = 6;
            // 
            // cmbEstado
            // 
            cmbEstado.Location = new Point(246, 202);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(121, 25);
            cmbEstado.TabIndex = 7;
            // 
            // lblFechaEntrada
            // 
            lblFechaEntrada.Location = new Point(399, 202);
            lblFechaEntrada.Name = "lblFechaEntrada";
            lblFechaEntrada.Size = new Size(100, 23);
            lblFechaEntrada.TabIndex = 8;
            // 
            // dtpEntrada
            // 
            dtpEntrada.Location = new Point(378, 6);
            dtpEntrada.Name = "dtpEntrada";
            dtpEntrada.Size = new Size(200, 25);
            dtpEntrada.TabIndex = 9;
            // 
            // lblFechaSalida
            // 
            lblFechaSalida.Location = new Point(246, 248);
            lblFechaSalida.Name = "lblFechaSalida";
            lblFechaSalida.Size = new Size(100, 23);
            lblFechaSalida.TabIndex = 10;
            // 
            // dtpSalida
            // 
            dtpSalida.Location = new Point(431, 248);
            dtpSalida.Name = "dtpSalida";
            dtpSalida.Size = new Size(200, 25);
            dtpSalida.TabIndex = 11;
            // 
            // btnCrearReserva
            // 
            btnCrearReserva.Location = new Point(131, 347);
            btnCrearReserva.Name = "btnCrearReserva";
            btnCrearReserva.Size = new Size(75, 23);
            btnCrearReserva.TabIndex = 12;
            // 
            // btnModificarReserva
            // 
            btnModificarReserva.Location = new Point(271, 347);
            btnModificarReserva.Name = "btnModificarReserva";
            btnModificarReserva.Size = new Size(75, 23);
            btnModificarReserva.TabIndex = 13;
            // 
            // btnAnularReserva
            // 
            btnAnularReserva.Location = new Point(409, 347);
            btnAnularReserva.Name = "btnAnularReserva";
            btnAnularReserva.Size = new Size(75, 23);
            btnAnularReserva.TabIndex = 14;
            // 
            // dgvReservas
            // 
            dgvReservas.Location = new Point(0, 0);
            dgvReservas.Name = "dgvReservas";
            dgvReservas.Size = new Size(240, 150);
            dgvReservas.TabIndex = 15;
            // 
            // tabDisponibilidad
            // 
            tabDisponibilidad.Controls.Add(lblDisponEntrada);
            tabDisponibilidad.Controls.Add(dtpDisponEntrada);
            tabDisponibilidad.Controls.Add(lblDisponSalida);
            tabDisponibilidad.Controls.Add(dtpDisponSalida);
            tabDisponibilidad.Controls.Add(btnConsultarDispon);
            tabDisponibilidad.Controls.Add(dgvDisponibilidad);
            tabDisponibilidad.Location = new Point(4, 26);
            tabDisponibilidad.Name = "tabDisponibilidad";
            tabDisponibilidad.Size = new Size(792, 570);
            tabDisponibilidad.TabIndex = 2;
            tabDisponibilidad.Text = "Disponibilidad";
            // 
            // lblDisponEntrada
            // 
            lblDisponEntrada.Location = new Point(222, 6);
            lblDisponEntrada.Name = "lblDisponEntrada";
            lblDisponEntrada.Size = new Size(100, 23);
            lblDisponEntrada.TabIndex = 0;
            // 
            // dtpDisponEntrada
            // 
            dtpDisponEntrada.Location = new Point(337, 6);
            dtpDisponEntrada.Name = "dtpDisponEntrada";
            dtpDisponEntrada.Size = new Size(200, 25);
            dtpDisponEntrada.TabIndex = 1;
            // 
            // lblDisponSalida
            // 
            lblDisponSalida.Location = new Point(222, 46);
            lblDisponSalida.Name = "lblDisponSalida";
            lblDisponSalida.Size = new Size(100, 23);
            lblDisponSalida.TabIndex = 2;
            // 
            // dtpDisponSalida
            // 
            dtpDisponSalida.Location = new Point(358, 46);
            dtpDisponSalida.Name = "dtpDisponSalida";
            dtpDisponSalida.Size = new Size(200, 25);
            dtpDisponSalida.TabIndex = 3;
            // 
            // btnConsultarDispon
            // 
            btnConsultarDispon.Location = new Point(263, 127);
            btnConsultarDispon.Name = "btnConsultarDispon";
            btnConsultarDispon.Size = new Size(75, 23);
            btnConsultarDispon.TabIndex = 4;
            // 
            // dgvDisponibilidad
            // 
            dgvDisponibilidad.Location = new Point(0, 0);
            dgvDisponibilidad.Name = "dgvDisponibilidad";
            dgvDisponibilidad.Size = new Size(240, 150);
            dgvDisponibilidad.TabIndex = 5;
            // 
            // tabFacturas
            // 
            tabFacturas.Controls.Add(lblClientesFacturaList);
            tabFacturas.Controls.Add(dgvClientesFacturas);
            tabFacturas.Controls.Add(lblFacturas);
            tabFacturas.Controls.Add(dgvFacturas);
            tabFacturas.Controls.Add(btnCrearFactura);
            tabFacturas.Controls.Add(btnImprimirFactura);
            tabFacturas.Controls.Add(lblDetalleFactura);
            tabFacturas.Controls.Add(txtDetalleFactura);
            tabFacturas.Location = new Point(4, 26);
            tabFacturas.Name = "tabFacturas";
            tabFacturas.Size = new Size(792, 570);
            tabFacturas.TabIndex = 3;
            tabFacturas.Text = "Facturación";
            // 
            // lblClientesFacturaList
            // 
            lblClientesFacturaList.Location = new Point(220, 3);
            lblClientesFacturaList.Name = "lblClientesFacturaList";
            lblClientesFacturaList.Size = new Size(100, 23);
            lblClientesFacturaList.TabIndex = 0;
            // 
            // dgvClientesFacturas
            // 
            dgvClientesFacturas.Location = new Point(0, 0);
            dgvClientesFacturas.Name = "dgvClientesFacturas";
            dgvClientesFacturas.Size = new Size(240, 150);
            dgvClientesFacturas.TabIndex = 1;
            // 
            // lblFacturas
            // 
            lblFacturas.Location = new Point(0, 0);
            lblFacturas.Name = "lblFacturas";
            lblFacturas.Size = new Size(100, 23);
            lblFacturas.TabIndex = 2;
            // 
            // dgvFacturas
            // 
            dgvFacturas.Location = new Point(0, 0);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.Size = new Size(240, 150);
            dgvFacturas.TabIndex = 3;
            // 
            // btnCrearFactura
            // 
            btnCrearFactura.Location = new Point(0, 0);
            btnCrearFactura.Name = "btnCrearFactura";
            btnCrearFactura.Size = new Size(75, 23);
            btnCrearFactura.TabIndex = 4;
            // 
            // btnImprimirFactura
            // 
            btnImprimirFactura.Location = new Point(0, 0);
            btnImprimirFactura.Name = "btnImprimirFactura";
            btnImprimirFactura.Size = new Size(75, 23);
            btnImprimirFactura.TabIndex = 5;
            // 
            // lblDetalleFactura
            // 
            lblDetalleFactura.Location = new Point(0, 0);
            lblDetalleFactura.Name = "lblDetalleFactura";
            lblDetalleFactura.Size = new Size(100, 23);
            lblDetalleFactura.TabIndex = 6;
            // 
            // txtDetalleFactura
            // 
            txtDetalleFactura.Location = new Point(0, 0);
            txtDetalleFactura.Name = "txtDetalleFactura";
            txtDetalleFactura.Size = new Size(100, 25);
            txtDetalleFactura.TabIndex = 7;
            // 
            // tabServicios
            // 
            tabServicios.Controls.Add(lblReservaServicio);
            tabServicios.Controls.Add(cmbReservaServicio);
            tabServicios.Controls.Add(lblServicio);
            tabServicios.Controls.Add(cmbServicios);
            tabServicios.Controls.Add(btnSolicitarServicio);
            tabServicios.Location = new Point(4, 26);
            tabServicios.Name = "tabServicios";
            tabServicios.Size = new Size(792, 570);
            tabServicios.TabIndex = 4;
            tabServicios.Text = "Servicios";
            // 
            // lblReservaServicio
            // 
            lblReservaServicio.Location = new Point(225, 12);
            lblReservaServicio.Name = "lblReservaServicio";
            lblReservaServicio.Size = new Size(100, 23);
            lblReservaServicio.TabIndex = 0;
            // 
            // cmbReservaServicio
            // 
            cmbReservaServicio.Location = new Point(331, 12);
            cmbReservaServicio.Name = "cmbReservaServicio";
            cmbReservaServicio.Size = new Size(121, 25);
            cmbReservaServicio.TabIndex = 1;
            // 
            // lblServicio
            // 
            lblServicio.Location = new Point(225, 68);
            lblServicio.Name = "lblServicio";
            lblServicio.Size = new Size(100, 23);
            lblServicio.TabIndex = 2;
            // 
            // cmbServicios
            // 
            cmbServicios.Location = new Point(331, 68);
            cmbServicios.Name = "cmbServicios";
            cmbServicios.Size = new Size(121, 25);
            cmbServicios.TabIndex = 3;
            // 
            // btnSolicitarServicio
            // 
            btnSolicitarServicio.Location = new Point(280, 147);
            btnSolicitarServicio.Name = "btnSolicitarServicio";
            btnSolicitarServicio.Size = new Size(75, 23);
            btnSolicitarServicio.TabIndex = 4;
            // 
            // tabOdoo
            // 
            tabOdoo.Controls.Add(pnlOdooContainer);
            tabOdoo.Location = new Point(4, 26);
            tabOdoo.Name = "tabOdoo";
            tabOdoo.Size = new Size(792, 570);
            tabOdoo.TabIndex = 5;
            tabOdoo.Text = "Carga Odoo";
            // 
            // pnlOdooContainer
            // 
            pnlOdooContainer.Dock = DockStyle.Fill;
            pnlOdooContainer.Location = new Point(0, 0);
            pnlOdooContainer.Name = "pnlOdooContainer";
            pnlOdooContainer.Size = new Size(792, 570);
            pnlOdooContainer.TabIndex = 0;
            // 
            // FormRecepcionista
            // 
            ClientSize = new Size(800, 600);
            Controls.Add(tabControl);
            Name = "FormRecepcionista";
            Text = "Panel de Recepcionista";
            tabControl.ResumeLayout(false);
            tabClientes.ResumeLayout(false);
            tabClientes.PerformLayout();
            ((ISupportInitialize)dgvClientes).EndInit();
            tabReservas.ResumeLayout(false);
            ((ISupportInitialize)dgvReservas).EndInit();
            tabDisponibilidad.ResumeLayout(false);
            ((ISupportInitialize)dgvDisponibilidad).EndInit();
            tabFacturas.ResumeLayout(false);
            tabFacturas.PerformLayout();
            ((ISupportInitialize)dgvClientesFacturas).EndInit();
            ((ISupportInitialize)dgvFacturas).EndInit();
            tabServicios.ResumeLayout(false);
            tabOdoo.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
