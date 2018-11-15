using DCICC.Entidades.EntidadesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesUsuarios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo usuario en la base de datos.
        /// </summary>
        /// <returns></returns>
        public Boolean RegistroUsuario(Usuarios infoUsuario)
        {
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_usuarios (id_rol,nombres_usuario,nick_usuario,password_usuario,correo_usuario,telefono_usuario,telefonocelular_usuario,direccion_usuario,habilitado_usuario) VALUES (@ir,@nu,@niu,@pu,@cu,@tu,@tcu,@du,@hu)", conn_BD))
                {
                    cmd.Parameters.AddWithValue("ir", infoUsuario.IdRol);
                    cmd.Parameters.AddWithValue("nu", infoUsuario.NombresUsuario);
                    cmd.Parameters.AddWithValue("niu", infoUsuario.NickUsuario);
                    cmd.Parameters.AddWithValue("pu", ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario));
                    cmd.Parameters.AddWithValue("cu", infoUsuario.CorreoUsuario);
                    cmd.Parameters.AddWithValue("tu", infoUsuario.TelefonoUsuario);
                    cmd.Parameters.AddWithValue("tcu", infoUsuario.TelefonoCelUsuario);
                    cmd.Parameters.AddWithValue("du", infoUsuario.DireccionUsuario);
                    cmd.Parameters.AddWithValue("hu", infoUsuario.HabilitadoUsuario);
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
