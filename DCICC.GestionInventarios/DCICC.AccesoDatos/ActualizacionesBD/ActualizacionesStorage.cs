using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesStorage
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesStorage()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        public MensajesStorage ActualizacionStorage(Storage infoStorage)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_Storage set nombre_Storage = @nl,descripcion_Storage=@dl,ubicacion_Storage=@ul,habilitado_Storage = @hl where id_Storage = @il", conn_BD))
                {
                    //cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NombreStorage;
                    //cmd.Parameters.Add("ul", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.UbicacionStorage;
                    //cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoStorage.DescripcionStorage) ? (object)infoStorage.DescripcionStorage : DBNull.Value;
                    //cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
                    //cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoStorage.IdStorage;
                    //cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjStorage.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjStorage.OperacionExitosa = false;
                msjStorage.MensajeError = e.Message;
            }
            return msjStorage;
        }
        /// <summary>
        /// Método para actualizar el estado de un Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        public MensajesStorage ActualizacionEstadoStorage(Storage infoStorage)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_Storage set habilitado_Storage = @hl where id_Storage = @il", conn_BD))
                {
                    //cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
                    //cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoStorage.IdStorage;
                    //cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjStorage.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjStorage.OperacionExitosa = false;
                msjStorage.MensajeError = e.Message;
            }
            return msjStorage;
        }
    }
}
