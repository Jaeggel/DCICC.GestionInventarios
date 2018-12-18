using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;

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
        /// Método para ingresar un nuevo Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios RegistroUsuario(Usuarios infoUsuario)
        {
            string pwdUsuario = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {                
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_usuarios (id_rol,nombres_usuario,nick_usuario,password_usuario,correo_usuario,telefono_usuario,telefonocelular_usuario,direccion_usuario,habilitado_usuario) VALUES (@ir,@nu,@niu,@pu,@cu,@tu,@tcu,@du,@hu)", conn_BD))
                {
                    cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdRol;
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario;
                    cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario;
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pwdUsuario;
                    cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario;
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoUsuario.DireccionUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = string.Empty;
                ConsultasRoles objConsultaRoles = new ConsultasRoles();
                infoUsuario.NombreRol = objConsultaRoles.ObtenerRolPorId(infoUsuario.IdRol).ObjetoInventarios.NombreRol;
                if (infoUsuario.NombreRol == "administrador")
                {
                    query= string.Format("create user {0} with password '{1}' LOGIN CREATEROLE CREATEUSER in group {2};", infoUsuario.NickUsuario, pwdUsuario, objConsultaRoles.ObtenerRolPorId(infoUsuario.IdRol).ObjetoInventarios.NombreRol);
                }
                else
                {
                    query = string.Format("create user {0} with password '{1}' NOCREATEROLE NOCREATEUSER in group {2};", infoUsuario.NickUsuario, pwdUsuario, objConsultaRoles.ObtenerRolPorId(infoUsuario.IdRol).ObjetoInventarios.NombreRol);
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = true;
            }
            catch(Exception e)
            {
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
    }
}
