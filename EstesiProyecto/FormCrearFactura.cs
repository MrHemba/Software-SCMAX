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

        public FormCrearFactura()
        {
            InitializeComponent();
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           

                //// Insertar el reporte de pago
                //using (SqlCommand cmd = new SqlCommand("sp_InsertarReportePago", conn))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;

                //    // Asignar parámetros
                //    cmd.Parameters.AddWithValue("@Id_Factura", txtIdReporte.Text);
                //    cmd.Parameters.AddWithValue("@Id_Sucursal", Convert.ToInt32(cmbSucursal.SelectedValue));
                //    cmd.Parameters.AddWithValue("@Id_Proveedor", idProveedor); // Usar el Id_Proveedor obtenido
                //    cmd.Parameters.AddWithValue("@Num_Factura", txtNFactura.Text);
                //    cmd.Parameters.AddWithValue("@Num_Retencion", txtNRetencion.Text);
                //    cmd.Parameters.AddWithValue("@Fecha_Facturacion", dtpFacturacion.Value);
                //    cmd.Parameters.AddWithValue("@Fecha_Vencimiento", dtpVencimiento.Value);
                //    cmd.Parameters.AddWithValue("@Fecha_Sugerida", dtpSugerido.Value);
                //    cmd.Parameters.AddWithValue("@Num_NC", txtNNotaCredito.Text);
                //    cmd.Parameters.AddWithValue("@Id_Motivo", Convert.ToInt32(cmbMotivo.SelectedValue));
                //    cmd.Parameters.AddWithValue("@Neto_Pagar", Convert.ToDecimal(txtNetoCancelar.Text));
                //    cmd.Parameters.AddWithValue("@Valor_Factura", Convert.ToDecimal(txtFacturado.Text));
                //    cmd.Parameters.AddWithValue("@Valor_NC", Convert.ToDecimal(txtValorNC.Text));

                //}
            
                    NuevaFactura = new Factura
                    {
                       
                        NFactura = txtNFactura.Text,
                        NRetencion = txtNRetencion.Text,
                        FechaFacturacion = dtpFacturacion.Value,
                        FechaVencimiento = dtpVencimiento.Value,
                        ValorFacturado = Convert.ToDecimal(txtFacturado.Text),           
                        FechaRecibido = dtpRecibido.Value
                    };
                this.DialogResult = DialogResult.OK;
                this.Close(); // Cerrar el formulario
           
        }

        private void FormCrearFactura_Load(object sender, EventArgs e)
        {
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
}
