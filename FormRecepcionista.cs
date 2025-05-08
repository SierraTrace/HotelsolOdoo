#nullable enable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using HotelSOL.Modelo;
using HotelSOL.Data;
using HotelSOL.Repositorio;

namespace HotelSOL.Client
{
    public partial class FormRecepcionista : Form
    {
        private readonly RecepcionistaRepository _repo;
        private bool _isInitializing = true;
        private readonly SemaphoreSlim _ctxLock = new SemaphoreSlim(1, 1);

        private Cliente? _clienteSeleccionado;
        private Reserva? _reservaSeleccionada;
        private Factura? _facturaSeleccionada;

        private readonly BindingList<Cliente> _listaClientes = new();
        private readonly BindingList<ReservaGrid> _listaReservas = new();
        private readonly BindingList<Habitacion> _listaDisponibilidad = new();
        private readonly BindingList<Factura> _listaFacturas = new();
        private readonly BindingList<Servicio> _listaServicios = new();
        private readonly BindingList<Reserva> _listaReservasParaServicio = new();

        private class ReservaGrid
        {
            public int Id { get; set; }
            public string Cliente { get; set; } = string.Empty;
            public string TipoReserva { get; set; } = string.Empty;
            public string Habitacion { get; set; } = string.Empty;
            public string TipoAlojamiento { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public DateTime FechaEntrada { get; set; }
            public DateTime FechaSalida { get; set; }
        }

        public FormRecepcionista()
        {
            InitializeComponent();
            AplicarEstiloMaterial();

            // Para poder usar .Context
            _repo = new RecepcionistaRepository(Program.Context);

            // Clientes
            dgvClientes.DataSource = _listaClientes;
            dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;
            btnCrearCliente.Click += async (_, __) => { await CrearClienteAsync(); await CargarClientesAsync(); };
            btnActualizarCliente.Click += async (_, __) => { await ActualizarClienteAsync(); await CargarClientesAsync(); };
            btnRefrescarClientes.Click += async (_, __) => await CargarClientesAsync();

            // Reservas
            dgvReservas.DataSource = _listaReservas;
            ConfigurarColumnasReservas();
            dgvReservas.SelectionChanged += DgvReservas_SelectionChanged;
            btnCrearReserva.Click += async (_, __) => { await CrearReservaAsync(); await CargarReservasAsync(); await CargarDisponibilidadAsync(); };
            btnModificarReserva.Click += async (_, __) => { await ModificarReservaAsync(); await CargarReservasAsync(); await CargarDisponibilidadAsync(); };
            btnAnularReserva.Click += async (_, __) => { await AnularReservaAsync(); await CargarReservasAsync(); await CargarDisponibilidadAsync(); };

            // Disponibilidad
            dgvDisponibilidad.DataSource = _listaDisponibilidad;
            btnConsultarDispon.Click += async (_, __) => await CargarDisponibilidadAsync();

            // Facturación
            dgvClientesFacturas.DataSource = _listaClientes;
            dgvClientesFacturas.SelectionChanged += DgvClientesFacturas_SelectionChanged;
            dgvFacturas.DataSource = _listaFacturas;
            ConfigurarColumnasFacturas();
            dgvFacturas.SelectionChanged += DgvFacturas_SelectionChanged;
            btnCrearFactura.Click += async (_, __) => { await CrearFacturaAsync(); await CargarFacturasAsync(); };
            btnImprimirFactura.Click += (_, __) => ImprimirFactura();

            // Servicios
            cmbReservaServicio.DataSource = _listaReservasParaServicio;
            cmbReservaServicio.DisplayMember = "Id";
            cmbReservaServicio.ValueMember = "Id";
            cmbServicios.DataSource = _listaServicios;
            cmbServicios.DisplayMember = "Nombre";
            cmbServicios.ValueMember = "Id";
            btnSolicitarServicio.Click += async (_, __) => await AgregarServicioAsync();

            // Inyectar FormCargaOdoo en tabOdoo
            var formOdoo = new FormCargaOdoo();
            formOdoo.TopLevel = false;
            formOdoo.FormBorderStyle = FormBorderStyle.None;
            formOdoo.Dock = DockStyle.Fill;

            pnlOdooContainer.Controls.Add(formOdoo);
            formOdoo.Show();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            try
            {
                await CargarClientesAsync();
                await CargarCombosReservaAsync();
                await CargarReservasAsync();
                await CargarDisponibilidadAsync();
                await CargarServiciosAsync();
                await CargarFacturasAsync();
                await CargarReservasParaServicioAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inicial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        private void AplicarEstiloMaterial()
        {
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10);
            void Recurse(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    switch (c)
                    {
                        case Button btn:
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.BackColor = Color.DeepSkyBlue;
                            btn.ForeColor = Color.White;
                            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            btn.FlatAppearance.BorderSize = 0;
                            btn.Height = 40;
                            btn.Width += 20;
                            btn.Cursor = Cursors.Hand;
                            break;
                        case ComboBox cmb:
                            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                            break;
                        case TextBox txt:
                            txt.BorderStyle = BorderStyle.FixedSingle;
                            break;
                        case DataGridView dgv:
                            dgv.BackgroundColor = Color.White;
                            dgv.BorderStyle = BorderStyle.FixedSingle;
                            dgv.EnableHeadersVisualStyles = false;
                            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
                            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            dgv.RowHeadersVisible = false;
                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                            break;
                    }
                    if (c.HasChildren) Recurse(c);
                }
            }
            Recurse(this);
        }


        #region Métodos de configuración de columnas
        private void ConfigurarColumnasReservas()
        {
            dgvReservas.Columns.Clear();
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 50 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cliente", HeaderText = "Cliente", Width = 150 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoReserva", HeaderText = "Tipo Reserva", Width = 120 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Habitacion", HeaderText = "Habitación", Width = 100 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoAlojamiento", HeaderText = "Tipo Alojamiento", Width = 120 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado", Width = 100 });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaEntrada", HeaderText = "Entrada", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } });
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaSalida", HeaderText = "Salida", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } });
        }

        private void ConfigurarColumnasFacturas()
        {
            dgvFacturas.Columns.Clear();
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "N° Factura",
                Width = 80
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaEmision",
                HeaderText = "Fecha emisión",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 80
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MetodoPago",
                HeaderText = "Método Pago",
                Width = 120
            });
        }
        #endregion

        #region Carga de datos
        private async Task CargarClientesAsync()
        {
            await _ctxLock.WaitAsync();
            try
            {
                var lista = await _repo.ObtenerClientesAsync();
                _listaClientes.Clear();
                lista.ForEach(cli => _listaClientes.Add(cli));
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarCombosReservaAsync()
        {
            await _ctxLock.WaitAsync();
            try
            {
                cmbClienteId.DataSource = _listaClientes;
                cmbClienteId.DisplayMember = "NombreCompleto";
                cmbClienteId.ValueMember = "Id";

                var tipos = await _repo.ObtenerTiposReservaAsync();
                cmbTipoReserva.DataSource = tipos;
                cmbTipoReserva.DisplayMember = "Nombre";
                cmbTipoReserva.ValueMember = "Id";

                cmbHabitacion.DataSource = await _repo.Context.ReservaHabitaciones
                                                    .Include(rh => rh.Habitacion)
                                                    .Select(rh => rh.Habitacion)
                                                    .Distinct()
                                                    .ToListAsync();
                cmbHabitacion.DisplayMember = "Numero";
                cmbHabitacion.ValueMember = "Id";

                var estados = await _repo.ObtenerEstadosReservaAsync();
                cmbEstado.DataSource = estados;
                cmbEstado.DisplayMember = "Nombre";
                cmbEstado.ValueMember = "Id";
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarReservasAsync()
        {
            await _ctxLock.WaitAsync();
            try
            {
                var datos = await _repo.Context.Reservas
                    .Include(r => r.Cliente)
                    .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                    .Include(r => r.Alojamiento).ThenInclude(a => a.Tipo)
                    .Include(r => r.Estado)
                    .ToListAsync();

                _listaReservas.Clear();
                foreach (var r in datos)
                {
                    _listaReservas.Add(new ReservaGrid
                    {
                        Id = r.Id,
                        Cliente = $"{r.Cliente.Nombre} {r.Cliente.Apellido}",
                        TipoReserva = r.TipoReserva.Nombre,
                        Habitacion = r.ReservaHabitaciones.FirstOrDefault()?.Habitacion.Numero ?? "",
                        TipoAlojamiento = r.Alojamiento.Tipo.Nombre,
                        Estado = r.Estado.Nombre,
                        FechaEntrada = r.FechaEntrada,
                        FechaSalida = r.FechaSalida
                    });
                }
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarDisponibilidadAsync()
        {
            var fe = dtpDisponEntrada.Value.Date;
            var fs = dtpDisponSalida.Value.Date;
            if (fs <= fe)
            {
                MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada",
                                "Error en fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await _ctxLock.WaitAsync();
            try
            {
                var estadosNo = new[] { "Confirmada", "Pendiente" };
                var ocupadas = await _repo.Context.ReservaHabitaciones
                    .Where(rh => estadosNo.Contains(rh.Reserva.Estado.Nombre)
                                 && rh.Reserva.FechaEntrada < fs
                                 && rh.Reserva.FechaSalida > fe)
                    .Select(rh => rh.HabitacionId)
                    .Distinct()
                    .ToListAsync();

                var libres = await _repo.Context.Habitaciones
                    .Where(h => h.Disponibilidad && !ocupadas.Contains(h.Id))
                    .ToListAsync();

                _listaDisponibilidad.Clear();
                libres.ForEach(h => _listaDisponibilidad.Add(h));
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarServiciosAsync()
        {
            await _ctxLock.WaitAsync();
            try
            {
                var servs = await _repo.ObtenerServiciosAsync();
                _listaServicios.Clear();
                servs.ForEach(s => _listaServicios.Add(s));
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarFacturasAsync()
        {
            if (_clienteSeleccionado == null) return;
            await _ctxLock.WaitAsync();
            try
            {
                var facturas = await _repo.Context.Facturas
                    .Include(f => f.Reserva).ThenInclude(r => r.Cliente)
                    .Include(f => f.Detalles).ThenInclude(d => d.Servicio)
                    .Where(f => f.Reserva.ClienteId == _clienteSeleccionado.Id)
                    .OrderByDescending(f => f.FechaEmision)
                    .ToListAsync();

                _listaFacturas.Clear();
                facturas.ForEach(f => _listaFacturas.Add(f));
            }
            finally { _ctxLock.Release(); }
        }

        private async Task CargarReservasParaServicioAsync()
        {
            await _ctxLock.WaitAsync();
            try
            {
                // Solo las reservas activas o todas, según prefieras
                var list = await _repo.Context.Reservas
                    .Where(r => r.FechaSalida >= DateTime.Today)
                    .ToListAsync();
                _listaReservasParaServicio.Clear();
                list.ForEach(r => _listaReservasParaServicio.Add(r));
            }
            finally { _ctxLock.Release(); }
        }
        #endregion

        #region Manejo de eventos de selección
        private async void DgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (dgvClientes.CurrentRow?.DataBoundItem is not Cliente cli) return;
            _clienteSeleccionado = cli;
            txtNombreCliente.Text = cli.Nombre;
            txtApellidoCliente.Text = cli.Apellido;
            txtEmailCliente.Text = cli.Email;
            txtMovilCliente.Text = cli.Movil.ToString();
            txtPasswordCliente.Text = cli.Password;
            cmbEsVip.SelectedItem = cli.EsVIP ? "Sí" : "No";
            await CargarFacturasAsync();
        }

        private async void DgvReservas_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (dgvReservas.CurrentRow?.DataBoundItem is not ReservaGrid rg) return;

            await _ctxLock.WaitAsync();
            Reserva? sel;
            try
            {
                sel = await _repo.Context.Reservas.FindAsync(rg.Id);
            }
            finally { _ctxLock.Release(); }
            if (sel == null) return;

            _reservaSeleccionada = sel;
            cmbClienteId.SelectedValue = sel.ClienteId;
            cmbTipoReserva.SelectedValue = sel.TipoReservaId;
            cmbEstado.SelectedValue = sel.EstadoId;
            dtpEntrada.Value = sel.FechaEntrada;
            dtpSalida.Value = sel.FechaSalida;

            // actualizar combo servicios
            cmbReservaServicio.SelectedValue = sel.Id;
        }

        private async void DgvClientesFacturas_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (dgvClientesFacturas.CurrentRow?.DataBoundItem is not Cliente cli) return;
            _clienteSeleccionado = cli;
            await CargarFacturasAsync();
            if (_listaFacturas.Any())
                MostrarDetalleFactura(_listaFacturas.First());
        }

        private async void DgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (dgvFacturas.CurrentRow?.DataBoundItem is not Factura f) return;
            _facturaSeleccionada = f;
            MostrarDetalleFactura(f);
        }
        #endregion

        #region CRUD Clientes
        private async Task CrearClienteAsync()
        {
            var nuevo = new Cliente
            {
                Nombre = txtNombreCliente.Text,
                Apellido = txtApellidoCliente.Text,
                Email = txtEmailCliente.Text,
                Movil = int.TryParse(txtMovilCliente.Text, out var m) ? m : 0,
                Password = txtPasswordCliente.Text,
                EsVIP = cmbEsVip.SelectedItem?.ToString() == "Sí"
            };
            await _repo.RegistrarClienteAsync(nuevo);
            MessageBox.Show("Cliente creado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task ActualizarClienteAsync()
        {
            if (_clienteSeleccionado == null) return;
            _clienteSeleccionado.Nombre = txtNombreCliente.Text;
            _clienteSeleccionado.Apellido = txtApellidoCliente.Text;
            _clienteSeleccionado.Email = txtEmailCliente.Text;
            _clienteSeleccionado.Movil = int.TryParse(txtMovilCliente.Text, out var m) ? m : 0;
            _clienteSeleccionado.Password = txtPasswordCliente.Text;
            _clienteSeleccionado.EsVIP = cmbEsVip.SelectedItem?.ToString() == "Sí";
            await _repo.ActualizarClienteAsync(_clienteSeleccionado);
            MessageBox.Show("Cliente actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region CRUD Reservas
        private async Task CrearReservaAsync()
        {
            var fe = dtpEntrada.Value.Date;
            var fs = dtpSalida.Value.Date;
            if (fs <= fe)
            {
                MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int hid = (int)cmbHabitacion.SelectedValue!;
            bool ocup = await _repo.Context.ReservaHabitaciones
                         .AnyAsync(rh => rh.HabitacionId == hid
                                     && rh.Reserva.FechaEntrada < fs
                                     && rh.Reserva.FechaSalida > fe
                                     && rh.Reserva.Estado.Nombre == "Confirmada");
            if (ocup)
            {
                MessageBox.Show("La habitación ya está ocupada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = new Reserva
            {
                ClienteId = (int)cmbClienteId.SelectedValue!,
                TipoReservaId = (int)cmbTipoReserva.SelectedValue!,
                EstadoId = (int)cmbEstado.SelectedValue!,
                FechaEntrada = fe,
                FechaSalida = fs,
                CalendarioId = 1
            };
            await _repo.RealizarReservaAsync(r);

            _repo.Context.ReservaHabitaciones.Add(new ReservaHabitacion
            {
                ReservaId = r.Id,
                HabitacionId = hid
            });
            await _repo.Context.SaveChangesAsync();

            MessageBox.Show("Reserva creada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task ModificarReservaAsync()
        {
            if (_reservaSeleccionada == null) return;
            var fe = dtpEntrada.Value.Date;
            var fs = dtpSalida.Value.Date;
            if (fs <= fe)
            {
                MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _reservaSeleccionada.FechaEntrada = fe;
            _reservaSeleccionada.FechaSalida = fs;
            _reservaSeleccionada.ClienteId = (int)cmbClienteId.SelectedValue!;
            _reservaSeleccionada.TipoReservaId = (int)cmbTipoReserva.SelectedValue!;
            _reservaSeleccionada.EstadoId = (int)cmbEstado.SelectedValue!;

            await _repo.ModificarReservaAsync(_reservaSeleccionada);
            MessageBox.Show("Reserva modificada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task AnularReservaAsync()
        {
            if (_reservaSeleccionada == null) return;
            if (MessageBox.Show("¿Confirma anular?", "Anular", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            await _repo.AnularReservaAsync(_reservaSeleccionada.Id);
            MessageBox.Show("Reserva anulada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Facturación
        private async Task CrearFacturaAsync()
        {
            if (_reservaSeleccionada == null)
            {
                MessageBox.Show("Seleccione primero una reserva", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var rh = await _repo.Context.ReservaHabitaciones
                        .Include(rh => rh.Habitacion)
                        .FirstOrDefaultAsync(rh => rh.ReservaId == _reservaSeleccionada.Id);
            if (rh == null) { MessageBox.Show("No hay habitación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            int noches = (_reservaSeleccionada.FechaSalida - _reservaSeleccionada.FechaEntrada).Days;
            double total = noches * rh.Habitacion.PrecioBase;
            var fac = new Factura(_reservaSeleccionada.Id, total);
            _repo.Context.Facturas.Add(fac);
            await _repo.Context.SaveChangesAsync();
            MessageBox.Show("Factura creada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarDetalleFactura(Factura f)
        {
            // Encabezado
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"FACTURA #{f.Id}");
            sb.AppendLine($"Fecha: {f.FechaEmision:g}");
            sb.AppendLine($"Cliente: {f.Reserva.Cliente.Nombre} {f.Reserva.Cliente.Apellido}");
            sb.AppendLine();

            // Alojamiento y habitación
            var rh = f.Reserva.ReservaHabitaciones.FirstOrDefault();
            if (rh != null)
            {
                var hab = rh.Habitacion;
                sb.AppendLine($"Habitación: {hab.Numero}");
                sb.AppendLine($"Tipo Alojamiento: {f.Reserva.Alojamiento.Tipo.Nombre}");
                sb.AppendLine();
            }

            // Servicios
            if (f.Detalles != null && f.Detalles.Any())
            {
                sb.AppendLine("Servicios:");
                foreach (var d in f.Detalles)
                {
                    sb.AppendLine($" - {d.Servicio.Nombre} x{d.Cantidad}: {d.PrecioUnitario:C2}");
                }
                sb.AppendLine();
            }

            sb.AppendLine($"Total: {f.Total:C2}");
            sb.AppendLine($"Pagada: {(f.Pagada ? "Sí" : "No")}");
            txtDetalleFactura.Text = sb.ToString();
        }
        #endregion

        #region Servicios
        private async Task AgregarServicioAsync()
        {
            if (cmbReservaServicio.SelectedItem is not Reserva res) return;
            if (cmbServicios.SelectedItem is not Servicio serv) return;

            // Obtener o crear factura
            var fac = await _repo.Context.Facturas.FirstOrDefaultAsync(f => f.ReservaId == res.Id);
            if (fac == null)
            {
                fac = new Factura(res.Id, 0);
                _repo.Context.Facturas.Add(fac);
                await _repo.Context.SaveChangesAsync();
            }

            // Agregar detalle
            var detalle = new DetalleFacturaServicio
            {
                FacturaId = fac.Id,
                ServicioId = serv.Id,
                Cantidad = 1,
                PrecioUnitario = serv.Precio
            };
            _repo.Context.DetalleFacturaServicios.Add(detalle);

            // Actualizar total
            fac.Total += serv.Precio;
            await _repo.Context.SaveChangesAsync();

            MessageBox.Show("Servicio agregado a la factura", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (_facturaSeleccionada?.Id == fac.Id)
                MostrarDetalleFactura(fac);
        }
        #endregion

        private void ImprimirFactura()
        {
            if (_facturaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var pd = new PrintDialog();
            if (pd.ShowDialog() != DialogResult.OK) return;
            var doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += (s, e) =>
            {
                e.Graphics.DrawString(txtDetalleFactura.Text, new Font("Arial", 10), Brushes.Black, new PointF(50, 50));
            };
            doc.Print();
        }
    }
}
