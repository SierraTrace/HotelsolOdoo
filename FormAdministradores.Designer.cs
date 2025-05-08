using System;
using System.Windows.Forms;
using System.Drawing;

namespace HotelSOL.Client
{
    partial class FormAdministradores
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtMovil;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private ComboBox cbTipoUsuario;
        private Label lblEsVip;
        private ComboBox cmbEsVip;
        private Panel pnlOdooContainer;
        private TabControl tabControlAdmin;
        private TabPage tabUsuarios, tabCargaOdoo;

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
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtMovil = new TextBox();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            dgvUsuarios = new DataGridView();
            lblNombre = new Label();
            lblApellido = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblMovil = new Label();
            cbTipoUsuario = new ComboBox();
            lblTipo = new Label();
            lblEsVip = new Label();
            cmbEsVip = new ComboBox();
            pnlOdooContainer = new Panel();
            tabControlAdmin = new TabControl();
            tabUsuarios = new TabPage();
            tabCargaOdoo = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            tabControlAdmin.SuspendLayout();
            tabUsuarios.SuspendLayout();
            tabCargaOdoo.SuspendLayout();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(126, 20);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(169, 23);
            txtNombre.TabIndex = 5;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(126, 60);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(169, 23);
            txtApellido.TabIndex = 6;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(126, 100);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(169, 23);
            txtEmail.TabIndex = 7;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(126, 140);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(169, 23);
            txtPassword.TabIndex = 8;
            // 
            // txtMovil
            // 
            txtMovil.Location = new Point(126, 180);
            txtMovil.Name = "txtMovil";
            txtMovil.Size = new Size(169, 23);
            txtMovil.TabIndex = 9;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(45, 344);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(75, 23);
            btnCrear.TabIndex = 5;
            btnCrear.Text = "Crear";
            btnCrear.Click += btnCrear_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(151, 344);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(75, 23);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(262, 344);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.Location = new Point(350, 20);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(420, 300);
            dgvUsuarios.TabIndex = 8;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(20, 23);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblApellido
            // 
            lblApellido.Location = new Point(20, 63);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(100, 23);
            lblApellido.TabIndex = 1;
            lblApellido.Text = "Apellido:";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(20, 103);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 143);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // lblMovil
            // 
            lblMovil.Location = new Point(20, 183);
            lblMovil.Name = "lblMovil";
            lblMovil.Size = new Size(100, 23);
            lblMovil.TabIndex = 4;
            lblMovil.Text = "Móvil:";
            // 
            // cbTipoUsuario
            // 
            cbTipoUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoUsuario.Items.AddRange(new object[] { "Administrador", "Recepcionista", "Cliente" });
            cbTipoUsuario.Location = new Point(126, 235);
            cbTipoUsuario.Name = "cbTipoUsuario";
            cbTipoUsuario.Size = new Size(165, 23);
            cbTipoUsuario.TabIndex = 0;
            cbTipoUsuario.SelectedIndexChanged += cmbTipoUsuario_SelectedIndexChanged;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(20, 238);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(91, 15);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "Tipo de usuario:";
            // 
            // lblEsVip
            // 
            lblEsVip.AutoSize = true;
            lblEsVip.Location = new Point(20, 278);
            lblEsVip.Name = "lblEsVip";
            lblEsVip.Size = new Size(67, 15);
            lblEsVip.TabIndex = 0;
            lblEsVip.Text = "Cliente VIP:";
            lblEsVip.Visible = false;
            // 
            // cmbEsVip
            // 
            cmbEsVip.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEsVip.Items.AddRange(new object[] { "Sí", "No" });
            cmbEsVip.Location = new Point(126, 275);
            cmbEsVip.Name = "cmbEsVip";
            cmbEsVip.Size = new Size(100, 23);
            cmbEsVip.TabIndex = 1;
            cmbEsVip.Visible = false;
            cmbEsVip.SelectedIndexChanged += cmbEsVip_SelectedIndexChanged;
            // 
            // pnlOdooContainer
            // 
            pnlOdooContainer.BorderStyle = BorderStyle.FixedSingle;
            pnlOdooContainer.Dock = DockStyle.Fill;
            pnlOdooContainer.Location = new Point(0, 0);
            pnlOdooContainer.Name = "pnlOdooContainer";
            pnlOdooContainer.Size = new Size(792, 372);
            pnlOdooContainer.TabIndex = 0;
            // 
            // tabControlAdmin
            // 
            tabControlAdmin.Controls.Add(tabUsuarios);
            tabControlAdmin.Controls.Add(tabCargaOdoo);
            tabControlAdmin.Dock = DockStyle.Fill;
            tabControlAdmin.Location = new Point(0, 0);
            tabControlAdmin.Name = "tabControlAdmin";
            tabControlAdmin.SelectedIndex = 0;
            tabControlAdmin.Size = new Size(800, 400);
            tabControlAdmin.TabIndex = 0;
            // 
            // tabUsuarios
            // 
            tabUsuarios.Controls.Add(lblNombre);
            tabUsuarios.Controls.Add(lblApellido);
            tabUsuarios.Controls.Add(lblEmail);
            tabUsuarios.Controls.Add(lblPassword);
            tabUsuarios.Controls.Add(lblMovil);
            tabUsuarios.Controls.Add(lblTipo);
            tabUsuarios.Controls.Add(txtNombre);
            tabUsuarios.Controls.Add(txtApellido);
            tabUsuarios.Controls.Add(txtEmail);
            tabUsuarios.Controls.Add(txtPassword);
            tabUsuarios.Controls.Add(txtMovil);
            tabUsuarios.Controls.Add(cbTipoUsuario);
            tabUsuarios.Controls.Add(cmbEsVip);
            tabUsuarios.Controls.Add(lblEsVip);
            tabUsuarios.Controls.Add(btnCrear);
            tabUsuarios.Controls.Add(btnActualizar);
            tabUsuarios.Controls.Add(btnEliminar);
            tabUsuarios.Controls.Add(dgvUsuarios);
            tabUsuarios.Location = new Point(4, 24);
            tabUsuarios.Name = "tabUsuarios";
            tabUsuarios.Size = new Size(792, 372);
            tabUsuarios.TabIndex = 0;
            tabUsuarios.Text = "Usuarios";
            // 
            // tabCargaOdoo
            // 
            tabCargaOdoo.Controls.Add(pnlOdooContainer);
            tabCargaOdoo.Location = new Point(4, 24);
            tabCargaOdoo.Name = "tabCargaOdoo";
            tabCargaOdoo.Size = new Size(792, 372);
            tabCargaOdoo.TabIndex = 1;
            tabCargaOdoo.Text = "Carga Odoo";
            // 
            // FormAdministradores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 400);
            Controls.Add(tabControlAdmin);
            Name = "FormAdministradores";
            Text = "Gestión de Administradores";
            Load += FormAdministradores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            tabControlAdmin.ResumeLayout(false);
            tabUsuarios.ResumeLayout(false);
            tabUsuarios.PerformLayout();
            tabCargaOdoo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblNombre;
        private Label lblApellido;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblMovil;
        private Label lblTipo;
    }
}
