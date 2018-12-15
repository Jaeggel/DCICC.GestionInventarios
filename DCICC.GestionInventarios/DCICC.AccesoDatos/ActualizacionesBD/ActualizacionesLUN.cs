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
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_lun SET id_storage=@is, nombre_lun=@nl, capacidad_lun=@cl, raid_tp_lun=@rl, descripcion_lun=@dl, habilitado_lun=@hl WHERE id_lun=@il;", conn_BD))
                {
                    cmd.Parameters.Add("is", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdStorage;
                    cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.NombreLUN;
                    cmd.Parameters.Add("cl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.CapacidadLUN;
                    cmd.Parameters.Add("rl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.RaidTPLUN;
                    cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoLUN.DescripcionLUN) ? (object)infoLUN.DescripcionLUN : DBNull.Value;
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdLUN;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
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
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_lun SET habilitado_lun=@hl WHERE id_lun=@il;", conn_BD))
                {
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdLUN;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
    }
}
