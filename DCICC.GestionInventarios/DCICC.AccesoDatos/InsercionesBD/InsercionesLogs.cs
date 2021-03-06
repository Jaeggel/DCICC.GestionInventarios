﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

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
        /// Método para ingresar un nuevo Log en la base de datos.
        /// </summary>
        /// <param name="infoLog"></param>
        /// <returns></returns>
        public MensajesLogs RegistroLogsInicioBD(Logs infoLog)
        {
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_logs (id_usuario,fecha_logs,operacion_logs,valoranterior_logs,valoractual_logs,tabla_logs,ip_logs) VALUES (@iu,@fl,@ol,@val,@vacl,@tl,@ipl)", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Varchar).Value=infoLog.IdUsuario;
                    cmd.Parameters.AddWithValue("fl", infoLog.FechaLogs);
                    cmd.Parameters.Add("ol", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLog.OperacionLogs.Trim();
                    cmd.Parameters.Add("val", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoLog.ValorAnteriorLogs) ? (object)infoLog.ValorAnteriorLogs.Trim() : DBNull.Value;
                    cmd.Parameters.Add("vacl", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoLog.ValorAnteriorLogs) ? (object)infoLog.ValorActualLogs.Trim() : DBNull.Value;
                    cmd.Parameters.Add("tl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLog.TablaLogs.Trim();
                    cmd.Parameters.Add("ipl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoLog.IpLogs) ? (object)infoLog.IpLogs.Trim() : DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLogs.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLogs.OperacionExitosa = false;
                msjLogs.MensajeError=e.Message;
            }
            return msjLogs;
        }
    }
}
