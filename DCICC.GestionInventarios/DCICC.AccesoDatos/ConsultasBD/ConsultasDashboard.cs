using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasDashboard
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasDashboard()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los parámetros que irán en el dashboard.
        /// </summary>
        /// <param name="nickUsuario"></param>
        /// <returns></returns>
        public MensajesDashboard ObtenerDashboard(string nickUsuario)
        {
            Dashboard objDashboard = new Dashboard();
            MensajesDashboard msjDashboard = new MensajesDashboard();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_logs where id_usuario=@iu and operacion_logs='Login'", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Varchar).Value = nickUsuario;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.SesionCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_detalleactivo where estado_detalleact='OPERATIVO';", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.ActivosOperativosCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_detalleactivo where estado_detalleact='NO OPERATIVO';", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.ActivosNoOperativosCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_detalleactivo where estado_detalleact='DE BAJA';", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.ActivosDeBajaCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_usuarios where habilitado_usuario = true;", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.UsuariosHabilitadosCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_tickets where estado_ticket='ABIERTO'", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.TicketsAbiertosCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_tickets where estado_ticket='EN PROCESO'", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.TicketsEnProcesoCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_tickets where estado_ticket='EN ESPERA'", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.TicketsEnEsperaCont = (long)dr[0];
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from dcicc_tickets where estado_ticket='RESUELTO'", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard.TicketsResueltosCont = (long)dr[0];
                        }
                    }
                }
                conn_BD.Close();
                msjDashboard.ObjetoInventarios = objDashboard;
                msjDashboard.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjDashboard.OperacionExitosa = false;
                msjDashboard.MensajeError = e.Message;
            }
            return msjDashboard;
        }
        /// <summary>
        /// Método para obtener la función activoportipo
        /// </summary>
        /// <returns></returns>
        public MensajesDashboard ObtenerDashboardActivos()
        {
            List<Dashboard> lstDashboard = new List<Dashboard>();
            Dashboard objDashboard = new Dashboard();
            MensajesDashboard msjDashboard = new MensajesDashboard();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("activoportipo", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard = new Dashboard()
                            {
                                NombreTipoActivo = dr[0].ToString().Trim(),
                                TipoActivoCont = (long)dr[1]
                            };
                            lstDashboard.Add(objDashboard);
                        }
                    }
                }
                conn_BD.Close();
                msjDashboard.ListaObjetoInventarios = lstDashboard;
                msjDashboard.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjDashboard.OperacionExitosa = false;
                msjDashboard.MensajeError = e.Message;
            }
            return msjDashboard;
        }
    }
}
