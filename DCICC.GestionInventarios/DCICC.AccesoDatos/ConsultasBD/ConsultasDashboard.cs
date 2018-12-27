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
        public MensajesDashboard ObtenerDashboard(string nickUsuario,List<string> sentencias)
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
                List<long> aux = new List<long>();
                foreach (var item in sentencias)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                    {
                        long valor = 0;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                valor = (long)dr[0];
                            }
                        }
                        aux.Add(valor);
                    }
                }
                foreach (var item in aux)
                {
                    objDashboard.ActivosOperativosCont = aux[0];
                    objDashboard.ActivosNoOperativosCont = aux[1];
                    objDashboard.ActivosDeBajaCont = aux[2];
                    objDashboard.UsuariosHabilitadosCont = aux[3];
                    objDashboard.TicketsAbiertosCont = aux[4];
                    objDashboard.TicketsEnProcesoCont = aux[5];
                    objDashboard.TicketsEnEsperaCont = aux[6];
                    objDashboard.TicketsResueltosCont = aux[7];
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
