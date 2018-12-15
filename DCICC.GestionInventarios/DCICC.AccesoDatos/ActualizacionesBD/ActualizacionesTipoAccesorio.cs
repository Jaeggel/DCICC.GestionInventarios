using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesTipoAccesorio
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesTipoAccesorio()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio ActualizacionTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_tipoAccesorio set nombre_tipoaccesorio = @nta,descripcion_tipoaccesorio=@dta,habilitado_tipoaccesorio = @hta where id_tipoaccesorio = @ita", conn_BD))
                {
                    cmd.Parameters.Add("nta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTipoAccesorio.NombreTipoAccesorio;
                    cmd.Parameters.Add("dta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTipoAccesorio.DescripcionTipoAccesorio) ? (object)infoTipoAccesorio.DescripcionTipoAccesorio : DBNull.Value;
                    cmd.Parameters.Add("hta", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoAccesorio.HabilitadoTipoAccesorio;
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoAccesorio.IdTipoAccesorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjTipoAccesorio.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjTipoAccesorio.OperacionExitosa = false;
                msjTipoAccesorio.MensajeError = e.Message;
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método para actualizar el estado de un Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio ActualizacionEstadoTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_tipoAccesorio set habilitado_tipoaccesorio = @hta where id_tipoaccesorio = @ita", conn_BD))
                {
                    cmd.Parameters.Add("hta", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoAccesorio.HabilitadoTipoAccesorio;
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoAccesorio.IdTipoAccesorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjTipoAccesorio.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjTipoAccesorio.OperacionExitosa = false;
                msjTipoAccesorio.MensajeError = e.Message;
            }
            return msjTipoAccesorio;
        }
    }
}
