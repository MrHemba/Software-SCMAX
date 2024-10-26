using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
        }

        private void ActualizarTotales()
        {
            decimal sumaValorFacturado = 0;
            decimal sumaValorNC = 0;
            decimal totalNetoACancelar = 0;

            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow row in dgvFactura.Rows)
            {
                if (row.Cells["Valor Facturado"].Value != null)
                {
                    sumaValorFacturado += Convert.ToDecimal(row.Cells["Valor Facturado"].Value);
                }

                if (row.Cells["Valor NC"].Value != null)
                {
                    sumaValorNC += Convert.ToDecimal(row.Cells["Valor NC"].Value);
                }
            }

            // Calcular el total neto a cancelar (valor facturado - valor NC)
            totalNetoACancelar = sumaValorFacturado - sumaValorNC;

            // Actualizar los TextBox con los valores calculados
            txtFactura.Text = sumaValorFacturado.ToString("N2");
            txtTotalNC.Text = sumaValorNC.ToString("N2");
            txtNetoCancelar.Text = totalNetoACancelar.ToString("N2");
        }


        public void AgregarNC_Factura(string numFactura, string numNC, decimal valorNC)
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
                    dgvFactura.Rows[rowIndex].Cells["Motivo"].Value = "Devolución"; // Motivo de la NC (puedes cambiar esto según lo que necesites)

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
                string facturaSeleccionada = dgvFactura.SelectedRows[0].Cells["Nº Factura"].Value.ToString();

                // Pasar la referencia de 'this' al constructor de FormCrearNC
                FormCrearNC formAgregarNC = new FormCrearNC(this, facturaSeleccionada);
                formAgregarNC.ShowDialog();
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
                    nuevaFactura.NotaCredito,
                    nuevaFactura.Motivo,
                    nuevaFactura.ValorNC,
                    nuevaFactura.ValorFacturado,
                    nuevaFactura.NetoPagar

                );
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

    }
}