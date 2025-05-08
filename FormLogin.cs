using System;
using System.Windows.Forms;
using HotelSOL.Repositorio;
using HotelSOL.Modelo;

namespace HotelSOL.Client
{
    public partial class FormLogin : Form
    {
        private readonly ClienteRepository _clienteRepo;
        private readonly RecepcionistaRepository _recepRepo;
        private readonly AdministradorRepository _adminRepo;

        public FormLogin()
        {
            InitializeComponent();

            _clienteRepo = new ClienteRepository(Program.Context);
            _recepRepo = new RecepcionistaRepository(Program.Context);
            _adminRepo = new AdministradorRepository(Program.Context);

            cmbTipoUsuario.SelectedIndex = 0; // Por defecto "Cliente"
        }

        private async void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim();
            var tipoUsuario = cmbTipoUsuario.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(tipoUsuario))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loginExitoso = false;

            if (tipoUsuario == "Cliente")
            {
                var cliente = await _clienteRepo.AutenticarClienteAsync(email, password);
                if (cliente != null)
                {
                    loginExitoso = true;
                    // Ahora pasamos el objeto Cliente, no su Id
                    var formCliente = new FormCliente(cliente);
                    formCliente.Show();
                }
            }
            else if (tipoUsuario == "Recepcionista")
            {
                var recepcionista = await _recepRepo.AutenticarRecepcionistaAsync(email, password);
                if (recepcionista != null)
                {
                    loginExitoso = true;
                    var formRecepcionista = new FormRecepcionista();
                    formRecepcionista.Show();
                }
            }
            else if (tipoUsuario == "Administrador")
            {
                var admin = await _adminRepo.AutenticarAdministradorAsync(email, password);
                if (admin != null)
                {
                    loginExitoso = true;
                    var formAdmin = new FormAdministradores();
                    formAdmin.Show();
                }
            }

            if (loginExitoso)
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales inválidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
