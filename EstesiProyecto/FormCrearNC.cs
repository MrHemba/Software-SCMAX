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

    public partial class FormCrearNC : Form
    {
        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public string NumeroFactura { get; set; }
        public string NumeroNC { get; set; }
        public decimal ValorNC { get; set; }

        public string MotivoNC { get; set; }
     

        private FormReporte formReporte;

        // Constructor unificado que admite tanto crear como editar NC
        public FormCrearNC(FormReporte parentForm, string Motivo, string factura, string numNC = "", decimal valorNC = 0)
        {
            InitializeComponent();
            formReporte = parentForm;
            MotivoNC = Motivo;
            NumeroFactura = factura;
            NumeroNC = numNC;
            ValorNC = valorNC;
           
            CargarMotivo();

            // Inicializa los controles con los datos existentes si son válidos
            txtnumFactura.Text = NumeroFactura;
            txtNC.Text = numNC;
            txtValorNC.Text = valorNC != 0 ? valorNC.ToString() : "";
        }
        public string TituloFormulario
        {
            set { lblTitulo.Text = value; } // Asumiendo que tu label se llama lblTitulo
        }
        private void CargarMotivo()
        {
            try
            {
                conexion.AbrirConexion();
                SqlConnection conn = conexion.ObtenerConexion();

                string query = "select id_motivo,motivo from Motivo_Ret";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // Crear una nueva fila para el valor vacío
                        DataRow row = dt.NewRow();
                        row["id_motivo"] = 0; // Un Id que no existe en la tabla, como 0
                        row["motivo"] = ""; // Texto que aparecerá en el ComboBox
                        dt.Rows.InsertAt(row, 0); // Insertar en la primera posición

                        cmbMotivo.DisplayMember = "motivo";
                        cmbMotivo.ValueMember = "id_motivo";
                        cmbMotivo.DataSource = dt;

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
            decimal valorNC;

            // Verifica si txtFacturado es nulo o está vacío, y asigna 0 si es el caso
            if (string.IsNullOrEmpty(txtValorNC.Text))
            {
                valorNC = 0; // Asigna 0 si está vacío
            }
            else
            {
                valorNC = Convert.ToDecimal(txtValorNC.Text); // Realiza la conversión si tiene un valor
            }
            // Obtener los datos desde los controles y asignarlos a las propiedades
            NumeroNC = txtNC.Text;
            ValorNC = valorNC;
            MotivoNC = cmbMotivo.Text;

          

            // Establece el resultado del diálogo como OK para indicar éxito
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCrearNC_Load(object sender, EventArgs e)
        {
            // Cualquier lógica adicional al cargar el formulario
        }
    }


}
