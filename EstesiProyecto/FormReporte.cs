using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EstesiProyecto.FormCrearFactura;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EstesiProyecto
{

    public partial class FormReporte : Form
    {

        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public AgregarReporte Reporte { get; private set; }
        public FormReporte()
        {
            InitializeComponent();
            CargarSucursal();
            CargarNuevoId();
            txtRucProveedor.Leave += new EventHandler(txtRucCi_Leave);
            dgvFactura.CellValueChanged += dgvFactura_CellValueChanged;
            txtRucProveedor.Validating += Control_Validating;
            cmbSucursal.Validating += Control_Validating;
            txtRucProveedor.Validating += Control_Validating;
            DeshabilitarControles();
           
        }

        private void DeshabilitarControles()
        {
            // Deshabilitar todos los botones
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnQuitar.Enabled = false;
            btnImprimir.Enabled = false;
            // Agrega otros controles que necesites deshabilitar
        }
        private void HabilitarControles()
        {
            // Habilitar todos los botones
            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnQuitar.Enabled = true;
            btnImprimir.Enabled=true;
            
        }
        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Control control = sender as Control;

            if (control is TextBox textBox)
            {
                // Verifica si el TextBox está vacío
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Muestra un mensaje de advertencia con el nombre del TextBox
                    MessageBox.Show($"Por favor, ingresa un dato", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Cancela el evento de validación
                    e.Cancel = true;
                    textBox.Focus(); // Regresar el foco al TextBox
                }
            }
            else if (control is ComboBox comboBox)
            {
                // Verifica si no se ha seleccionado un elemento
                if (comboBox.SelectedIndex == -1)
                {
                    // Muestra un mensaje de advertencia con el nombre del ComboBox
                    MessageBox.Show($"Por favor, selecciona un valor ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Cancela el evento de validación
                    e.Cancel = true;
                    comboBox.Focus(); // Regresar el foco al ComboBox
                }
            }
        }



        private void ActualizarTotales()
        {
            decimal sumaValorFacturado = 0;
            decimal sumaValorNC = 0;

            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow row in dgvFactura.Rows)
            {
                // Validar y acumular el valor de "Valor Facturado"
                if (row.Cells["Valor Facturado"].Value != null &&
                    decimal.TryParse(row.Cells["Valor Facturado"].Value.ToString(), out decimal valorFacturado))
                {
                    sumaValorFacturado += valorFacturado;
                }

                // Validar y acumular el valor de "Valor NC"
                if (row.Cells["Valor NC"].Value != null &&
                    decimal.TryParse(row.Cells["Valor NC"].Value.ToString(), out decimal valorNC))
                {
                    sumaValorNC += valorNC;
                }
            }

            // Calcular el total neto a cancelar
            decimal totalNetoACancelar = sumaValorFacturado - sumaValorNC;

            // Actualizar los TextBox con los valores calculados
            txtFactura.Text = sumaValorFacturado.ToString("N2");
            txtTotalNC.Text = sumaValorNC.ToString("N2");
            txtNetoCancelar.Text = totalNetoACancelar.ToString("N2");
        }
        public void AgregarNC_Factura(string numFactura, string numNC, decimal valorNC, string Motivo)
        {
            // Buscar la fila de la factura seleccionada
            foreach (DataGridViewRow row in dgvFactura.Rows)
            {
                if (row.Cells["Nº Factura"].Value != null && row.Cells["Nº Factura"].Value.ToString() == numFactura)
                {
                    // Insertar una nueva fila para la NC debajo de la factura encontrada
                    int rowIndex = row.Index + 1; // Posición para insertar la nueva fila
                    dgvFactura.Rows.Insert(rowIndex);

                    // Rellenar los valores de la NC en las columnas correspondientes
                    dgvFactura.Rows[rowIndex].Cells["Nº Factura"].Value = ""; // Vacío para no repetir el número de factura
                    dgvFactura.Rows[rowIndex].Cells["Nota Credito"].Value = numNC; // Número de NC
                    dgvFactura.Rows[rowIndex].Cells["Valor NC"].Value = valorNC; // Valor de la NC
                    dgvFactura.Rows[rowIndex].Cells["Motivo"].Value = Motivo; // Motivo de la NC (puedes cambiar esto según lo que necesites)

                    // Puedes sumar los valores de NC y restarlos del valor total de la factura
                    decimal totalNeto = Convert.ToDecimal(row.Cells["Valor Facturado"].Value) - valorNC;
                    row.Cells["Neto a Pagar"].Value = totalNeto; // Actualizar el total a pagar
                    break;
                }
                ActualizarTotales();
            }
        }
        private void txtRucCi_Leave(object sender, EventArgs e)
        {
            // Obtener el valor del campo Identificación (RUC/CI)
            string identificacion = txtRucProveedor.Text.Trim();

            if (!string.IsNullOrEmpty(identificacion))
            {
                // Llamar a un método para buscar el proveedor en la base de datos
                CargarProveedor(identificacion);
            }
        }
        private void FormReporte_Load(object sender, EventArgs e)
        {

            // Crear el menú contextual
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem agregarNCItem = new ToolStripMenuItem("Agregar NC");

            // Asociar el evento click del ítem "Agregar NC"
            agregarNCItem.Click += new EventHandler(agregarNCItem_Click);  // Asegúrate de tener este método

            contextMenu.Items.Add(agregarNCItem);

            // Asociar el menú contextual al DataGridView
            dgvFactura.ContextMenuStrip = contextMenu;

            CargarNuevoId();

            dgvFactura.Columns.Add("Sucursal", "Sucursal");
            dgvFactura.Columns.Add("Proveedor", "Proveedor");
            dgvFactura.Columns.Add("Nº Factura", "Nº Factura");
            dgvFactura.Columns.Add("Nº Retencion", "Nº Retencion");
            dgvFactura.Columns.Add("Fecha Facturacion", "Fecha Facturacion");
            dgvFactura.Columns.Add("Fecha Vencimiento", "Fecha Vencimiento");
            dgvFactura.Columns.Add("Fecha Recibido", "Fecha Recibido");
            dgvFactura.Columns.Add("Fecha Sugerida", "Fecha Sugerida");
            dgvFactura.Columns.Add("Nota Credito", "Nota Credito");
            dgvFactura.Columns.Add("Motivo", "Motivo");
            dgvFactura.Columns.Add("Valor Facturado", "Valor Facturado");
            dgvFactura.Columns.Add("Valor NC", "Valor NC");
            dgvFactura.Columns.Add("Neto a Pagar", "Neto a Pagar");

        }
        public void agregarNCItem_Click(object sender, EventArgs e)
        {
            if (dgvFactura.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvFactura.SelectedRows[0];

                // Verificar si la fila está vacía
                bool filaVacia = true;
                foreach (DataGridViewCell cell in filaSeleccionada.Cells)
                {
                    // Verifica si la celda no está vacía
                    if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        filaVacia = false;
                        break;
                    }
                }

                // Si la fila está vacía, mostrar un mensaje
                if (filaVacia)
                {
                    MessageBox.Show("La fila seleccionada está vacía. Por favor, seleccione una factura válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Salir del método para evitar abrir el formulario
                }

                // Si la fila no está vacía, continuar con la lógica normal
                string facturaSeleccionada = filaSeleccionada.Cells["Nº Factura"].Value.ToString();

                // Pasar la referencia de 'this' y la factura seleccionada a FormCrearNC
                FormCrearNC formAgregarNC = new FormCrearNC(this,"", facturaSeleccionada);

                // Mostrar el formulario y esperar el resultado
                if (formAgregarNC.ShowDialog() == DialogResult.OK)
                {
                    // Leer los valores de NC de FormCrearNC
                    string numNC = formAgregarNC.NumeroNC;
                    decimal valorNC = formAgregarNC.ValorNC;
                    string motivo = formAgregarNC.MotivoNC;

                    // Agregar una nueva fila de NC en el DataGridView debajo de la factura seleccionada
                    int rowIndex = dgvFactura.SelectedRows[0].Index + 1;
                    dgvFactura.Rows.Insert(rowIndex, "", "", facturaSeleccionada, "", "", "", "", "", numNC, motivo, "", valorNC, "");  // Ajusta las columnas según el orden y los datos necesarios
                }
                ActualizarTotales();
            }

        }
        private void CargarSucursal()
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                string query = "select Id_Sucursal, NombreSucursal from Sucursales";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["Id_Sucursal"] = 0; // Un Id que no existe en la tabla, como 0
                        row["NombreSucursal"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbSucursal.DisplayMember = "NombreSucursal";
                        cmbSucursal.ValueMember = "Id_Sucursal";
                        cmbSucursal.DataSource = dt;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Sucursal: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
   
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormCrearFactura formFactura = new FormCrearFactura();

            Reporte = new AgregarReporte
            {

                Sucursal = cmbSucursal.Text,
                Proveedor = txtResultProveedor.Text

            };


            if (formFactura.ShowDialog() == DialogResult.OK)
            {
                Factura nuevaFactura = formFactura.NuevaFactura;

                // Agregar la nueva factura al DataGridView
                dgvFactura.Rows.Add(

                    Reporte.Sucursal,
                    Reporte.Proveedor,
                   nuevaFactura.NFactura,
                    nuevaFactura.NRetencion,
                    nuevaFactura.FechaFacturacion.ToShortDateString(),
                    nuevaFactura.FechaVencimiento.ToShortDateString(),
                    nuevaFactura.FechaRecibido.ToShortDateString(),
                    nuevaFactura.FechaSugerida.ToShortDateString(),
                    nuevaFactura.NotaCredito,
                    nuevaFactura.Motivo,
                     nuevaFactura.ValorFacturado,
                    nuevaFactura.ValorNC,
                   
                    nuevaFactura.NetoPagar

                );
                ActualizarTotales();
            }
        }
        private void CargarProveedor(string identificacion)
        {
            int bloqueTamano = 100; // Tamaño del bloque
            int offset = 0; // Empezar desde la primera fila
            bool encontrado = false;

            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                while (!encontrado)
                {
                    using (SqlCommand cmd = new SqlCommand("BuscarProveedorPorBloques", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@BlockSize", bloqueTamano);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Mostrar el nombre del proveedor en el TextBox correspondiente
                                txtResultProveedor.Text = reader["Razon_Social"].ToString();
                                encontrado = true; // Salir del bucle al encontrar el proveedor
                                HabilitarControles();
                            }
                            else if (!reader.HasRows)
                            {
                                // Si no se encontraron más resultados en el bloque, terminar la búsqueda
                                MessageBox.Show("Proveedor no encontrado.");
                                break;
                            }
                        }
                    }

                    // Si no se encontró, mover al siguiente bloque
                    offset += bloqueTamano;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        private void CargarNuevoId()
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                // Consulta para obtener el último ID registrado
                string query = "SELECT ISNULL(MAX(id_factura), 0) + 1 FROM reportePago";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nuevoId = (int)cmd.ExecuteScalar(); // Obtener el ID incrementado
                                                            // txtIdReporte.Text = nuevoId.ToString("D10"); // Mostrarlo en el TextBox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el ID: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public class AgregarReporte
        {
            public String Sucursal { get; set; }
            public String Proveedor { get; set; }
        }

        private void dgvFactura_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarTotales();  // Recalcular los totales cuando cambia una celda
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvFactura.SelectedRows.Count > 0)
            {
                var selectedRow = dgvFactura.SelectedRows[0];
                string tipoFila = selectedRow.Cells["Nota Credito"].Value?.ToString();

                // Si la fila es una factura (sin valor en "Nota Credito")
                if (string.IsNullOrEmpty(tipoFila))
                {
                    // Eliminar todas las filas relacionadas con la factura seleccionada
                    string facturaNumero = selectedRow.Cells["Nº Factura"].Value.ToString();
                    for (int i = dgvFactura.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dgvFactura.Rows[i].Cells["Nº Factura"].Value?.ToString() == facturaNumero)
                        {
                            dgvFactura.Rows.RemoveAt(i);
                            ActualizarTotales();
                  
                        }
                        
                    }
                }
                else
                {
                    // Si es una nota de crédito, solo eliminar esa fila
                    dgvFactura.Rows.Remove(selectedRow);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.");
            }
        }
        private Factura ObtenerFacturaDeFilaSeleccionada(DataGridViewRow fila)
        {
            return new Factura
            {
                NFactura = fila.Cells["Nº Factura"].Value?.ToString(),
                NRetencion = fila.Cells["Nº Retencion"].Value?.ToString(),
                FechaFacturacion = Convert.ToDateTime(fila.Cells["Fecha Facturacion"].Value),
                FechaVencimiento = Convert.ToDateTime(fila.Cells["Fecha Vencimiento"].Value),
                ValorFacturado = Convert.ToDecimal(fila.Cells["Valor Facturado"].Value),
                FechaRecibido = Convert.ToDateTime(fila.Cells["Fecha Recibido"].Value),
                // Puedes agregar más campos si los tienes en la clase Factura
            };
        }

        // Método para actualizar una fila en el DataGridView con los datos de una factura modificada
        private void ActualizarFilaConFactura(DataGridViewRow fila, Factura factura)
        {
            fila.Cells["Nº Factura"].Value = factura.NFactura;
            fila.Cells["Nº Retencion"].Value = factura.NRetencion;
            fila.Cells["Fecha Facturacion"].Value = factura.FechaFacturacion.ToShortDateString();
            fila.Cells["Fecha Vencimiento"].Value = factura.FechaVencimiento.ToShortDateString();
            fila.Cells["Fecha Recibido"].Value = factura.FechaRecibido.ToShortDateString();
            fila.Cells["Fecha Sugerida"].Value = factura.FechaRecibido.AddDays(30).ToShortDateString();
            fila.Cells["Valor Facturado"].Value = factura.ValorFacturado;
           
            // Actualiza más campos si es necesario
        }

        // Método para abrir el formulario en modo edición
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvFactura.SelectedRows.Count > 0)
            {
                var selectedRow = dgvFactura.SelectedRows[0];

                // Comprobar si es una NC o una factura
                if (selectedRow.Cells["Nota Credito"].Value != null && !string.IsNullOrEmpty(selectedRow.Cells["Nota Credito"].Value.ToString()))
                {
                    // Es una NC
                    string numFactura = selectedRow.Cells["Nº Factura"].Value.ToString();
                    string numNC = selectedRow.Cells["Nota Credito"].Value.ToString();
                    decimal valorNC = Convert.ToDecimal(selectedRow.Cells["Valor NC"].Value);
                    string motivonc = selectedRow.Cells["Motivo"].Value.ToString();

                    // Crear y mostrar el formulario para editar NC
                    FormCrearNC formEditarNC = new FormCrearNC(this, motivonc, numFactura, numNC, valorNC);
                    formEditarNC.TituloFormulario = "Editar NC"; // Texto para el botón Crear NC
                  

                    if (formEditarNC.ShowDialog() == DialogResult.OK)
                    {
                        // Actualiza la fila de la NC con los nuevos valores ingresados
                        selectedRow.Cells["Nota Credito"].Value = formEditarNC.NumeroNC;
                        selectedRow.Cells["Valor NC"].Value = formEditarNC.ValorNC;
                    }
                }
                else
                {
                    // Es una factura
                    Factura facturaSeleccionada = ObtenerFacturaDeFilaSeleccionada(selectedRow);

                    // Crear y mostrar el formulario para editar factura
                    FormCrearFactura formEditarFactura = new FormCrearFactura(facturaSeleccionada);
                    formEditarFactura.TituloFormulario = "Editar Factura"; // Texto para el botón Crear NC
                 
                    if (formEditarFactura.ShowDialog() == DialogResult.OK)
                    {
                        // Actualiza la fila de la factura con los datos editados
                        ActualizarFilaConFactura(selectedRow, formEditarFactura.NuevaFactura);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para editar.");
            }
        }

        private void dgvFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}


