using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasLogs
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasLogs()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesLogs ObtenerLogs()
        {
            List<Logs> lstLogs = new List<Logs>();
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                using (var cmd = new NpgsqlCommand("consultalogs", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Logs objLogs = new Logs
                            {
                                IdLogs= (int)dr[0],
                                IdUsuario= dr[1].ToString().Trim(),
                                FechaLogs = DateTime.Parse(dr[2].ToString().Trim()),
                                OperacionLogs= dr[3].ToString().Trim(),
                                ValorAnteriorLogs= dr[4].ToString().Trim(),
                                ValorActualLogs = dr[5].ToString().Trim(),
                                TablaLogs = dr[6].ToString().Trim(),
                                IpLogs= dr[7].ToString().Trim()
                            };
                            lstLogs.Add(objLogs);
                        }
                        conn_BD.Close();
                        msjLogs.ListaObjetoInventarios = lstLogs;
                        msjLogs.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjLogs.OperacionExitosa = false;
                msjLogs.MensajeError = e.Message;
            }
            return msjLogs;
        }
    }
}
