using System;
using System.Data;
using System.Data.SqlClient;
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
            CargarCiudad();
            CargarPais();
            CargarProvincia();
            txtRucCi.Leave += new EventHandler(txtRucCi_Leave);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(txtRucCi.Text, out id))
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

                        using (SqlCommand cmd = new SqlCommand("DeleteProveedor", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Identificacion", id);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Proveedor eliminado correctamente.");
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
                BuscarProveedorPorBloques(identificacion);
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
       
            private void BuscarProveedorPorBloques(string identificacion)
            {
            int bloqueTamano = 100; // Tamaño del bloque
            int offset = 0; // Empezar desde la primera fila
            bool encontrado = false;

            try
            {
                conexion.Open();

                while (!encontrado)
                {
                    using (SqlCommand cmd = new SqlCommand("BuscarProveedorPorBloques", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@BlockSize", bloqueTamano);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Rellenar los campos si se encuentra el proveedor
                                txtRazonSocial.Text = reader["Razon_Social"].ToString();
                                txtComercial.Text = reader["Nombre_Comercial"].ToString();
                                txtApellidosRep.Text = reader["Apellidos_Representante"].ToString();
                                txtNombreRep.Text = reader["Nombres_Representante"].ToString();
                                txtCedula.Text = reader["Cedula"].ToString();
                                cmbContribuyente.SelectedItem = reader["Contribuyente"].ToString();

                                // Cambios aquí para obtener los valores de Pais, Provincia y Ciudad
                                cmbPais.SelectedItem = reader["Pais"] != DBNull.Value ? reader["Pais"].ToString() : string.Empty;
                                cmbProvincia.SelectedItem = reader["Provincia"] != DBNull.Value ? reader["Provincia"].ToString() : string.Empty;
                                cmbCiudad.SelectedItem = reader["Nombre_Ciudad"] != DBNull.Value ? reader["Nombre_Ciudad"].ToString() : string.Empty;

                                txtTelefono.Text = reader["Telefono"].ToString();

                                encontrado = true; // Salir del bucle porque ya lo encontró
                            }
                            else if (!reader.HasRows)
                            {
                                // Si no se encontraron más resultados en el bloque, terminar la búsqueda
                                MessageBox.Show("Proveedor no encontrado.");
                                break;
                            }
                        }
                    }

                    // Mover el siguiente bloque
                    offset += bloqueTamano;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

            private void cmbIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarIdentificacion();
        }

        private void btnGuardr_Click(object sender, EventArgs e)
        {
            //Validar que todos los campos esten llenos
            if (string.IsNullOrWhiteSpace(txtRucCi.Text) ||
        cmbIdentificacion.SelectedItem == null ||
        string.IsNullOrWhiteSpace(txtRazonSocial.Text) ||
        string.IsNullOrWhiteSpace(txtComercial.Text) ||
        string.IsNullOrWhiteSpace(txtApellidosRep.Text) ||
        string.IsNullOrWhiteSpace(txtNombreRep.Text) ||
        string.IsNullOrWhiteSpace(txtCedula.Text) ||
        cmbContribuyente.SelectedItem == null )
        //cmbPais.SelectedItem == null ||
       // cmbProvincia.SelectedItem == null ||
                // cmbCiudad.SelectedItem == null )
            {
                MessageBox.Show("Todos los campos obligatorios deben estar llenos antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución si algún campo está vacío
            }
            try
            {
                // Verificar si la identificación ya existe en la base de datos
                if (IdentificacionExiste(txtRucCi.Text))
                {
                    MessageBox.Show("El proveedor con esta identificación ya existe. No se puede guardar.");
                    return; // Cancelar el guardado si la identificación ya existe
                }

                // Abrir la conexión
                conexion.Open();

                // LLamar al procedimiento almacenado para insertar el proveedor
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
                    cmd.Parameters.AddWithValue("@Pais", Convert.ToInt32(cmbPais.SelectedValue));
                    cmd.Parameters.AddWithValue("@Provincia", Convert.ToInt32(cmbProvincia.SelectedValue));
                    cmd.Parameters.AddWithValue("@Ciudad", Convert.ToInt32(cmbCiudad.SelectedValue));
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Estado", "1");

                    // Ejecutar la consulta y obtener el nuevo ID generado
                   cmd.ExecuteNonQuery();
                    // Verificar si se seleccionó una ciudad
                   

                    MessageBox.Show("Proveedor registrado correctamente.");
                    LimpiarCampos();
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

        private bool IdentificacionExiste(string identificacion)
        {
            bool existe = false;

            try
            {
                // Abrir la conexión
                conexion.Open();

                // Llamar al procedimiento almacenado para verificar si la identificación existe
                using (SqlCommand cmd = new SqlCommand("VerificarIdentificacion", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Identificacion", identificacion);

                    // Ejecutar el procedimiento y leer el resultado
                    object result = cmd.ExecuteScalar();

                    if (result != null && Convert.ToInt32(result) == 1)
                    {
                        existe = true; // La identificación ya existe
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la identificación: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return existe;
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdentificacion_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void CargarPais()
        {
            try
            {
                conexion.Open();
                string query = "SELECT Id_Pais, Pais FROM Pais";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["Id_Pais"] = 0; // Un Id que no existe en la tabla, como 0
                        row["Pais"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbPais.DisplayMember = "Pais"; // Lo que se muestra en el ComboBox
                        cmbPais.ValueMember = "Id_Pais";       // Lo que se guarda (el Id)
                        cmbPais.DataSource = dt;

                        // Seleccionar el valor vacío por defecto
                        cmbPais.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los países: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        private void CargarProvincia()
        {
            try
            {
                conexion.Open();
                string query = "SELECT Id_Provincia, Provincia FROM Provincia";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["Id_Provincia"] = 0; // Un Id que no existe en la tabla, como 0
                        row["Provincia"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbProvincia.DisplayMember = "Provincia";
                        cmbProvincia.ValueMember = "Id_Provincia";
                        cmbProvincia.DataSource = dt;

                        // Seleccionar el valor vacío por defecto
                        cmbProvincia.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las provincias: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        private void CargarCiudad()
        {

            try
            {
                conexion.Open();
                string query = "SELECT Id_Ciudad, Nombre_Ciudad FROM Ciudad ";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                  
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["Id_Ciudad"] = 0; // Un Id que no existe en la tabla, como 0
                        row["Nombre_Ciudad"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbCiudad.DisplayMember = "Nombre_Ciudad";
                        cmbCiudad.ValueMember = "Id_Ciudad";
                        cmbCiudad.DataSource = dt;

                        // Seleccionar el valor vacío por defecto
                        cmbCiudad.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las ciudades: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        

    }
}

