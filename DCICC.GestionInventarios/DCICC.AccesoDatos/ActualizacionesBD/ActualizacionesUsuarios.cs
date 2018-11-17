using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesUsuarios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo usuario en la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                using (var cmd = new NpgsqlCommand("update dcicc_usuarios set id_rol= :ir, nombres_usuario = :nu," +
                    "nick_usuario= :niu, " +
                    "password_usuario= :pu," +
                    " correo_usuario= :cu, telefono_usuario= :tu," +
                    " telefonocelular_usuario= :tcu," +
                    " direccion_usuario= :du," +
                    "habilitado_usuario= :hu where id_usuario= :iu", conn_BD))
                {
                    cmd.Parameters.AddWithValue("iu", infoUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("ir", infoUsuario.IdRol);
                    cmd.Parameters.AddWithValue("nu", infoUsuario.NombresUsuario);
                    cmd.Parameters.AddWithValue("niu", infoUsuario.NickUsuario);
                    cmd.Parameters.AddWithValue("pu", ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario));
                    cmd.Parameters.AddWithValue("cu", infoUsuario.CorreoUsuario);
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.AddWithValue("hu", infoUsuario.HabilitadoUsuario);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
    }
}
