namespace HotelSOL.Client
{
    partial class FormCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCliente));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDisponibilidad = new System.Windows.Forms.TabPage();
            this.dgvDisponibilidad = new System.Windows.Forms.DataGridView();
            this.cmbCalendarios = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbAlojamientos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTipoReserva = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReservar = new System.Windows.Forms.Button();
            this.txtPrecioSeleccionado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHabitacionSeleccionada = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscarDispon = new System.Windows.Forms.Button();
            this.dtpDisponSalida = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDisponEntrada = new System.Windows.Forms.DateTimePicker();
            this.lblFechaEntrada = new System.Windows.Forms.Label();
            this.tabMisReservas = new System.Windows.Forms.TabPage();
            this.dgvMisReservas = new System.Windows.Forms.DataGridView();
            this.btnConfirmarReserva = new System.Windows.Forms.Button();
            this.btnCancelarReserva = new System.Windows.Forms.Button();
            this.tabServicios = new System.Windows.Forms.TabPage();
            this.dgvServiciosReserva = new System.Windows.Forms.DataGridView();
            this.lblReservaSeleccionada = new System.Windows.Forms.Label();
            this.btnSolicitarServicio = new System.Windows.Forms.Button();
            this.cmbServicios = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbReservaServicio = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabFacturas = new System.Windows.Forms.TabPage();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.btnVerDetalleFactura = new System.Windows.Forms.Button();
            this.btnPagarFactura = new System.Windows.Forms.Button();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabDisponibilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).BeginInit();
            this.tabMisReservas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisReservas)).BeginInit();
            this.tabServicios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosReserva)).BeginInit();
            this.tabFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabDisponibilidad);
            this.tabControl1.Controls.Add(this.tabMisReservas);
            this.tabControl1.Controls.Add(this.tabServicios);
            this.tabControl1.Controls.Add(this.tabFacturas);
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(976, 626);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDisponibilidad
            // 
            this.tabDisponibilidad.Controls.Add(this.dgvDisponibilidad);
            this.tabDisponibilidad.Controls.Add(this.cmbCalendarios);
            this.tabDisponibilidad.Controls.Add(this.label8);
            this.tabDisponibilidad.Controls.Add(this.cmbAlojamientos);
            this.tabDisponibilidad.Controls.Add(this.label7);
            this.tabDisponibilidad.Controls.Add(this.cmbTipoReserva);
            this.tabDisponibilidad.Controls.Add(this.label6);
            this.tabDisponibilidad.Controls.Add(this.btnReservar);
            this.tabDisponibilidad.Controls.Add(this.txtPrecioSeleccionado);
            this.tabDisponibilidad.Controls.Add(this.label3);
            this.tabDisponibilidad.Controls.Add(this.txtHabitacionSeleccionada);
            this.tabDisponibilidad.Controls.Add(this.label2);
            this.tabDisponibilidad.Controls.Add(this.btnBuscarDispon);
            this.tabDisponibilidad.Controls.Add(this.dtpDisponSalida);
            this.tabDisponibilidad.Controls.Add(this.label1);
            this.tabDisponibilidad.Controls.Add(this.dtpDisponEntrada);
            this.tabDisponibilidad.Controls.Add(this.lblFechaEntrada);
            this.tabDisponibilidad.Location = new System.Drawing.Point(4, 29);
            this.tabDisponibilidad.Name = "tabDisponibilidad";
            this.tabDisponibilidad.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisponibilidad.Size = new System.Drawing.Size(968, 593);
            this.tabDisponibilidad.TabIndex = 0;
            this.tabDisponibilidad.Text = "Disponibilidad";
            this.tabDisponibilidad.UseVisualStyleBackColor = true;
            // 
            // dgvDisponibilidad
            // 
            this.dgvDisponibilidad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisponibilidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisponibilidad.Location = new System.Drawing.Point(20, 180);
            this.dgvDisponibilidad.Name = "dgvDisponibilidad";
            this.dgvDisponibilidad.ReadOnly = true;
            this.dgvDisponibilidad.RowHeadersWidth = 51;
            this.dgvDisponibilidad.RowTemplate.Height = 29;
            this.dgvDisponibilidad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisponibilidad.Size = new System.Drawing.Size(930, 400);
            this.dgvDisponibilidad.TabIndex = 0;
            this.dgvDisponibilidad.SelectionChanged += new System.EventHandler(this.DgvDisponibilidad_SelectionChanged);
            // 
            // cmbCalendarios
            // 
            this.cmbCalendarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCalendarios.FormattingEnabled = true;
            this.cmbCalendarios.Location = new System.Drawing.Point(680, 120);
            this.cmbCalendarios.Name = "cmbCalendarios";
            this.cmbCalendarios.Size = new System.Drawing.Size(200, 28);
            this.cmbCalendarios.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(680, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Calendario:";
            // 
            // cmbAlojamientos
            // 
            this.cmbAlojamientos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlojamientos.FormattingEnabled = true;
            this.cmbAlojamientos.Location = new System.Drawing.Point(350, 120);
            this.cmbAlojamientos.Name = "cmbAlojamientos";
            this.cmbAlojamientos.Size = new System.Drawing.Size(300, 28);
            this.cmbAlojamientos.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Alojamiento:";
            // 
            // cmbTipoReserva
            // 
            this.cmbTipoReserva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoReserva.FormattingEnabled = true;
            this.cmbTipoReserva.Location = new System.Drawing.Point(20, 120);
            this.cmbTipoReserva.Name = "cmbTipoReserva";
            this.cmbTipoReserva.Size = new System.Drawing.Size(300, 28);
            this.cmbTipoReserva.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Tipo Reserva:";
            // 
            // btnReservar
            // 
            this.btnReservar.BackColor = System.Drawing.Color.Teal;
            this.btnReservar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReservar.ForeColor = System.Drawing.Color.White;
            this.btnReservar.Location = new System.Drawing.Point(800, 120);
            this.btnReservar.Name = "btnReservar";
            this.btnReservar.Size = new System.Drawing.Size(140, 40);
            this.btnReservar.TabIndex = 14;
            this.btnReservar.Text = "Reservar";
            this.btnReservar.UseVisualStyleBackColor = false;
            this.btnReservar.Click += new System.EventHandler(this.btnReservar_Click);
            // 
            // txtPrecioSeleccionado
            // 
            this.txtPrecioSeleccionado.Location = new System.Drawing.Point(650, 40);
            this.txtPrecioSeleccionado.Name = "txtPrecioSeleccionado";
            this.txtPrecioSeleccionado.ReadOnly = true;
            this.txtPrecioSeleccionado.Size = new System.Drawing.Size(100, 27);
            this.txtPrecioSeleccionado.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(550, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Precio/Noche:";
            // 
            // txtHabitacionSeleccionada
            // 
            this.txtHabitacionSeleccionada.Location = new System.Drawing.Point(650, 10);
            this.txtHabitacionSeleccionada.Name = "txtHabitacionSeleccionada";
            this.txtHabitacionSeleccionada.ReadOnly = true;
            this.txtHabitacionSeleccionada.Size = new System.Drawing.Size(100, 27);
            this.txtHabitacionSeleccionada.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Habitación:";
            // 
            // btnBuscarDispon
            // 
            this.btnBuscarDispon.BackColor = System.Drawing.Color.Teal;
            this.btnBuscarDispon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarDispon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBuscarDispon.ForeColor = System.Drawing.Color.White;
            this.btnBuscarDispon.Location = new System.Drawing.Point(350, 40);
            this.btnBuscarDispon.Name = "btnBuscarDispon";
            this.btnBuscarDispon.Size = new System.Drawing.Size(140, 40);
            this.btnBuscarDispon.TabIndex = 9;
            this.btnBuscarDispon.Text = "Buscar";
            this.btnBuscarDispon.UseVisualStyleBackColor = false;
            this.btnBuscarDispon.Click += new System.EventHandler(this.btnBuscarDispon_Click);
            // 
            // dtpDisponSalida
            // 
            this.dtpDisponSalida.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDisponSalida.Location = new System.Drawing.Point(200, 40);
            this.dtpDisponSalida.Name = "dtpDisponSalida";
            this.dtpDisponSalida.Size = new System.Drawing.Size(130, 27);
            this.dtpDisponSalida.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Salida:";
            // 
            // dtpDisponEntrada
            // 
            this.dtpDisponEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDisponEntrada.Location = new System.Drawing.Point(20, 40);
            this.dtpDisponEntrada.Name = "dtpDisponEntrada";
            this.dtpDisponEntrada.Size = new System.Drawing.Size(130, 27);
            this.dtpDisponEntrada.TabIndex = 6;
            // 
            // lblFechaEntrada
            // 
            this.lblFechaEntrada.AutoSize = true;
            this.lblFechaEntrada.Location = new System.Drawing.Point(20, 10);
            this.lblFechaEntrada.Name = "lblFechaEntrada";
            this.lblFechaEntrada.Size = new System.Drawing.Size(60, 20);
            this.lblFechaEntrada.TabIndex = 5;
            this.lblFechaEntrada.Text = "Entrada:";
            // 
            // tabMisReservas
            // 
            this.tabMisReservas.Controls.Add(this.dgvMisReservas);
            this.tabMisReservas.Controls.Add(this.btnConfirmarReserva);
            this.tabMisReservas.Controls.Add(this.btnCancelarReserva);
            this.tabMisReservas.Location = new System.Drawing.Point(4, 29);
            this.tabMisReservas.Name = "tabMisReservas";
            this.tabMisReservas.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisReservas.Size = new System.Drawing.Size(968, 593);
            this.tabMisReservas.TabIndex = 1;
            this.tabMisReservas.Text = "Mis Reservas";
            this.tabMisReservas.UseVisualStyleBackColor = true;
            // 
            // dgvMisReservas
            // 
            this.dgvMisReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMisReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisReservas.Location = new System.Drawing.Point(20, 20);
            this.dgvMisReservas.Name = "dgvMisReservas";
            this.dgvMisReservas.ReadOnly = true;
            this.dgvMisReservas.RowHeadersWidth = 51;
            this.dgvMisReservas.RowTemplate.Height = 29;
            this.dgvMisReservas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMisReservas.Size = new System.Drawing.Size(930, 500);
            this.dgvMisReservas.TabIndex = 0;
            this.dgvMisReservas.SelectionChanged += new System.EventHandler(this.DgvMisReservas_SelectionChanged);
            // 
            // btnConfirmarReserva
            // 
            this.btnConfirmarReserva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirmarReserva.BackColor = System.Drawing.Color.Teal;
            this.btnConfirmarReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarReserva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfirmarReserva.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarReserva.Location = new System.Drawing.Point(180, 540);
            this.btnConfirmarReserva.Name = "btnConfirmarReserva";
            this.btnConfirmarReserva.Size = new System.Drawing.Size(150, 40);
            this.btnConfirmarReserva.TabIndex = 2;
            this.btnConfirmarReserva.Text = "Confirmar Reserva";
            this.btnConfirmarReserva.UseVisualStyleBackColor = false;
            this.btnConfirmarReserva.Click += new System.EventHandler(this.btnConfirmarReserva_Click);
            // 
            // btnCancelarReserva
            // 
            this.btnCancelarReserva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelarReserva.BackColor = System.Drawing.Color.Teal;
            this.btnCancelarReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarReserva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancelarReserva.ForeColor = System.Drawing.Color.White;
            this.btnCancelarReserva.Location = new System.Drawing.Point(20, 540);
            this.btnCancelarReserva.Name = "btnCancelarReserva";
            this.btnCancelarReserva.Size = new System.Drawing.Size(150, 40);
            this.btnCancelarReserva.TabIndex = 1;
            this.btnCancelarReserva.Text = "Cancelar Reserva";
            this.btnCancelarReserva.UseVisualStyleBackColor = false;
            this.btnCancelarReserva.Click += new System.EventHandler(this.btnCancelarReserva_Click);
            // 
            // tabServicios
            // 
            this.tabServicios.Controls.Add(this.dgvServiciosReserva);
            this.tabServicios.Controls.Add(this.lblReservaSeleccionada);
            this.tabServicios.Controls.Add(this.btnSolicitarServicio);
            this.tabServicios.Controls.Add(this.cmbServicios);
            this.tabServicios.Controls.Add(this.label5);
            this.tabServicios.Controls.Add(this.cmbReservaServicio);
            this.tabServicios.Controls.Add(this.label4);
            this.tabServicios.Location = new System.Drawing.Point(4, 29);
            this.tabServicios.Name = "tabServicios";
            this.tabServicios.Padding = new System.Windows.Forms.Padding(3);
            this.tabServicios.Size = new System.Drawing.Size(968, 593);
            this.tabServicios.TabIndex = 2;
            this.tabServicios.Text = "Servicios";
            this.tabServicios.UseVisualStyleBackColor = true;
            // 
            // dgvServiciosReserva
            // 
            this.dgvServiciosReserva.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServiciosReserva.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiciosReserva.Location = new System.Drawing.Point(20, 140);
            this.dgvServiciosReserva.Name = "dgvServiciosReserva";
            this.dgvServiciosReserva.ReadOnly = true;
            this.dgvServiciosReserva.RowHeadersWidth = 51;
            this.dgvServiciosReserva.RowTemplate.Height = 29;
            this.dgvServiciosReserva.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServiciosReserva.Size = new System.Drawing.Size(930, 440);
            this.dgvServiciosReserva.TabIndex = 0;
            // 
            // lblReservaSeleccionada
            // 
            this.lblReservaSeleccionada.AutoSize = true;
            this.lblReservaSeleccionada.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReservaSeleccionada.Location = new System.Drawing.Point(350, 20);
            this.lblReservaSeleccionada.Name = "lblReservaSeleccionada";
            this.lblReservaSeleccionada.Size = new System.Drawing.Size(119, 20);
            this.lblReservaSeleccionada.TabIndex = 6;
            this.lblReservaSeleccionada.Text = "Reserva #XXXX";
            // 
            // btnSolicitarServicio
            // 
            this.btnSolicitarServicio.BackColor = System.Drawing.Color.Teal;
            this.btnSolicitarServicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolicitarServicio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSolicitarServicio.ForeColor = System.Drawing.Color.White;
            this.btnSolicitarServicio.Location = new System.Drawing.Point(350, 80);
            this.btnSolicitarServicio.Name = "btnSolicitarServicio";
            this.btnSolicitarServicio.Size = new System.Drawing.Size(180, 40);
            this.btnSolicitarServicio.TabIndex = 5;
            this.btnSolicitarServicio.Text = "Solicitar Servicio";
            this.btnSolicitarServicio.UseVisualStyleBackColor = false;
            this.btnSolicitarServicio.Click += new System.EventHandler(this.btnSolicitarServicio_Click);
            // 
            // cmbServicios
            // 
            this.cmbServicios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServicios.FormattingEnabled = true;
            this.cmbServicios.Location = new System.Drawing.Point(20, 80);
            this.cmbServicios.Name = "cmbServicios";
            this.cmbServicios.Size = new System.Drawing.Size(300, 28);
            this.cmbServicios.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Servicio:";
            // 
            // cmbReservaServicio
            // 
            this.cmbReservaServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReservaServicio.FormattingEnabled = true;
            this.cmbReservaServicio.Location = new System.Drawing.Point(20, 20);
            this.cmbReservaServicio.Name = "cmbReservaServicio";
            this.cmbReservaServicio.Size = new System.Drawing.Size(300, 28);
            this.cmbReservaServicio.TabIndex = 2;
            this.cmbReservaServicio.SelectedIndexChanged += new System.EventHandler(this.CmbReservaServicio_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Reserva:";
            // 
            // tabFacturas
            // 
            this.tabFacturas.Controls.Add(this.dgvFacturas);
            this.tabFacturas.Controls.Add(this.btnVerDetalleFactura);
            this.tabFacturas.Controls.Add(this.btnPagarFactura);
            this.tabFacturas.Location = new System.Drawing.Point(4, 29);
            this.tabFacturas.Name = "tabFacturas";
            this.tabFacturas.Padding = new System.Windows.Forms.Padding(3);
            this.tabFacturas.Size = new System.Drawing.Size(968, 593);
            this.tabFacturas.TabIndex = 3;
            this.tabFacturas.Text = "Facturas";
            this.tabFacturas.UseVisualStyleBackColor = true;
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Location = new System.Drawing.Point(20, 20);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersWidth = 51;
            this.dgvFacturas.RowTemplate.Height = 29;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(930, 500);
            this.dgvFacturas.TabIndex = 0;
            this.dgvFacturas.SelectionChanged += new System.EventHandler(this.DgvFacturas_SelectionChanged);
            // 
            // btnVerDetalleFactura
            // 
            this.btnVerDetalleFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerDetalleFactura.BackColor = System.Drawing.Color.Teal;
            this.btnVerDetalleFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalleFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnVerDetalleFactura.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalleFactura.Location = new System.Drawing.Point(180, 540);
            this.btnVerDetalleFactura.Name = "btnVerDetalleFactura";
            this.btnVerDetalleFactura.Size = new System.Drawing.Size(150, 40);
            this.btnVerDetalleFactura.TabIndex = 2;
            this.btnVerDetalleFactura.Text = "Ver Detalle";
            this.btnVerDetalleFactura.UseVisualStyleBackColor = false;
            this.btnVerDetalleFactura.Click += new System.EventHandler(this.btnVerDetalleFactura_Click);
            // 
            // btnPagarFactura
            // 
            this.btnPagarFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagarFactura.BackColor = System.Drawing.Color.Teal;
            this.btnPagarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagarFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPagarFactura.ForeColor = System.Drawing.Color.White;
            this.btnPagarFactura.Location = new System.Drawing.Point(20, 540);
            this.btnPagarFactura.Name = "btnPagarFactura";
            this.btnPagarFactura.Size = new System.Drawing.Size(150, 40);
            this.btnPagarFactura.TabIndex = 1;
            this.btnPagarFactura.Text = "Pagar Factura";
            this.btnPagarFactura.UseVisualStyleBackColor = false;

            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBienvenida.Location = new System.Drawing.Point(20, 20);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(168, 28);
            this.lblBienvenida.TabIndex = 1;
            this.lblBienvenida.Text = "Bienvenido, User";
            // 
            // FormCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zona Cliente";
            this.tabControl1.ResumeLayout(false);
            this.tabDisponibilidad.ResumeLayout(false);
            this.tabDisponibilidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).EndInit();
            this.tabMisReservas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisReservas)).EndInit();
            this.tabServicios.ResumeLayout(false);
            this.tabServicios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosReserva)).EndInit();
            this.tabFacturas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDisponibilidad;
        private System.Windows.Forms.DataGridView dgvDisponibilidad;
        private System.Windows.Forms.ComboBox cmbCalendarios;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbAlojamientos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTipoReserva;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReservar;
        private System.Windows.Forms.TextBox txtPrecioSeleccionado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHabitacionSeleccionada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarDispon;
        private System.Windows.Forms.DateTimePicker dtpDisponSalida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDisponEntrada;
        private System.Windows.Forms.Label lblFechaEntrada;
        private System.Windows.Forms.TabPage tabMisReservas;
        private System.Windows.Forms.DataGridView dgvMisReservas;
        private System.Windows.Forms.Button btnConfirmarReserva;
        private System.Windows.Forms.Button btnCancelarReserva;
        private System.Windows.Forms.TabPage tabServicios;
        private System.Windows.Forms.DataGridView dgvServiciosReserva;
        private System.Windows.Forms.Label lblReservaSeleccionada;
        private System.Windows.Forms.Button btnSolicitarServicio;
        private System.Windows.Forms.ComboBox cmbServicios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbReservaServicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabFacturas;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Button btnVerDetalleFactura;
        private System.Windows.Forms.Button btnPagarFactura;
        private System.Windows.Forms.Label lblBienvenida;
    }
}
