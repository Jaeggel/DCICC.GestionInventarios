using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesTipoActivo
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesTipoActivo()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo ActualizacionTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_tipoactivos SET id_categoriaact=@ic, nombre_tipoact=@nt, descripcion_tipoact=@dt, vidautil_tipoact=@vua, habilitado_tipoact=@ht WHERE id_tipoact=@it", conn_BD))
                {
                    cmd.Parameters.Add("ic", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.IdCategoriaActivo;
                    cmd.Parameters.Add("nt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTipoActivo.NombreTipoActivo;
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTipoActivo.DescripcionTipoActivo) ? (object)infoTipoActivo.DescripcionTipoActivo : DBNull.Value;
                    cmd.Parameters.Add("vua", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.VidaUtilTipoActivo;
                    cmd.Parameters.Add("ht", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoActivo.HabilitadoTipoActivo;
                    cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.IdTipoActivo;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjTipoActivo.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjTipoActivo.OperacionExitosa = false;
                msjTipoActivo.MensajeError = e.Message;
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método para actualizar el estado de un Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo ActualizacionEstadoTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_tipoactivos SET habilitado_tipoact=@ht WHERE id_tipoact=@it", conn_BD))
                {
                    cmd.Parameters.Add("ht", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoActivo.HabilitadoTipoActivo;
                    cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.IdTipoActivo;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjTipoActivo.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjTipoActivo.OperacionExitosa = false;
                msjTipoActivo.MensajeError = e.Message;
            }
            return msjTipoActivo;
        }
    }
}
