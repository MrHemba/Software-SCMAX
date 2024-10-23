using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstesiProyecto
{
    internal class ConexionSQL
    {
        private static ConexionSQL instancia = null;

        // Cadena de conexión a la base de datos
        private string connectionString;

        // Objeto SqlConnection que será utilizado globalmente
        private SqlConnection connection;

        // Constructor privado para evitar la creación directa de la clase
        private ConexionSQL()
        {
            // Aquí estableces tu cadena de conexión
            connectionString = "server=DESKTOP-9DGCSEO\\SQLEXPRESS01; database=SYSProvedores; integrated security=true";
            connection = new SqlConnection(connectionString);
        }
        // Método estático para obtener la instancia de la clase
        public static ConexionSQL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ConexionSQL();
            }
            return instancia;
        }

        // Método para abrir la conexión (se hace por separado)
        public void AbrirConexion()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                Console.WriteLine("Conexión abierta.");
            }
        }

        // Método para cerrar la conexión
        public void CerrarConexion()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Conexión cerrada.");
            }
        }

        // Obtener el objeto SqlConnection (sin abrirla automáticamente)
        public SqlConnection ObtenerConexion()
        {
            return connection;
        }

        // Método para verificar el estado de la conexión
        public string EstadoConexion()
        {
            return connection.State.ToString();
        }
    }
}