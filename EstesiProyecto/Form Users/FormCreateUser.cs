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
    public partial class FormCreateUser : Form
    {
        SqlConnection conexion = new SqlConnection("server=DESKTOP-9DGCSEO\\SQLEXPRESS01; database=SYSProvedores; integrated security=true");
        public FormCreateUser()
        {
            InitializeComponent();
            CargarRoles(); // Llama a la función para cargar roles
        }

        // Función para cargar los roles en el ComboBox
        private void CargarRoles()
        {
            try
            {
                conexion.Open();
                // Consulta SQL para obtener los roles
                string query = "SELECT Rol FROM Roles"; // Asume que la tabla Roles tiene una columna 'NombreRol'

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Agregar cada rol al ComboBox
                            cbmRoles.Items.Add(reader["Rol"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text); // Hasheamos la contraseña
            string rol = cbmRoles.SelectedItem?.ToString(); // Obtener el valor seleccionado del ComboBox
            DateTime fechaCreacion = DateTime.Now; // Fecha de creación automática
            byte estado = 1;  // Estado fijo a 1 (activo)

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rol))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            try
            {
                conexion.Open();

                //LLamar a procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CrearUsuario", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreUsuario", username);
                    cmd.Parameters.AddWithValue("@Contraseña", password); // Contraseña hasheada
                    cmd.Parameters.AddWithValue("@Rol", rol); // Enviar el rol seleccionado
                    cmd.Parameters.AddWithValue("@FechaCreacion", fechaCreacion);  // Fecha de creación automática
                    cmd.Parameters.AddWithValue("@Estado", estado);  // Estado siempre en 1

                    // Ejecutar la consulta y obtener el nuevo ID generado
                    object newId = cmd.ExecuteScalar();

                    if (newId != null)
                    {
                        txtUsuarioID.Text = newId.ToString(); // Mostrar el ID autoincrementable en el Label
                        MessageBox.Show("Usuario registrado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el usuario.");
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




        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
