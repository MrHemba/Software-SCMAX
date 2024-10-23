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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EstesiProyecto
{

    public partial class FormReporte : Form
    {
        private string usuarioActual;
        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public FormReporte()
        {
            InitializeComponent();

            CargarSucursal();
            txtRucProveedor.Leave += new EventHandler(txtRucCi_Leave);
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

        private void FormReporte_Load(object sender, EventArgs e)
        {
            CargarNuevoId();

        }
        private void CargarNuevoId()
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                // Consulta para obtener el último ID registrado
                string query = "SELECT ISNULL(MAX(Id_Proveedor), 0) + 1 FROM reportePago";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nuevoId = (int)cmd.ExecuteScalar(); // Obtener el ID incrementado
                    txtIdReporte.Text = nuevoId.ToString("D10"); // Mostrarlo en el TextBox
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

                // Insertar el reporte de pago
                using (SqlCommand cmd = new SqlCommand("sp_InsertarReportePago", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asignar parámetros
                    cmd.Parameters.AddWithValue("@Id_Reporte", txtIdReporte.Text);
                    cmd.Parameters.AddWithValue("@Id_Sucursal", Convert.ToInt32(cmbSucursal.SelectedValue));
                    cmd.Parameters.AddWithValue("@Id_Proveedor", idProveedor); // Usar el Id_Proveedor obtenido
                    cmd.Parameters.AddWithValue("@Num_Factura", txtNFactura.Text);
                    cmd.Parameters.AddWithValue("@Num_Retencion", txtNRetencion.Text);
                    cmd.Parameters.AddWithValue("@Fecha_Facturacion", dtpFacturacion.Value);
                    cmd.Parameters.AddWithValue("@Fecha_Vencimiento", dtpVencimiento.Value);
                    cmd.Parameters.AddWithValue("@Fecha_Sugerida", dtpSugerido.Value);
                    cmd.Parameters.AddWithValue("@Num_NC", txtNNotaCredito.Text);
                    cmd.Parameters.AddWithValue("@Id_Motivo", Convert.ToInt32(cmbMotivo.SelectedValue));
                    cmd.Parameters.AddWithValue("@Neto_Pagar", Convert.ToDecimal(txtNetoCancelar.Text));
                    cmd.Parameters.AddWithValue("@Valor_Factura", Convert.ToDecimal(txtFacturado.Text));

                    // Ejecutar procedimiento
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reporte guardado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el reporte: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }


        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}