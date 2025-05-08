using HotelSOL.Modelo;
using HotelSOL.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSOL.Client
{
    public partial class FormAdministradores : Form
    {
        private AdministradorRepository? _repo;

        public FormAdministradores()
        {
            InitializeComponent();
        }

        private void Inicializar()
        {
            _repo = new AdministradorRepository(Program.Context);
        }

        private async void FormAdministradores_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Inicializar();
            await CargarUsuarios();
            CargarTabOdoo();
        }

        private async Task CargarUsuarios()
        {
            if (_repo == null) return;
            var usuarios = await _repo.ObtenerTodosLosUsuariosAsync();
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (_repo == null) return;

            Usuario usuario;

            var tipoSeleccionado = cbTipoUsuario.SelectedItem?.ToString();

            if (tipoSeleccionado == "Cliente")
            {
                usuario = new Cliente
                {
                    EsVIP = cmbEsVip.SelectedItem?.ToString() == "Sí"
                };
            }
            else if (tipoSeleccionado == "Recepcionista")
            {
                usuario = new Recepcionista();
            }
            else
            {
                usuario = new Administrador();
            }

            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.Email = txtEmail.Text;
            usuario.Password = txtPassword.Text;
            usuario.Movil = int.Parse(txtMovil.Text);

            await _repo.CrearUsuarioAsync(usuario);
            await CargarUsuarios();
        }


        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_repo == null || dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.Email = txtEmail.Text;
            usuario.Password = txtPassword.Text;
            usuario.Movil = int.Parse(txtMovil.Text);

            // Verifica si es Cliente para actualizar también EsVIP
            if (usuario is Cliente cliente)
            {
                cliente.EsVIP = cmbEsVip.SelectedItem?.ToString() == "Sí";
            }

            await _repo.ActualizarUsuarioAsync(usuario);
            await CargarUsuarios();
        }


        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_repo == null || dgvUsuarios.CurrentRow == null) return;
            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            await _repo.EliminarUsuarioAsync(usuario.Id);
            await CargarUsuarios();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtEmail.Text = usuario.Email;
            txtPassword.Text = usuario.Password;
            txtMovil.Text = usuario.Movil.ToString();

            // Seleccionar el tipo de usuario en el combo
            cbTipoUsuario.SelectedItem = usuario.GetType().Name;

            // Mostrar EsVIP solo si es cliente
            if (usuario is Cliente cliente)
            {
                lblEsVip.Visible = true;
                cmbEsVip.Visible = true;
                cmbEsVip.SelectedItem = cliente.EsVIP ? "Sí" : "No";
            }
            else
            {
                lblEsVip.Visible = false;
                cmbEsVip.Visible = false;
            }
        }
        private void CargarTabOdoo()
        {
            var formOdoo = new FormCargaOdoo();
            formOdoo.TopLevel = false;
            formOdoo.FormBorderStyle = FormBorderStyle.None;
            formOdoo.Dock = DockStyle.Fill;

            pnlOdooContainer.Controls.Clear();
            pnlOdooContainer.Controls.Add(formOdoo);
            formOdoo.Show();
        }



        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cmbTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = cbTipoUsuario.SelectedItem?.ToString();

            if (tipo == "Cliente")
            {
                lblEsVip.Visible = true;
                cmbEsVip.Visible = true;
            }
            else
            {
                lblEsVip.Visible = false;
                cmbEsVip.Visible = false;
            }
        }

        private void cmbEsVip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
