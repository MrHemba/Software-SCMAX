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
    public partial class FormPrincipal : Form
    {
        private string usuarioRol;

        public FormPrincipal(string rol)
        {
            InitializeComponent();
            usuarioRol = rol;  // Asignar el rol recibido
            // Llama al método para configurar el menú según el rol
            ConfigurarMenuSegunRol();
        }

        // Método para configurar el menú según el rol
        private void ConfigurarMenuSegunRol()
        {
            // Primero, ocultamos todas las opciones del menú para tener control completo
            menuAdmin.Visible = false;  // Solo para admin
            menuUser.Visible = false;  // Para todos

            // Verifica el rol y ajusta la visibilidad del menú
            if (usuarioRol.Trim() == "admin")
            {
                menuUser.Visible = true;
                menuAdmin.Visible = true;  // Mostrar opción para admin
            }

            // Mostrar la opción visible para todos los roles
            if (usuarioRol.Trim() == "usuario")
            {
                menuUser.Visible = true;  // Mostrar opción para admin y usuario
            }
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuAdmin_Click(object sender, EventArgs e)
        {
           
        }

        private void crearUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreateUser formCreateUser = new FormCreateUser();
            formCreateUser.Show();
         
        }

        private void modificarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormModUser formModUser = new FormModUser();
            formModUser.Show();
            
        }

        private void crearProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProveedor formProveedor = new FormProveedor();
            formProveedor.Show();
        }

        private void reporteProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReporte formReporte = new FormReporte();
            formReporte.Show();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}