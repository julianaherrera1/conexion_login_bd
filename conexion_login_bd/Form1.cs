﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace conexion_login_bd
{
    public partial class frm_1 : Form
    {
        public frm_1()
        {
            InitializeComponent();
        }

        int intentos = 0;
        // Conexion con base de datos
         SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-KQUVA2I\SQLEXPRESS;Initial Catalog=LOGIN;Integrated Security=True ");
        
    



        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            // Cerrar formulario
            this.Close();
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            // Minimizar formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar el campo del usuario
                if (string.IsNullOrWhiteSpace(txt_usuario.Text))
                {
                    errorProvider1.SetError(txt_usuario, "Ingrese un nombre de usuario");
                    return;
                }
                if (txt_usuario.Text == "")
                {
                    errorProvider1.SetError(txt_usuario, " Debe llenar el campo");
                    return;
                }
                errorProvider1.SetError(txt_usuario, "");

                // Validar el campo de la contraseña
                if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
                {
                    errorProvider1.SetError(txt_contraseña, "Ingrese una contraseña");
                    return;
                }
                if (txt_contraseña.Text == "")
                {
                    errorProvider1.SetError(txt_contraseña, " Debe llenar el campo");
                }
                errorProvider1.SetError(txt_contraseña, "");

                // Abrir la conexión a la base de datos
                conexion.Open();

                // Crear un objeto SqlCommand para ejecutar la consulta
                SqlCommand comando = new SqlCommand("SELECT USUARIO, CONTRASENA FROM Personas WHERE USUARIO = @vusuario AND CONTRASENA = @vcontrasena", conexion);

                // Asignar los valores de los parámetros de la consulta
                comando.Parameters.AddWithValue("@vusuario", txt_usuario.Text);
                comando.Parameters.AddWithValue("@vcontrasena", txt_contraseña.Text);

                // Ejecutar la consulta y obtener un lector de datos SqlDataReader
                SqlDataReader lector = comando.ExecuteReader();

                // Verificar si se encontraron registros coincidentes
                if (lector.Read())
                {
                    MessageBox.Show("Ingreso correcto");
                    intentos = 0;
                    frm2 frm = new frm2();
                    frm.Show();
                }
                else
                {
                    intentos++;
                    MessageBox.Show("Usuario y/o contraseña incorrectos");
                    if(intentos == 3)
                    {
                        MessageBox.Show(" Has excedido los intentos. La aplicación se cerrará");
                        Application.Exit();
                    }
                }

                // Cerrar el lector de datos
                lector.Close();
                limpiar();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexión o consulta: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


        }

        private void img_ver_contraseña_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.PasswordChar == '*')
            {
                // mostrar contraseña
                txt_contraseña.PasswordChar = '\0';
            }
            else
            {
                // ocultar contraseña
                txt_contraseña.PasswordChar = '*';
            }
        }

        public void limpiar()
        {
            txt_usuario.Text = "";
            txt_contraseña.Text = "";
            txt_usuario.Focus();
        }
        
    }
}
