using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;

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
        /// Método para actualizar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                string pwdUsuario = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                ConsultasUsuarios objConsultaUsuarios = new ConsultasUsuarios();
                if (objConsultaUsuarios.ObtenerUsuarioPorNick(infoUsuario.NickUsuario).ObjetoInventarios == null)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set id_rol = @ir,nombres_usuario = @nu,password_usuario = @pu,correo_usuario = @cu,telefono_usuario = @tu,telefonocelular_usuario = @tcu,direccion_usuario = @du,habilitado_usuario = @hu where id_usuario = @iu", conn_BD))
                    {
                        cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdRol;
                        cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario.Trim();
                        cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pwdUsuario.Trim();
                        cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario.Trim();
                        cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                        cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    msjUsuarios = ActualizacionNickUsuario(infoUsuario);
                    if (infoUsuario.NombreRolAntiguo!=null)
                    {
                        ActualizarRolUsuario(infoUsuario.NombreRolAntiguo.Trim(), infoUsuario.NombreRol.Trim(), infoUsuario.NickUsuario.Trim());
                    }
                    conn_BD.Close();
                }
                else
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set id_rol = @ir,nombres_usuario = @nu,nick_usuario = @niu,password_usuario = @pu,correo_usuario = @cu,telefono_usuario = @tu,telefonocelular_usuario = @tcu,direccion_usuario = @du,habilitado_usuario = @hu where id_usuario = @iu", conn_BD))
                    {
                        cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdRol;
                        cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario.Trim();
                        cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario.Trim();
                        cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pwdUsuario.Trim();
                        cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario.Trim();
                        cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario.Trim() : DBNull.Value;
                        cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                        cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                        cmd.ExecuteNonQuery();
                    }
                    if (infoUsuario.NombreRolAntiguo != null)
                    {
                        ActualizarRolUsuario(infoUsuario.NombreRolAntiguo.Trim(), infoUsuario.NombreRol.Trim(), infoUsuario.NickUsuario.Trim());
                    }
                    string query = string.Format("ALTER USER {0} with password '{1}';", infoUsuario.NickUsuario.Trim(), pwdUsuario.Trim());
                    using (var cmd = new NpgsqlCommand(query, conn_BD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    conn_BD.Close();
                    msjUsuarios.OperacionExitosa = true;
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para actualizar el rol de un usuario en la base de datos BD.
        /// </summary>
        /// <param name="nombreRolAnterior"></param>
        /// <param name="nombreRolNuevo"></param>
        /// <param name="nickUsuario"></param>
        public void ActualizarRolUsuario(string nombreRolAnterior,string nombreRolNuevo,string nickUsuario)
        {
            string queryAdd = "ALTER GROUP {0} ADD USER {1};";
            string queryRemv = "ALTER GROUP {0} DROP USER {1};";
            using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format(queryAdd, nombreRolNuevo, nickUsuario), conn_BD))
            {
                cmd.ExecuteNonQuery();
            }
            using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format(queryRemv, nombreRolAnterior, nickUsuario), conn_BD))
            {
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Método para actualizar el perfil de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionPerfilUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set nombres_usuario = @nu,correo_usuario = @cu,telefono_usuario = @tu,telefonocelular_usuario = @tcu,direccion_usuario = @du where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario.Trim();
                    cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario.Trim();
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario.Trim() : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario.Trim() : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario.Trim() : DBNull.Value;
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                ConsultasUsuarios objConsultaUsuarios = new ConsultasUsuarios();
                if (objConsultaUsuarios.ObtenerUsuarioPorNick(infoUsuario.NickUsuario).ObjetoInventarios == null)
                {
                    ConsultasUsuarios objConsultaUsuariosBD = new ConsultasUsuarios();
                    Usuarios infoUsuarioBD = objConsultaUsuariosBD.ObtenerUsuarioPorId(infoUsuario.IdUsuario).ObjetoInventarios;
                    string nickAnterior = infoUsuarioBD.NickUsuario.Trim();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set nick_usuario = @niu where id_usuario = @iu", conn_BD))
                    {
                        cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario.Trim();
                        cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    conn_BD.Close();
                    NpgsqlConnection connBD = new NpgsqlConnection("Server='192.168.0.9';Port=5432;User Id=postgres;Password=postgres;Database=DCICC_BDInventario; CommandTimeout=3020;");
                    connBD.Open();
                    NpgsqlTransaction tranBD = connBD.BeginTransaction();
                    string queryUser = string.Format("ALTER USER {0} RENAME TO {1};", nickAnterior.Trim(), infoUsuario.NickUsuario.Trim());
                    using (var cmd = new NpgsqlCommand(queryUser, connBD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string queryPwd = string.Format("ALTER USER {0} with password '{1}';", infoUsuario.NickUsuario.Trim(), ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario.Trim()));
                    using (var cmd = new NpgsqlCommand(queryPwd, connBD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    tranBD.Commit();
                    connBD.Close();
                }
                else
                {
                    tran.Commit();
                    conn_BD.Close();
                }
                msjUsuarios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para actualizar el Nick de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionNickUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ConsultasUsuarios objConsultaUsuarios = new ConsultasUsuarios();
                Usuarios infoUsuarioBD= objConsultaUsuarios.ObtenerUsuarioPorId(infoUsuario.IdUsuario).ObjetoInventarios;
                string nickAnterior = infoUsuarioBD.NickUsuario;
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set nick_usuario = @niu where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario.Trim();
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string queryUser = string.Format("ALTER USER {0} RENAME TO {1};",nickAnterior,infoUsuario.NickUsuario.Trim());
                using (var cmd = new NpgsqlCommand(queryUser, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                string queryPwd = string.Format("ALTER USER {0} with password '{1}';",infoUsuario.NickUsuario,ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario.Trim()));
                using (var cmd = new NpgsqlCommand(queryPwd, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                msjUsuarios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para actualizar el password de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionPasswordUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                string pwdUsuario = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set password_usuario = @pu where id_usuario = @iu", conn_BD))
                {
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pwdUsuario.Trim();
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = string.Format("ALTER USER {0} with password '{1}';" ,infoUsuario.NickUsuario.Trim(), pwdUsuario.Trim());
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
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para actualizar el estado de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizacionEstadoUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_usuarios set habilitado_usuario = @hu where id_usuario = @iu", conn_BD))
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
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
    }
}
