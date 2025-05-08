using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSOL.Client
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblTipoUsuario;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private ComboBox cmbTipoUsuario;
        private Button btnIniciarSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblEmail = new Label();
            this.lblPassword = new Label();
            this.lblTipoUsuario = new Label();
            this.txtEmail = new TextBox();
            this.txtPassword = new TextBox();
            this.cmbTipoUsuario = new ComboBox();
            this.btnIniciarSesion = new Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(50, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(300, 50);
            this.lblTitulo.Text = "Hotel SOL - Login";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new Font("Segoe UI", 10F);
            this.lblEmail.Location = new Point(50, 90);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(100, 23);
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(160, 90);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(180, 23);
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.Location = new Point(50, 130);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(100, 23);
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(160, 130);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(180, 23);
            // 
            // lblTipoUsuario
            // 
            this.lblTipoUsuario.Font = new Font("Segoe UI", 10F);
            this.lblTipoUsuario.Location = new Point(50, 170);
            this.lblTipoUsuario.Name = "lblTipoUsuario";
            this.lblTipoUsuario.Size = new Size(100, 23);
            this.lblTipoUsuario.Text = "Tipo Usuario:";
            // 
            // cmbTipoUsuario
            // 
            this.cmbTipoUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoUsuario.Items.AddRange(new object[] { "Cliente", "Recepcionista", "Administrador" });
            this.cmbTipoUsuario.Location = new Point(160, 170);
            this.cmbTipoUsuario.Name = "cmbTipoUsuario";
            this.cmbTipoUsuario.Size = new Size(180, 23);
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.BackColor = Color.SteelBlue;
            this.btnIniciarSesion.ForeColor = Color.White;
            this.btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            this.btnIniciarSesion.Location = new Point(125, 220);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new Size(150, 40);
            this.btnIniciarSesion.Text = "Iniciar Sesión";
            this.btnIniciarSesion.Click += new EventHandler(this.btnIniciarSesion_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(400, 300);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblTipoUsuario);
            this.Controls.Add(this.cmbTipoUsuario);
            this.Controls.Add(this.btnIniciarSesion);
            this.Name = "FormLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login - Hotel SOL";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
