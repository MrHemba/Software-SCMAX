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
        ConexionSQL conexion = ConexionSQL.GetInstancia();
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
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                using (SqlCommand cmd = new SqlCommand("ValidarLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreUsuario", username);

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
                conexion.CerrarConexion();
            }
        }


            private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}