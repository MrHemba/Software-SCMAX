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

namespace EstesiProyecto
{

    public partial class FormCrearFactura : Form
    {
        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public Factura NuevaFactura { get; private set; }

        public FormCrearFactura()
        {
            InitializeComponent();
            CargarSucursal();
            txtRucProveedor.Leave += new EventHandler(txtRucCi_Leave);
            CargarNuevoId();
            txtFacturado.Leave += new EventHandler(txtFacturado_Leave);
            txtValorNC.Leave += new EventHandler(txtFacturado_Leave);
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                // Buscar el Id_Proveedor según el RUC/Cédula ingresado
                int idProveedor = 0;
                using (SqlCommand cmdBuscar = new SqlCommand("SELECT Id_Proveedor FROM Proveedores WHERE Identificacion = @Ruc_Ci", conn))
                {
                    cmdBuscar.Parameters.AddWithValue("@Ruc_Ci", txtRucProveedor.Text);
                    object result = cmdBuscar.ExecuteScalar();

                    if (result != null)
                    {
                        idProveedor = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Proveedor no encontrado.");
                        return; // Salir si no se encuentra el proveedor
                    }
                }

                //// Insertar el reporte de pago
                //using (SqlCommand cmd = new SqlCommand("sp_InsertarReportePago", conn))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;

                //    // Asignar parámetros
                //    cmd.Parameters.AddWithValue("@Id_Factura", txtIdReporte.Text);
                //    cmd.Parameters.AddWithValue("@Id_Sucursal", Convert.ToInt32(cmbSucursal.SelectedValue));
                //    cmd.Parameters.AddWithValue("@Id_Proveedor", idProveedor); // Usar el Id_Proveedor obtenido
                //    cmd.Parameters.AddWithValue("@Num_Factura", txtNFactura.Text);
                //    cmd.Parameters.AddWithValue("@Num_Retencion", txtNRetencion.Text);
                //    cmd.Parameters.AddWithValue("@Fecha_Facturacion", dtpFacturacion.Value);
                //    cmd.Parameters.AddWithValue("@Fecha_Vencimiento", dtpVencimiento.Value);
                //    cmd.Parameters.AddWithValue("@Fecha_Sugerida", dtpSugerido.Value);
                //    cmd.Parameters.AddWithValue("@Num_NC", txtNNotaCredito.Text);
                //    cmd.Parameters.AddWithValue("@Id_Motivo", Convert.ToInt32(cmbMotivo.SelectedValue));
                //    cmd.Parameters.AddWithValue("@Neto_Pagar", Convert.ToDecimal(txtNetoCancelar.Text));
                //    cmd.Parameters.AddWithValue("@Valor_Factura", Convert.ToDecimal(txtFacturado.Text));
                //    cmd.Parameters.AddWithValue("@Valor_NC", Convert.ToDecimal(txtValorNC.Text));

                //}
            
                    NuevaFactura = new Factura
                    {
                        Sucursal = cmbSucursal.Text,
                        Proveedor = Convert.ToString(idProveedor),
                        NRetencion = txtNRetencion.Text,
                        FechaFacturacion = dtpFacturacion.Value,
                        FechaVencimiento = dtpVencimiento.Value,
                        FechaSugerida = dtpSugerido.Value,
                        NotaCredito = txtNNotaCredito.Text,
                        Motivo = cmbMotivo.Text,
                        ValorNC = Convert.ToDecimal(txtValorNC.Text),
                        ValorFacturado = Convert.ToDecimal(txtFacturado.Text),
                        NetoPagar = Convert.ToDecimal(txtNetoCancelar.Text)
                    };
                this.DialogResult = DialogResult.OK;
                this.Close(); // Cerrar el formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la factura: " + ex.Message);
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

        private void FormCrearFactura_Load(object sender, EventArgs e)
        {
            CargarNuevoId();
        }
        public class Factura
        {
            public string Sucursal { get; set; }
            public string Proveedor { get; set; }
            public string NRetencion { get; set; }
            public DateTime FechaFacturacion { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public DateTime FechaSugerida { get; set; }
            public string NotaCredito { get; set; }
            public string Motivo { get; set; }
            public decimal ValorNC { get; set; }
            public decimal ValorFacturado { get; set; }
            public decimal NetoPagar { get; set; }
        }

        private void txtFacturado_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtFacturado_Leave(object sender, EventArgs e)
        {
            try
            {
                // Verificar que los campos tengan valores válidos antes de hacer el cálculo
                decimal valorFacturado, valorNC;

                // Intentar convertir el valor del campo Total Facturado a decimal
                if (decimal.TryParse(txtFacturado.Text, out valorFacturado))
                {
                    // Si el campo Valor NC está vacío, lo tratamos como 0
                    if (!decimal.TryParse(txtValorNC.Text, out valorNC))
                    {
                        valorNC = 0;
                    }

                    // Calcular la diferencia entre Total Facturado y Valor NC
                    decimal netoCancelar = valorFacturado - valorNC;

                    // Asignar el valor calculado al campo Neto a Cancelar
                    txtNetoCancelar.Text = netoCancelar.ToString("F2"); // Formato con dos decimales
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa un valor válido para el Total Facturado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular el Neto a Cancelar: " + ex.Message);
            }
        }

    }
}
