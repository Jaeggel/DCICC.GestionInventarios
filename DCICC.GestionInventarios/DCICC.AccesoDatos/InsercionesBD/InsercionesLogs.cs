﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
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
        public MensajesLogs RegistroLogs(Logs infoLog)
        {
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_logs (id_usuario,fecha_logs,operacion_logs,valoranterior_logs,valoractual_logs,tabla_logs,ip_logs) VALUES (@iu,@fl,@ol,@val,@vacl,@tl,@ipl)", conn_BD))
                {
                    cmd.Parameters.AddWithValue("iu", infoLog.IdUsuario);
                    cmd.Parameters.AddWithValue("fl", infoLog.FechaLogs);
                    cmd.Parameters.AddWithValue("ol", infoLog.OperacionLogs);
                    cmd.Parameters.Add("val", NpgsqlTypes.NpgsqlDbType.Text).Value = !String.IsNullOrEmpty(infoLog.ValorAnteriorLogs) ? (object)infoLog.ValorAnteriorLogs: DBNull.Value;
                    cmd.Parameters.Add("vacl", NpgsqlTypes.NpgsqlDbType.Text).Value = !String.IsNullOrEmpty(infoLog.ValorAnteriorLogs) ? (object)infoLog.ValorActualLogs : DBNull.Value;
                    cmd.Parameters.AddWithValue("tl", infoLog.TablaLogs);
                    cmd.Parameters.Add("ipl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoLog.IpLogs) ? (object)infoLog.IpLogs : DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjLogs.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLogs.OperacionExitosa = false;
                msjLogs.MensajeError=e.Message;
            }
            return msjLogs;
        }
    }
}
