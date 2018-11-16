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
        /// <returns></returns>
        public MensajesUsuarios RegistroUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ConsultasRoles objConsultasRolesBD = new ConsultasRoles();
                MensajesRoles msjRoles = objConsultasRolesBD.ObtenerRolesHab();
                Roles infoRol = msjRoles.ListaObjetoInventarios.Find(x => x.IdRol == infoUsuario.IdRol);
                if (infoRol != null)
                {
                    using (var cmd = new NpgsqlCommand("create user @user with password '@pass' in group @rol", conn_BD))
                    {
                        cmd.Parameters.AddWithValue("user", infoUsuario.NickUsuario);
                        cmd.Parameters.AddWithValue("pass", ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario));
                        cmd.Parameters.AddWithValue("rol", infoRol.NombreRol);
                        cmd.ExecuteNonQuery();
                    }
                }
                using (var cmd = new NpgsqlCommand("insert into dcicc_usuarios (id_rol,nombres_usuario,nick_usuario,password_usuario,correo_usuario,telefono_usuario,telefonocelular_usuario,direccion_usuario,habilitado_usuario) VALUES (@ir,@nu,@niu,@pu,@cu,@tu,@tcu,@du,@hu)", conn_BD))
                {
                    cmd.Parameters.AddWithValue("ir", infoUsuario.IdRol);
                    cmd.Parameters.AddWithValue("nu", infoUsuario.NombresUsuario);
                    cmd.Parameters.AddWithValue("niu", infoUsuario.NickUsuario);
                    cmd.Parameters.AddWithValue("pu", ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario));
                    cmd.Parameters.AddWithValue("cu", infoUsuario.CorreoUsuario);
                    cmd.Parameters.Add("tu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoUsuario) ? (object)infoUsuario.TelefonoUsuario : DBNull.Value;
                    cmd.Parameters.Add("tcu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.TelefonoCelUsuario : DBNull.Value;
                    cmd.Parameters.Add("du", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoUsuario.TelefonoCelUsuario) ? (object)infoUsuario.DireccionUsuario : DBNull.Value;
                    cmd.Parameters.AddWithValue("hu", infoUsuario.HabilitadoUsuario);
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
