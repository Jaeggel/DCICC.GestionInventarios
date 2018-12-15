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
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_storage SET nombre_storage=@ns, nick_storage=@nis, capacidad_storage=@cs,  descripcion_storage=@ds, habilitado_storage=@hs WHERE id_storage=@is;", conn_BD))
                {
                    cmd.Parameters.Add("ns", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NombreStorage;
                    cmd.Parameters.Add("nis", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NickStorage;
                    cmd.Parameters.Add("cs", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.CapacidadStorage;
                    cmd.Parameters.Add("ds", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoStorage.DescripcionStorage) ? (object)infoStorage.DescripcionStorage : DBNull.Value;
                    cmd.Parameters.Add("hs", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
                    cmd.Parameters.Add("is", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoStorage.IdStorage;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjStorage.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
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
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_storage SET habilitado_storage=@hs WHERE id_storage=@is;", conn_BD))
                {
                    cmd.Parameters.Add("hs", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
                    cmd.Parameters.Add("is", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoStorage.IdStorage;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjStorage.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjStorage.OperacionExitosa = false;
                msjStorage.MensajeError = e.Message;
            }
            return msjStorage;
        }
    }
}
