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
        public FormReporte formReporte;
        private string numeroFactura;

        // Constructor que recibe una referencia al formulario principal (FormReporte)
        public FormCrearNC(FormReporte formReporte, string factura)
        {
            InitializeComponent();
            this.formReporte = formReporte; // Guardamos la referencia
            numeroFactura = factura;
        }

        public void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtén los datos de la NC
            string numNC = txtNC.Text;
            decimal valorNC = Convert.ToDecimal(txtValorNC.Text);

            // Llama a la función en el formulario principal para agregar la NC
            formReporte.AgregarNC_Factura(numeroFactura, numNC, valorNC);

            // Cierra el formulario de creación de NC
            this.Close();
        }
    }

}
