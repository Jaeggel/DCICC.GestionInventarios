using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesSistOperativos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesSistOperativos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un sistema operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        public MensajesSistOperativos ActualizacionSistOperativo(SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_sistoperativos set nombre_so = @nso,descripcion_so=@dso,habilitado_so = @hso where id_so = @iso", conn_BD))
                {
                    cmd.Parameters.Add("nso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoSistOperativo.NombreSistOperativos;
                    cmd.Parameters.Add("dso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoSistOperativo.DescripcionSistOperativos) ? (object)infoSistOperativo.DescripcionSistOperativos : DBNull.Value;
                    cmd.Parameters.Add("hso", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoSistOperativo.HabilitadoSistOperativos;
                    cmd.Parameters.Add("iso", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoSistOperativo.IdSistOperativos;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjSistOperativos.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjSistOperativos.OperacionExitosa = false;
                msjSistOperativos.MensajeError = e.Message;
            }
            return msjSistOperativos;
        }
    }
}
