using DCICC.AccesoDatos.ConsultasBD;
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
        /// Método para actualizar un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using(var cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set id_rol = @ir,nombres_usuario = @nu,nick_usuario = @niu,password_usuario = @pu,correo_usuario = @cu,telefono_usuario = @tu,telefonocelular_usuario = @tcu,direccion_usuario = @du,habilitado_usuario = @hu where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value=infoUsuario.IdRol;
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value=infoUsuario.NombresUsuario;
                    cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario;
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                    cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario;
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value =infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = "ALTER USER " + infoUsuario.NickUsuario + " with password '" + ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario) + "';";
                using (var cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
        /// <summary>
        /// Método para actualizar el perfil de un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionPerfilUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set nombres_usuario = @nu,nick_usuario = @niu,password_usuario = @pu,correo_usuario = @cu,telefono_usuario = @tu,telefonocelular_usuario = @tcu,direccion_usuario = @du where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario;
                    cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario;
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                    cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario;
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = "ALTER USER " + infoUsuario.NickUsuario + " with password '" + ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario) + "';";
                using (var cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
        /// <summary>
        /// Método para actualizar el perfil de un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionPasswordUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set password_usuario = @pu where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = "ALTER USER " + infoUsuario.NickUsuario + " with password '" + ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario) + "';";
                using (var cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
        /// <summary>
        /// Método para actualizar el estado de un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionEstadoUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set habilitado_usuario = @hu where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value =infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
