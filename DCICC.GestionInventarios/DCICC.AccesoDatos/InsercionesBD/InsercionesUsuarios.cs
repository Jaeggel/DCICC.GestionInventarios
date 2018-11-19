using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
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
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios RegistroUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {                
                string query = "create user " + infoUsuario.NickUsuario + " with password '" + ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario) + "' LOGIN CREATEROLE CREATEUSER in group " + ConsultasRoles.ObtenerRolPorId(infoUsuario.IdRol).ObjetoInventarios.NombreRol + ";";
                using (var cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new NpgsqlCommand("insert into dcicc_usuarios (id_rol,nombres_usuario,nick_usuario,password_usuario,correo_usuario,telefono_usuario,telefonocelular_usuario,direccion_usuario,habilitado_usuario) VALUES (@ir,@nu,@niu,@pu,@cu,@tu,@tcu,@du,@hu)", conn_BD))
                {
                    cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdRol;
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NombresUsuario;
                    cmd.Parameters.Add("niu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.NickUsuario;
                    cmd.Parameters.Add("pu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario);
                    cmd.Parameters.Add("cu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoUsuario.CorreoUsuario;
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.Add("hu", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoUsuario.HabilitadoUsuario;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjUsuarios.OperacionExitosa = true;
            }
            catch(Exception e)
            {
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
            }
            return msjUsuarios;
        }
    }
}
