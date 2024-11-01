using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstesiProyecto
{
    public partial class FormCrearNC : Form
    {
        public string NumeroFactura { get; set; }
        public string NumeroNC { get; set; }
        public decimal ValorNC { get; set; }

        private FormReporte formReporte;

        // Constructor unificado que admite tanto crear como editar NC
        public FormCrearNC(FormReporte parentForm, string factura, string numNC = "", decimal valorNC = 0)
        {
            InitializeComponent();
            formReporte = parentForm;
            NumeroFactura = factura;
            NumeroNC = numNC;
            ValorNC = valorNC;

            // Inicializa los controles con los datos existentes si son válidos
            txtnumFactura.Text = NumeroFactura;
            txtNC.Text = numNC;
            txtValorNC.Text = valorNC != 0 ? valorNC.ToString() : "";
        }
        public string TituloFormulario
        {
            set { lblTitulo.Text = value; } // Asumiendo que tu label se llama lblTitulo
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
