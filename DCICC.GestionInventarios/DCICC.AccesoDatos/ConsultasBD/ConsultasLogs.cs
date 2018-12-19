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
        public MensajesLogs ObtenerLogs(string nombreFuncion)
        {
            List<Logs> lstLogs = new List<Logs>();
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("consultalogs", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(nombreFuncion=="consultalogs")
                        {
                            while (dr.Read())
                            {
                                Logs objLogs = new Logs
                                {
                                    IdLogs = (int)dr[0],
                                    IdUsuario = dr[1].ToString().Trim(),
                                    FechaLogs = DateTime.Parse(dr[2].ToString().Trim()),
                                    OperacionLogs = dr[3].ToString().Trim(),
                                    ValorAnteriorLogs = dr[4].ToString().Trim(),
                                    ValorActualLogs = dr[5].ToString().Trim(),
                                    TablaLogs = dr[6].ToString().Trim(),
                                    IpLogs = dr[7].ToString().Trim()
                                };
                                lstLogs.Add(objLogs);
                            }
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                Logs objLogs = new Logs
                                {
                                    IdLogs = (int)dr[0],
                                    IdUsuario = dr[1].ToString().Trim(),
                                    FechaLogs = DateTime.Parse(dr[2].ToString().Trim()),
                                    OperacionLogs = dr[3].ToString().Trim(),
                                    TablaLogs = dr[6].ToString().Trim(),
                                    IpLogs = dr[7].ToString().Trim()
                                };
                                lstLogs.Add(objLogs);
                            }
                        }
                        conn_BD.Close();
                        msjLogs.ListaObjetoInventarios = lstLogs;
                        msjLogs.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLogs.OperacionExitosa = false;
                msjLogs.MensajeError = e.Message;
            }
            return msjLogs;
        }
        /// <summary>
        /// Obtener cuantos inicios de sesión se han hecho por usuario.
        /// </summary>
        /// <param name="nickUsuario"></param>
        /// <returns></returns>
        public MensajesLogs ObtenerLogsLoginCount(string nickUsuario)
        {
            long valor = 0;
            MensajesLogs msjRoles = new MensajesLogs();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_logs where id_usuario=@iu and operacion_logs='Login'", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = nickUsuario;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            valor = (long)dr[0];
                        }
                        conn_BD.Close();
                        msjRoles.ValorLong = valor;
                        msjRoles.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjRoles.OperacionExitosa = false;
                msjRoles.MensajeError = e.Message;
            }
            return msjRoles;
        }
    }
}
