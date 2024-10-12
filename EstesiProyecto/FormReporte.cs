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
        
    public partial class FormReporte : Form
    {
        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public FormReporte()
        {
            InitializeComponent();
            CargarProveedor();
            CargarSucursal();
        }

        private void FormReporte_Load(object sender, EventArgs e)
        {
           
        }
        private void CargarProveedor()
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                string query = "select Id_Proveedor, Nombre_Comercial from Proveedores";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["Id_Proveedor"] = 0; // Un Id que no existe en la tabla, como 0
                        row["Nombre_Comercial"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbProveedor.DisplayMember = "Nombre_Comercial";
                        cmbProveedor.ValueMember = "Id_Proveedor";
                        cmbProveedor.DataSource = dt;

                        // Seleccionar el valor vacío por defecto
                        cmbProveedor.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Proveedores: " + ex.Message);
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

                        // Seleccionar el valor vacío por defecto
                        cmbProveedor.SelectedIndex = 0;
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
    }
}
