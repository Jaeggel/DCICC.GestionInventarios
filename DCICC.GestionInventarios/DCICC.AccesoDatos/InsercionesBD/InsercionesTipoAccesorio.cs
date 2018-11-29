using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesTipoAccesorio
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesTipoAccesorio()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio RegistroTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_tipoaccesorio (nombre_tipoaccesorio,descripcion_tipoaccesorio,habilitado_tipoaccesorio) VALUES (@nta,@dta,@hta)", conn_BD))
                {
                    cmd.Parameters.Add("nta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTipoAccesorio.NombreTipoAccesorio;
                    cmd.Parameters.Add("dta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTipoAccesorio.DescripcionTipoAccesorio) ? (object)infoTipoAccesorio.DescripcionTipoAccesorio : DBNull.Value;
                    cmd.Parameters.Add("hta", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoAccesorio.HabilitadoTipoAccesorio;
                    cmd.ExecuteNonQuery();
                }
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
