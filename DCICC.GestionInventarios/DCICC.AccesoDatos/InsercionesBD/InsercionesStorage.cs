using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesStorage
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesStorage()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        public MensajesStorage RegistroStorage(Storage infoStorage)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_storage(nombre_storage, nick_storage, capacidad_storage, descripcion_storage, habilitado_storage)VALUES (@ns, @nis, @cs, @ds, @hs);", conn_BD))
                {
                    cmd.Parameters.Add("ns", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NombreStorage.Trim();
                    cmd.Parameters.Add("nis", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NickStorage.Trim();
                    cmd.Parameters.Add("cs", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.CapacidadStorage;
                    cmd.Parameters.Add("ds", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoStorage.DescripcionStorage) ? (object)infoStorage.DescripcionStorage.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hs", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
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
