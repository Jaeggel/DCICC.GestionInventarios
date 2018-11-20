using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método para actualizar un tipo de accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio ActualizacionTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_tipoAccesorio set nombre_tipoaccesorio = @nta,descripcion_tipoaccesorio=@dta,habilitado_tipoaccesorio = @hta where id_tipoaccesorio = @ita", conn_BD))
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
                msjTipoAccesorio.OperacionExitosa = false;
                msjTipoAccesorio.MensajeError = e.Message;
            }
            return msjTipoAccesorio;
        }
    }
}
