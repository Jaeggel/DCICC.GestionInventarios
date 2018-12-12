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
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_Storage (nombre_Storage,ubicacion_Storage,descripcion_Storage,habilitado_Storage) VALUES (@nl,@ul,@dl,@hl)", conn_BD))
                {
                    //cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.NombreStorage;
                    //cmd.Parameters.Add("ul", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoStorage.UbicacionStorage;
                    //cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoStorage.DescripcionStorage) ? (object)infoStorage.DescripcionStorage : DBNull.Value;
                    //cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoStorage.HabilitadoStorage;
                    cmd.ExecuteNonQuery();
                }
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
