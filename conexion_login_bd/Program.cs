using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conexion_login_bd
{
    /// <summary>
    /// Creado por: Juliana Herrera
    /// Fecha: 11/mayo/2023
    /// Inicio de sesion conectado con base de datos (sql server)
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_1());
        }
    }
}
