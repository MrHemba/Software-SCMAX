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
            CargarIdentificacion();
            txtRucCi.Leave += new EventHandler(txtRucCi_Leave);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void FormProveedor_Load(object sender, EventArgs e)
        {

        }
        private void txtRucCi_Leave(object sender, EventArgs e)
        {
            // Obtener el valor del campo Identificación (RUC/CI)
            string identificacion = txtRucCi.Text.Trim();

            if (!string.IsNullOrEmpty(identificacion))
            {
                // Llamar a un método para buscar el proveedor en la base de datos
                BuscarProveedorPorIdentificacion(identificacion);
            }
        }
        private void BuscarProveedorPorIdentificacion(string identificacion)
        {
            try
            {
                conexion.Open();

                // Consulta para buscar el proveedor por identificación
                string query = "SELECT * FROM Proveedores WHERE Identificacion = @Identificacion";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Identificacion", identificacion);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Si se encuentra un registro
                        {
                            // Rellenar los campos del formulario con los datos de la base de datos
                            txtRazonSocial.Text = reader["Razon_Social"].ToString();
                            txtComercial.Text = reader["Nombre_Comercial"].ToString();
                            txtApellidosRep.Text = reader["Apellidos_Representante"].ToString();
                            txtNombreRep.Text = reader["Nombres_Representante"].ToString();
                            txtCedula.Text = reader["Cedula"].ToString();
                            cmbContribuyente.SelectedItem = reader["Contribuyente"].ToString();
                            cmbPais.SelectedItem = reader["Pais"].ToString();
                            cmbProvincia.SelectedItem = reader["Provincia"].ToString();
                            cmbCiudad.SelectedItem = reader["Ciudad"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                        }
                        else
                        {
                            // No se encontró ningún proveedor con esa identificación, no hacer nada
                            MessageBox.Show("No se encontró ningún proveedor con esa identificación.");
                            LimpiarCampos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void LimpiarCampos()
        {
            txtRazonSocial.Clear();
            txtComercial.Clear();
            txtApellidosRep.Clear();
            txtNombreRep.Clear();
            txtCedula.Clear();
            cmbContribuyente.SelectedIndex = -1;
            cmbPais.SelectedIndex = -1;
            cmbProvincia.SelectedIndex = -1;
            cmbCiudad.SelectedIndex = -1;
            txtTelefono.Clear();
            
        }

        private void CargarIdentificacion()
        {
            try
            {
                conexion.Open();

                string query = "SELECT Indentificacion FROM Tipoid";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())

                    {
                        while (reader.Read())
                        {

                            cmbIdentificacion.Items.Add(reader["Indentificacion"].ToString());
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
            CargarIdentificacion();
        }

        private void btnGuardr_Click(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();

                //LLamar a procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CrearProveedor", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tipo_Identificacion", cmbIdentificacion.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Identificacion", txtRucCi.Text);
                    cmd.Parameters.AddWithValue("@Razon_Social", txtRazonSocial.Text);
                    cmd.Parameters.AddWithValue("@Nombre_Comercial", txtComercial.Text);
                    cmd.Parameters.AddWithValue("@Apellidos_Representante", txtApellidosRep.Text);
                    cmd.Parameters.AddWithValue("@Nombres_Representante", txtNombreRep.Text);
                    cmd.Parameters.AddWithValue("@Cedula", txtCedula.Text);
                    cmd.Parameters.AddWithValue("@Contribuyente", cmbContribuyente.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Pais", cmbPais.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Provincia", cmbProvincia.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ciudad", cmbCiudad.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Estado", "1");


                    int rowsAffected = cmd.ExecuteNonQuery();

                    object newId = cmd.ExecuteScalar();
                    if (rowsAffected > 0)
                    {
                        LimpiarCampos();
                        MessageBox.Show("Proveedor registrado correctamente.");

                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}

