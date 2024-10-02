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
    public partial class FormModUser : Form
    {
        SqlConnection conexion = new SqlConnection("server=DESKTOP-9DGCSEO\\SQLEXPRESS01; database=SYSProvedores; integrated security=true");
        public FormModUser()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
            string buscarNombre = txtBuscarNombre.Text;

            if (string.IsNullOrEmpty(buscarNombre))
            {
                MessageBox.Show("Por favor, ingresa un ID o un Nombre de Usuario para buscar.");
                return;
            }

            try
            {
                conexion.Open();

                string query;
                SqlCommand cmd;

               
                    query = "SELECT UsuarioID, NombreUsuario, Contraseña, Rol FROM Usuarios WHERE NombreUsuario = @nombreUsuario";
                    cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombreUsuario", buscarNombre);
               

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Rellenar los campos del formulario con los datos obtenidos
                        txtUsuarioID.Text = reader["UsuarioID"].ToString();
                        txtUser.Text = reader["NombreUsuario"].ToString();
                        txtPassword.Text = "";
                        cbmRoles.SelectedItem = reader["Rol"].ToString(); // Seleccionar el rol en el ComboBox
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el usuario: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

        }

        private void cbmRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormModUser_Load(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string query = "SELECT Rol FROM Roles"; // Cambia esto si tu tabla tiene un nombre diferente
                SqlCommand cmd = new SqlCommand(query, conexion);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbmRoles.Items.Add(reader["Rol"].ToString());
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
            int id = Convert.ToInt32(txtUsuarioID.Text);
            string username = txtUser.Text;
            string password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text); // Hashear la nueva contraseña
            string rol = cbmRoles.SelectedItem?.ToString(); // Obtener el rol seleccionado

            try
            {
                conexion.Open();
                string query = @"UPDATE Usuarios 
                         SET NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Rol = @Rol 
                         WHERE UsuarioID = @UsuarioID";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@NombreUsuario", username);
                    cmd.Parameters.AddWithValue("@Contraseña", password);
                    cmd.Parameters.AddWithValue("@Rol", rol); // Enviar el rol seleccionado
                    cmd.Parameters.AddWithValue("@UsuarioID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Usuario actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el usuario.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Asegúrate de que lblUsuarioID tenga el ID del usuario que deseas eliminar
            int id;
            if (int.TryParse(txtUsuarioID.Text, out id))
            {
                // Mostrar un cuadro de diálogo de confirmación
                var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?",
                                                     "Confirmar eliminación",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        conexion.Open();

                        // Consulta SQL para eliminar el usuario
                        string query = "DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID";
                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioID", id);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Usuario eliminado correctamente.");
                                // Limpiar los campos después de eliminar
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("Error al eliminar el usuario.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("ID de usuario no válido.");
            }
        }

        private void LimpiarCampos()
        {
            // Método para limpiar los campos del formulario
            txtUsuarioID.Text = string.Empty;
            txtUser.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cbmRoles.SelectedIndex = -1; // Deseleccionar el ComboBox
        }

    }
}
