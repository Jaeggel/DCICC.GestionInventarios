using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesLUN
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesLUN()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar una LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        public MensajesLUN ActualizacionLUN(LUN infoLUN)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_LUN set nombre_LUN = @nm,descripcion_LUN=@dm,habilitado_LUN = @hm where id_LUN = @im", conn_BD))
                {
                    //cmd.Parameters.Add("nm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.NombreLUN;
                    //cmd.Parameters.Add("dm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoLUN.DescripcionLUN) ? (object)infoLUN.DescripcionLUN : DBNull.Value;
                    //cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    //cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdLUN;
                    //cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
        /// <summary>
        /// Método para actualizar el estado de una LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        public MensajesLUN ActualizacionEstadoLUN(LUN infoLUN)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_LUN set habilitado_LUN = @hm where id_LUN = @im", conn_BD))
                {
                    //cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    //cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdLUN;
                    //cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
    }
}
