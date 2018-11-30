using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.EliminacionesBD
{
    public class EliminacionesUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public EliminacionesUsuarios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para eliminar un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios EliminacionUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM public.dcicc_usuarios WHERE id_usuario=@iu;", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                string query = "DROP USER @nu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn_BD))
                {
                    cmd.Parameters.Add("nu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoUsuario.NickUsuario;
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
