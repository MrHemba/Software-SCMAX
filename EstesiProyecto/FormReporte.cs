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
using static EstesiProyecto.FormCrearFactura;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EstesiProyecto
{

    public partial class FormReporte : Form
    {
        private string usuarioActual;
        ConexionSQL conexion = ConexionSQL.GetInstancia();
        public FormReporte()
        {
            InitializeComponent();

           
        }


        private void FormReporte_Load(object sender, EventArgs e)
        {
           
            dgvFactura.Columns.Add("Sucursal", "Sucursal");
            dgvFactura.Columns.Add("Proveedor", "Proveedor");
            dgvFactura.Columns.Add("Nº Retencion", "Nº Retencion");
            dgvFactura.Columns.Add("Fecha Facturacion", "Fecha Facturacion");
            dgvFactura.Columns.Add("Fecha Vencimiento", "Fecha Vencimiento");
            dgvFactura.Columns.Add("Fecha Sugerida", "Fecha Sugerida");
            dgvFactura.Columns.Add("Nota Credito", "Nota Credito");
            dgvFactura.Columns.Add("Motivo", "Motivo");
            dgvFactura.Columns.Add("Valor NC", "Valor NC");
            dgvFactura.Columns.Add("Valor Facturado", "Valor Facturado");
            dgvFactura.Columns.Add("Neto a Pagar", "Neto a Pagar");

        }
       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           
        }


        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormCrearFactura formFactura = new FormCrearFactura();

            if (formFactura.ShowDialog() == DialogResult.OK)
            {
                Factura nuevaFactura = formFactura.NuevaFactura;

                // Agregar la nueva factura al DataGridView
                dgvFactura.Rows.Add(
                    nuevaFactura.Sucursal,
                    nuevaFactura.Proveedor,
                    nuevaFactura.NRetencion,
                    nuevaFactura.FechaFacturacion.ToShortDateString(),
                    nuevaFactura.FechaVencimiento.ToShortDateString(),
                    nuevaFactura.FechaSugerida.ToShortDateString(),
                    nuevaFactura.NotaCredito,
                    nuevaFactura.Motivo,
                    nuevaFactura.ValorNC,
                    nuevaFactura.ValorFacturado,
                    nuevaFactura.NetoPagar
                );
            }

        }
    }
}