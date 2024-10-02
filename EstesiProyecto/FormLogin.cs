using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EstesiProyecto
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        // Conexión a la base de datos
        SqlConnection conexion = new SqlConnection("server=DESKTOP-9DGCSEO\\SQLEXPRESS01; database=SYSProvedores; integrated security=true");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }



        private void txtInicioSesion_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtContrase.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingresa tu usuario y contraseña.");
                return;
            }

            try
            {
                conexion.Open();

                // Consulta SQL para obtener la contraseña hasheada y el rol del usuario
                string query = "SELECT Contraseña, Rol FROM Usuarios WHERE NombreUsuario = @username";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    // Ejecutar la consulta y obtener la contraseña y el rol del usuario
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())  // Si se encuentra el usuario
                        {
                            string storedHashedPassword = reader["Contraseña"].ToString();
                            string userRole = reader["Rol"].ToString();  // Obtener el rol del usuario

                            // Verificar la contraseña
                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);

                            if (isPasswordValid)
                            {
                                reader.Close();  // Cerrar el DataReader antes de ejecutar cualquier otro comando

                                // Actualizar la fecha de la última sesión
                                string updateQuery = "UPDATE Usuarios SET Ultima_Sesion = @UltimaSesion WHERE NombreUsuario = @username";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conexion))
                                {
                                    updateCmd.Parameters.AddWithValue("@UltimaSesion", DateTime.Now);
                                    updateCmd.Parameters.AddWithValue("@username", username);
                                    updateCmd.ExecuteNonQuery();
                                }

                                // Abrir el formulario principal pasando el rol del usuario
                                FormPrincipal formPrincipal = new FormPrincipal(userRole);
                                formPrincipal.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Contraseña incorrecta.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuario no encontrado.");
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


            private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}