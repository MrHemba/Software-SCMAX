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
    public partial class FormProveedor : Form
    {
        SqlConnection conexion = new SqlConnection("server=DESKTOP-9DGCSEO\\SQLEXPRESS01; database=SYSProvedores; integrated security=true");
        public FormProveedor()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void FormProveedor_Load(object sender, EventArgs e)
        {

        }
        private void CargarIdentificacion()
        {
            try
            {
                conexion.Open();
               
                string query = "SELECT Id_Identificacion FROM Tipoid"; 

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                           
                            cmbIdentificacion.Items.Add(reader["Id_Identificacion"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los Identificacion: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        private void BuscarProveedorPorRUC(string ruc)
        {
            conexion.Open();

            using (SqlCommand cmd = new SqlCommand("BuscarProveedor", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Identificacion", ruc);

                try
                {
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Si hay coincidencia, rellena los campos
                        while (reader.Read())
                        {
                            cmbIdentificacion.Text = reader["Tipo_Identificacion"].ToString();
                            txtRazonSocial.Text = reader["Razon_Social"].ToString();
                            txtComercial.Text = reader["Nombre_Comercial"].ToString();
                            txtApellidosRep.Text = reader["Apellidos_Representante"].ToString();
                            txtNombreRep.Text = reader["Nombres_Representante"].ToString();
                            txtCedula.Text = reader["Cedula"].ToString();
                            cmbContribuyente.Text = reader["Contribuyente"].ToString();
                            cmbPais.Text = reader["Pais"].ToString();
                            cmbProvincia.Text = reader["Provincia"].ToString();
                            cmbCiudad.Text = reader["Ciudad"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                        }
                    }
                    else
                    {
                        // Si no hay coincidencia, no hacer nada (empieza a llenar manualmente)
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el proveedor: " + ex.Message);
                }
            }
        }

        private void cmbIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
