using DCICC.Entidades.EntidadesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesLogs
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesLogs()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo log en la base de datos.
        /// </summary>
        /// <returns></returns>
        public Boolean RegistroLogs(LogsSistema infoLog)
        {
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_logs (id_usuario,fecha_logs,operacion_logs,valoranterior_logs,valoractual_logs,tabla_logs,ip_logs) VALUES (@iu,@fl,@ol,@val,@vacl,@tl,@ipl)", conn_BD))
                {
                    cmd.Parameters.AddWithValue("iu", infoLog.IdUsuario);
                    cmd.Parameters.AddWithValue("fl", infoLog.FechaLogs);
                    cmd.Parameters.AddWithValue("ol", infoLog.OperacionLogs);
                    cmd.Parameters.AddWithValue("val", infoLog.ValorAnteriorLogs);
                    cmd.Parameters.AddWithValue("vacl", infoLog.ValorActualLogs);
                    cmd.Parameters.AddWithValue("tl", infoLog.TablaLogs);
                    cmd.Parameters.AddWithValue("ipl", infoLog.IpLogs);
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
