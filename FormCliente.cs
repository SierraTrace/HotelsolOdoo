using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using HotelSOL.Modelo;
using HotelSOL.Repositorio;
using HotelSOL.Repositorio.Interfaces;

namespace HotelSOL.Client
{
    public partial class FormCliente : Form
    {
        private readonly IClienteRepository _repo;
        private readonly Cliente _currentCliente;
        private Reserva? _reservaSeleccionada;
        private Habitacion? _habitacionSeleccionada;
        private Factura? _facturaSeleccionada;

        // Lista original de reservas (dominio), para mapear al hacer clic
        private List<Reserva> _misReservasOriginales = new();

        // Lista de facturas (entidad) para recuperar la factura seleccionada
        private List<Factura> _listFacturas = new();

        // Listas de datos para el binding (vistas y datos)
        private readonly BindingList<TipoHabitacion> _listTiposHabitacion = new();
        private readonly BindingList<Habitacion> _listDisponibilidad = new();
        private readonly BindingList<Servicio> _listServicios = new();
        private readonly BindingList<Reserva> _listReservasParaServ = new();
        private readonly BindingList<ReservaServicio> _listServiciosReserva = new();
        private readonly BindingList<TipoReserva> _listTiposReserva = new();
        private readonly BindingList<AlojamientoComboDto> _listAlojamientos = new();
        private readonly BindingList<Calendario> _listCalendarios = new();
        private readonly BindingList<FacturaView> _listFacturasVM = new();
        private readonly BindingList<ReservaView> _listMisReservasVM = new();

        public FormCliente(Cliente cliente)
        {
            InitializeComponent();
            _currentCliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _repo = new ClienteRepository(Program.Context);

            AplicarEstiloMaterial();
            ConfigurarColumnasDisponibilidad();
            ConfigurarColumnasMisReservas();
            ConfigurarColumnasServiciosReserva();
            ConfigurarColumnasFacturas();

            dtpDisponEntrada.Value = DateTime.Today;
            dtpDisponSalida.Value = DateTime.Today.AddDays(1);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            lblBienvenida.Text = $"Bienvenido, {_currentCliente.Nombre} {_currentCliente.Apellido}";

            await CargarTiposReservaAsync();
            await CargarTiposHabitacionAsync();
            await CargarCombosAlojCalendarioAsync();
            await CargarDisponibilidadAsync();
            await CargarMisReservasAsync();
            await CargarServiciosAsync();
            await CargarReservasParaServicioAsync();
            await CargarFacturasAsync();
        }

        private void AplicarEstiloMaterial()
        {
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10);

            void Rec(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    switch (c)
                    {
                        case Button btn:
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.BackColor = Color.Teal;
                            btn.ForeColor = Color.White;
                            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            btn.Height = 40;
                            btn.AutoSize = false;
                            btn.Cursor = Cursors.Hand;
                            break;
                        case ComboBox cb:
                            cb.DropDownStyle = ComboBoxStyle.DropDownList;
                            break;
                        case DataGridView dgv:
                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                            dgv.EnableHeadersVisualStyles = false;
                            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
                            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                            break;
                    }
                    if (c.HasChildren) Rec(c);
                }
            }
            Rec(this);
        }

        private async Task CargarTiposReservaAsync()
        {
            var tipos = await _repo.ObtenerTiposReservaAsync();
            _listTiposReserva.Clear();
            tipos.ForEach(t => _listTiposReserva.Add(t));
            cmbTipoReserva.DataSource = _listTiposReserva;
            cmbTipoReserva.DisplayMember = "Nombre";
            cmbTipoReserva.ValueMember = "Id";
        }

        private async Task CargarTiposHabitacionAsync()
        {
            var tipos = await Program.Context.TipoHabitaciones.ToListAsync();
            _listTiposHabitacion.Clear();
            tipos.ForEach(t => _listTiposHabitacion.Add(t));
        }

        private async Task CargarCombosAlojCalendarioAsync()
        {
            try
            {
                var alojs = await Program.Context.Alojamientos
                    .Include(a => a.Tipo)
                    .Select(a => new AlojamientoComboDto
                    {
                        Id = a.Id,
                        Nombre = a.Nombre ?? $"{a.Id} - {a.Tipo.Nombre}"
                    })
                    .ToListAsync();

                _listAlojamientos.Clear();
                foreach (var a in alojs)
                {
                    _listAlojamientos.Add(a);
                }

                cmbAlojamientos.DataSource = _listAlojamientos;
                cmbAlojamientos.DisplayMember = "Nombre";
                cmbAlojamientos.ValueMember = "Id";

                if (cmbAlojamientos.Items.Count > 0)
                {
                    cmbAlojamientos.SelectedIndex = 0;
                }

                var cals = await Program.Context.Calendarios.ToListAsync();
                _listCalendarios.Clear();
                cals.ForEach(c => _listCalendarios.Add(c));
                cmbCalendarios.DataSource = _listCalendarios;
                cmbCalendarios.DisplayMember = "Id";
                cmbCalendarios.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alojamientos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarDisponibilidadAsync()
        {
            var fe = dtpDisponEntrada.Value.Date;
            var fs = dtpDisponSalida.Value.Date;
            if (fs <= fe)
            {
                MessageBox.Show(
                    "La fecha de salida debe ser posterior a la de entrada",
                    "Fechas inválidas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var libres = await _repo.ConsultarDisponibilidadAsync(fe, fs);
            _listDisponibilidad.Clear();
            libres.ForEach(h => _listDisponibilidad.Add(h));
            dgvDisponibilidad.DataSource = _listDisponibilidad;

            txtHabitacionSeleccionada.Clear();
            txtPrecioSeleccionado.Clear();
            _habitacionSeleccionada = null;
        }

        private async Task CargarMisReservasAsync()
        {
            var reservas = await Program.Context.Reservas
                .Where(r => r.ClienteId == _currentCliente.Id)
                .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .Include(r => r.Alojamiento).ThenInclude(a => a.Tipo)
                .Include(r => r.Estado)
                .Include(r => r.Servicios).ThenInclude(rs => rs.Servicio)
                .ToListAsync();

            _misReservasOriginales = reservas;
            _listMisReservasVM.Clear();

            foreach (var r in reservas)
            {
                var rh = r.ReservaHabitaciones.FirstOrDefault();
                decimal precioNoche = Convert.ToDecimal(rh?.PrecioPorNoche ?? 0.0);
                int noches = rh?.Noches ?? 0;
                var servicios = r.Servicios
                    .Select(rs => $"{rs.Servicio.Nombre} (x{rs.Cantidad})")
                    .ToList();

                _listMisReservasVM.Add(new ReservaView
                {
                    Id = r.Id,
                    HabitacionNumero = rh?.Habitacion.Numero ?? "",
                    TipoAlojamiento = r.Alojamiento?.Tipo?.Nombre ?? "",
                    PrecioBase = precioNoche,
                    Noches = noches,
                    PrecioTotal = precioNoche * noches,
                    FechaEntrada = r.FechaEntrada,
                    FechaSalida = r.FechaSalida,
                    Estado = r.Estado?.Nombre ?? "",
                    Servicios = string.Join(", ", servicios)
                });
            }

            dgvMisReservas.DataSource = _listMisReservasVM;
            _reservaSeleccionada = null;
            btnCancelarReserva.Enabled = btnConfirmarReserva.Enabled = false;
        }

        private async Task CargarServiciosAsync()
        {
            var servs = await _repo.ObtenerServiciosAsync();
            _listServicios.Clear();
            servs.ForEach(s => _listServicios.Add(s));
            cmbServicios.DataSource = _listServicios;
            cmbServicios.DisplayMember = "Nombre";
            cmbServicios.ValueMember = "Id";
        }

        private async Task CargarReservasParaServicioAsync()
        {
            var lista = await _repo.ConsultarReservasAsync(_currentCliente.Id);
            var activas = lista.Where(r => r.Estado?.Nombre != "Cancelada").ToList();
            _listReservasParaServ.Clear();
            activas.ForEach(r => _listReservasParaServ.Add(r));
            cmbReservaServicio.DataSource = _listReservasParaServ;
            cmbReservaServicio.DisplayMember = "Id";
            cmbReservaServicio.ValueMember = "Id";
        }

        private async Task CargarServiciosReservaAsync(int reservaId)
        {
            var reserva = await Program.Context.Reservas
                .Include(r => r.Servicios).ThenInclude(rs => rs.Servicio)
                .FirstOrDefaultAsync(r => r.Id == reservaId);

            _listServiciosReserva.Clear();
            reserva?.Servicios.ToList().ForEach(rs => _listServiciosReserva.Add(rs));
            dgvServiciosReserva.DataSource = _listServiciosReserva;
        }

        private async Task CargarFacturasAsync()
        {
            var facturas = await Program.Context.Facturas
                .Include(f => f.Reserva).ThenInclude(r => r.Cliente)
                .Include(f => f.Reserva).ThenInclude(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .Include(f => f.Reserva).ThenInclude(r => r.Alojamiento).ThenInclude(a => a.Tipo)
                .Include(f => f.Detalles).ThenInclude(d => d.Servicio)
                .Where(f => f.Reserva.ClienteId == _currentCliente.Id)
                .ToListAsync();

            _listFacturas = facturas;
            _listFacturasVM.Clear();

            foreach (var factura in facturas)
            {
                var rh = factura.Reserva.ReservaHabitaciones.FirstOrDefault();
                decimal precioNoche = Convert.ToDecimal(rh?.PrecioPorNoche ?? 0.0);
                int noches = rh?.Noches ?? 0;
                decimal subAloj = precioNoche * noches;
                decimal totServ = Convert.ToDecimal(factura.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario));
                decimal descVIP = _currentCliente.EsVIP ? (subAloj + totServ) * 0.10m : 0m;

                _listFacturasVM.Add(new FacturaView
                {
                    Id = factura.Id,
                    ReservaId = factura.ReservaId.ToString(),
                    Cliente = $"{factura.Reserva.Cliente.Nombre} {factura.Reserva.Cliente.Apellido}",
                    Habitacion = rh?.Habitacion.Numero ?? "",
                    TipoAlojamiento = factura.Reserva.Alojamiento?.Tipo?.Nombre ?? "",
                    FechaEmision = factura.FechaEmision,
                    Noches = noches,
                    PrecioNoche = precioNoche,
                    SubtotalAlojamiento = subAloj,
                    TotalServicios = totServ,
                    DescuentoVIP = descVIP,
                    Total = subAloj + totServ - descVIP,
                    Estado = factura.Pagada ? "Pagada" : "Pendiente",
                    MetodoPago = factura.MetodoPago
                });
            }

            dgvFacturas.DataSource = _listFacturasVM;
            btnPagarFactura.Enabled = _listFacturasVM.Any(f => f.Estado == "Pendiente");
        }

        private void DgvDisponibilidad_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDisponibilidad.SelectedRows.Count > 0 &&
                dgvDisponibilidad.SelectedRows[0].DataBoundItem is Habitacion h)
            {
                _habitacionSeleccionada = h;
                txtHabitacionSeleccionada.Text = h.Numero;
                txtPrecioSeleccionado.Text = h.PrecioBase.ToString("C2");
            }
        }

        private void DgvMisReservas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMisReservas.SelectedRows.Count > 0 &&
                dgvMisReservas.SelectedRows[0].DataBoundItem is ReservaView vm)
            {
                _reservaSeleccionada = _misReservasOriginales.FirstOrDefault(r => r.Id == vm.Id);
                btnCancelarReserva.Enabled = _reservaSeleccionada?.Estado?.Nombre != "Cancelada";
                btnConfirmarReserva.Enabled = _reservaSeleccionada?.Estado?.Nombre == "Pendiente";
            }
            else
            {
                btnCancelarReserva.Enabled = btnConfirmarReserva.Enabled = false;
            }
        }

        private void DgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count > 0 &&
                dgvFacturas.SelectedRows[0].DataBoundItem is FacturaView fv)
            {
                _facturaSeleccionada = _listFacturas.FirstOrDefault(x => x.Id == fv.Id);
            }
        }

        private async void CmbReservaServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReservaServicio.SelectedItem is Reserva res)
            {
                await CargarServiciosReservaAsync(res.Id);
                lblReservaSeleccionada.Text = $"Reserva #{res.Id} – {res.FechaEntrada:d} a {res.FechaSalida:d}";
            }
        }

        private async void btnBuscarDispon_Click(object sender, EventArgs e) =>
            await CargarDisponibilidadAsync();

        private async void btnReservar_Click(object sender, EventArgs e)
        {
            if (_habitacionSeleccionada == null)
            {
                MessageBox.Show("Seleccione primero una habitación", "Aviso",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (cmbAlojamientos.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un tipo de alojamiento", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var estadoPend = await Program.Context.Estados
                    .FirstOrDefaultAsync(st => st.Nombre == "Pendiente")
                    ?? throw new InvalidOperationException("No existe el estado 'Pendiente'.");

                var reserva = new Reserva
                {
                    ClienteId = _currentCliente.Id,
                    TipoReservaId = (int)cmbTipoReserva.SelectedValue,
                    EstadoId = estadoPend.Id,
                    FechaEntrada = dtpDisponEntrada.Value.Date,
                    FechaSalida = dtpDisponSalida.Value.Date,
                    AlojamientoId = cmbAlojamientos.SelectedValue.ToString(),
                    CalendarioId = (int)cmbCalendarios.SelectedValue
                };

                if (reserva.FechaSalida <= reserva.FechaEntrada)
                {
                    MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (await _repo.RealizarReservaConHabitacionAsync(reserva, _habitacionSeleccionada.Id))
                {
                    MessageBox.Show("¡Reserva creada con éxito!", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDisponibilidadAsync();
                    await CargarMisReservasAsync();
                    await CargarReservasParaServicioAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear reserva: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (_reservaSeleccionada == null) return;
            if (MessageBox.Show($"¿Cancelar reserva #{_reservaSeleccionada.Id}?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (await _repo.AnularReservaAsync(_reservaSeleccionada.Id))
            {
                MessageBox.Show("Reserva cancelada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDisponibilidadAsync();
                await CargarMisReservasAsync();
                await CargarReservasParaServicioAsync();
            }
        }

        private async void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            if (_reservaSeleccionada == null) return;

            var estadoConf = await Program.Context.Estados.FirstOrDefaultAsync(st => st.Nombre == "Confirmada")
                            ?? throw new InvalidOperationException("No existe el estado 'Confirmada'.");

            _reservaSeleccionada.EstadoId = estadoConf.Id;
            if (await _repo.ModificarReservaAsync(_reservaSeleccionada))
            {
                MessageBox.Show("Reserva confirmada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMisReservasAsync();
                await CargarReservasParaServicioAsync();
            }
        }

        private async void btnSolicitarServicio_Click(object sender, EventArgs e)
        {
            if (cmbReservaServicio.SelectedItem is not Reserva res ||
                cmbServicios.SelectedItem is not Servicio serv)
                return;

            if (await _repo.SolicitarServicioParaReservaAsync(res.Id, serv.Id))
            {
                MessageBox.Show("Servicio agregado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarServiciosReservaAsync(res.Id);
                await CargarFacturasAsync();
                await CargarMisReservasAsync();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnVerDetalleFactura_Click(object sender, EventArgs e)
        {
            if (_facturaSeleccionada == null) return;

            var fv = _listFacturasVM.First(f => f.Id == _facturaSeleccionada.Id);
            var detalle =
                $"FACTURA N°: {fv.Id}\n" +
                $"FECHA: {fv.FechaEmision:d}\n" +
                $"CLIENTE: {fv.Cliente}\n" +
                $"HABITACIÓN: {fv.Habitacion}\n" +
                $"ALOJAMIENTO: {fv.TipoAlojamiento}\n" +
                $"NOCHES: {fv.Noches}\n\n" +
                $"Alojamiento: {fv.SubtotalAlojamiento:C2}\n" +
                $"Servicios: {fv.TotalServicios:C2}\n";

            if (fv.DescuentoVIP > 0)
                detalle += $"Descuento VIP: -{fv.DescuentoVIP:C2}\n";

            detalle += $"\nTOTAL: {fv.Total:C2}\n" +
                       $"ESTADO: {fv.Estado}\n" +
                       $"MÉTODO: {fv.MetodoPago}";

            MessageBox.Show(detalle, "Detalle Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConfigurarColumnasDisponibilidad()
        {
            dgvDisponibilidad.AutoGenerateColumns = false;
            dgvDisponibilidad.Columns.Clear();
            dgvDisponibilidad.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                ReadOnly = true,
                Width = 50
            });
            dgvDisponibilidad.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Numero",
                HeaderText = "Nº Habitación",
                ReadOnly = true,
                Width = 80
            });
            dgvDisponibilidad.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Capacidad",
                HeaderText = "Capacidad",
                ReadOnly = true,
                Width = 70
            });
            dgvDisponibilidad.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioBase",
                HeaderText = "Precio/Noche",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 90
            });
            dgvDisponibilidad.Columns.Add(new DataGridViewComboBoxColumn
            {
                DataPropertyName = "TipoHabitacionId",
                HeaderText = "Tipo Habitación",
                DataSource = _listTiposHabitacion,
                DisplayMember = "Nombre",
                ValueMember = "Id",
                ReadOnly = true,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                Width = 120
            });
        }

        private void ConfigurarColumnasMisReservas()
        {
            dgvMisReservas.AutoGenerateColumns = false;
            dgvMisReservas.Columns.Clear();
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID Reserva",
                Width = 80
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HabitacionNumero",
                HeaderText = "Habitación",
                Width = 80
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TipoAlojamiento",
                HeaderText = "Tipo",
                Width = 100
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioBase",
                HeaderText = "Precio/Noche",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Noches",
                HeaderText = "Noches",
                Width = 60
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioTotal",
                HeaderText = "Total Alojamiento",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 120
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaEntrada",
                HeaderText = "Entrada",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" },
                Width = 90
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaSalida",
                HeaderText = "Salida",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" },
                Width = 90
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 90
            });
            dgvMisReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Servicios",
                HeaderText = "Servicios",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 200,
                MinimumWidth = 200
            });
        }

        private void ConfigurarColumnasServiciosReserva()
        {
            dgvServiciosReserva.AutoGenerateColumns = false;
            dgvServiciosReserva.Columns.Clear();
            dgvServiciosReserva.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Servicio.Nombre",
                HeaderText = "Servicio",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue
                }
            });
            dgvServiciosReserva.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvServiciosReserva.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Unitario",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", ForeColor = Color.Green }
            });
            dgvServiciosReserva.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalServicio",
                HeaderText = "Total",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", ForeColor = Color.Blue }
            });
        }

        private void ConfigurarColumnasFacturas()
        {
            dgvFacturas.AutoGenerateColumns = false;
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
                HeaderText = "Fecha",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" },
                Width = 90
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cliente",
                HeaderText = "Cliente",
                Width = 150
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Habitacion",
                HeaderText = "Habitación",
                Width = 80
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TipoAlojamiento",
                HeaderText = "Alojamiento",
                Width = 100
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Noches",
                HeaderText = "Noches",
                Width = 60
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioNoche",
                HeaderText = "Precio/Noche",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 90
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SubtotalAlojamiento",
                HeaderText = "Subtotal Aloj.",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalServicios",
                HeaderText = "Total Servicios",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DescuentoVIP",
                HeaderText = "Descuento VIP",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 90
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total Factura",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 90
            });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MetodoPago",
                HeaderText = "Método Pago",
                Width = 100
            });
        }

        private class ReservaView
        {
            public int Id { get; set; }
            public string HabitacionNumero { get; set; } = "";
            public string TipoAlojamiento { get; set; } = "";
            public decimal PrecioBase { get; set; }
            public int Noches { get; set; }
            public decimal PrecioTotal { get; set; }
            public DateTime FechaEntrada { get; set; }
            public DateTime FechaSalida { get; set; }
            public string Estado { get; set; } = "";
            public string Servicios { get; set; } = "";
        }

        private class FacturaView
        {
            public string Id { get; set; } = "";
            public string ReservaId { get; set; } = "";
            public string Cliente { get; set; } = "";
            public string Habitacion { get; set; } = "";
            public string TipoAlojamiento { get; set; } = "";
            public DateTime FechaEmision { get; set; }
            public int Noches { get; set; }
            public decimal PrecioNoche { get; set; }
            public decimal SubtotalAlojamiento { get; set; }
            public decimal TotalServicios { get; set; }
            public decimal DescuentoVIP { get; set; }
            public decimal Total { get; set; }
            public string Estado { get; set; } = "";
            public string MetodoPago { get; set; } = "";
        }
    }

    public class FormMetodoPago : Form
    {
        public string MetodoPagoSeleccionado { get; private set; } = "";
        private ComboBox cmbMetodosPago;
        private Button btnAceptar;
        private Button btnCancelar;

        public FormMetodoPago()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
        }

        private void InitializeComponent()
        {
            Text = "Seleccionar Método de Pago";
            ClientSize = new Size(300, 150);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblMetodo = new Label
            {
                Text = "Método de Pago:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            cmbMetodosPago = new ComboBox
            {
                Location = new Point(20, 50),
                Size = new Size(260, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbMetodosPago.Items.AddRange(new[] {
                "Tarjeta Crédito",
                "Tarjeta Débito",
                "Efectivo",
                "Transferencia"
            });
            cmbMetodosPago.SelectedIndex = 0;

            btnAceptar = new Button
            {
                Text = "Aceptar",
                Location = new Point(120, 90),
                Size = new Size(80, 40),
                DialogResult = DialogResult.OK,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAceptar.Click += (_, __) =>
            {
                MetodoPagoSeleccionado = cmbMetodosPago.SelectedItem?.ToString() ?? "";
                Close();
            };

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(210, 90),
                Size = new Size(80, 40),
                DialogResult = DialogResult.Cancel,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnCancelar.Click += (_, __) => Close();

            Controls.Add(lblMetodo);
            Controls.Add(cmbMetodosPago);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
        }
    }
}