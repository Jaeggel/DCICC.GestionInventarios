using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesSistOperativos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesSistOperativos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nuevo sistema operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        public MensajesSistOperativos RegistroSistOperativo(SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_sistoperativos (nombre_so,descripcion_so,habilitado_so) VALUES (@nso,@dso,@hso)", conn_BD))
                {
                    cmd.Parameters.Add("nso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoSistOperativo.NombreSistOperativos;
                    cmd.Parameters.Add("dso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoSistOperativo.DescripcionSistOperativos) ? (object)infoSistOperativo.DescripcionSistOperativos : DBNull.Value;
                    cmd.Parameters.Add("hso", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoSistOperativo.HabilitadoSistOperativos;
                    cmd.ExecuteNonQuery();
                }
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
