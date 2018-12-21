using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

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
        /// Método para ingresar una nuevo Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        public MensajesSistOperativos RegistroSistOperativo(SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_sistoperativos (nombre_so,descripcion_so,habilitado_so) VALUES (@nso,@dso,@hso)", conn_BD))
                {
                    cmd.Parameters.Add("nso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoSistOperativo.NombreSistOperativos.Trim();
                    cmd.Parameters.Add("dso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoSistOperativo.DescripcionSistOperativos) ? (object)infoSistOperativo.DescripcionSistOperativos.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hso", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoSistOperativo.HabilitadoSistOperativos;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjSistOperativos.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjSistOperativos.OperacionExitosa = false;
                msjSistOperativos.MensajeError = e.Message;
            }
            return msjSistOperativos;
        }
    }
}
