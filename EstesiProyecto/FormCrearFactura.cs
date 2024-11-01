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

    public partial class FormCrearFactura : Form
    {

            ConexionSQL conexion = ConexionSQL.GetInstancia();
            public Factura NuevaFactura { get; private set; }

            // Constructor para crear una nueva factura
            public FormCrearFactura()
            {
                InitializeComponent();
                NuevaFactura = new Factura();  // Inicializar con una nueva factura
            }
        public string TituloFormulario
        {
            set { lblTituloFac.Text = value; } // Asumiendo que tu label se llama lblTitulo
        }

        // Constructor para editar una factura existente
        public FormCrearFactura(Factura facturaAEditar) : this()
            {
         
            // Cargar los datos de la factura en los controles
            txtNFactura.Text = facturaAEditar.NFactura;
                txtNRetencion.Text = facturaAEditar.NRetencion;
                dtpFacturacion.Value = facturaAEditar.FechaFacturacion;
                dtpVencimiento.Value = facturaAEditar.FechaVencimiento;
                txtFacturado.Text = facturaAEditar.ValorFacturado.ToString();
                dtpRecibido.Value = facturaAEditar.FechaRecibido;

                // Asignar la factura existente para que se actualicen sus valores
                NuevaFactura = facturaAEditar;
            }

            private void btnGuardar_Click(object sender, EventArgs e)
            {
            // Obtener las fechas de los DateTimePickers
            DateTime fechaRecibida = dtpRecibido.Value; // Suponiendo que el DateTimePicker se llama dtpFechaRecibida
            DateTime fechaFacturada = dtpFacturacion.Value; // Suponiendo que el DateTimePicker se llama dtpFechaFacturado

            // Calcular la fecha de vencimiento sumando 30 días a la fecha recibida
            DateTime fechaSugerido = fechaRecibida.AddDays(30);
           
            // Actualizar los datos de la factura con los valores de los controles
            NuevaFactura.NFactura = txtNFactura.Text;
                NuevaFactura.NRetencion = txtNRetencion.Text;
                NuevaFactura.FechaFacturacion = dtpFacturacion.Value;
                NuevaFactura.FechaVencimiento = dtpVencimiento.Value;
                NuevaFactura.FechaRecibido = dtpRecibido.Value;
           
                NuevaFactura.ValorFacturado = Convert.ToDecimal(txtFacturado.Text);
            NuevaFactura.FechaSugerida = fechaSugerido;

                this.DialogResult = DialogResult.OK;  // Indicar que se cerró correctamente
                this.Close();
            }
        }

        public class Factura
        {
           
            public string NFactura { get; set; }
            public string NRetencion { get; set; }
            public DateTime FechaFacturacion { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public DateTime FechaRecibido { get; set; }
            public DateTime FechaSugerida { get; set; }
            public string NotaCredito { get; set; }
            public string Motivo { get; set; }
            public decimal ValorNC { get; set; }
            public decimal ValorFacturado { get; set; }
            public decimal NetoPagar { get; set; }
            
        }
    }
