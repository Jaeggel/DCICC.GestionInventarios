using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasTickets
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasTickets()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Tickets de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultatickets o ticketshabilitados</param>
        /// <returns></returns>
        public MensajesTickets ObtenerTickets(string nombreFuncion)
        {
            List<Tickets> lstTickets = new List<Tickets>();
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tickets objTickets = new Tickets
                            {
                                IdTicket = dr[0] != DBNull.Value ? (int)dr[0] : 0,
                                IdUsuario = dr[1] != DBNull.Value ? (int)dr[1] : 0,
                                IdResponsableUsuario = dr[2] != DBNull.Value ? (int)dr[2] : 0,
                                IdLaboratorio = dr[3] != DBNull.Value ? (int)dr[3] : 0,
                                IdDetalleActivo = dr[4] != DBNull.Value ? (int)dr[4] : 0,
                                IdAccesorio = dr[5] != DBNull.Value ? (int)dr[5] : 0,
                                EstadoTicket = dr[6].ToString().Trim(),
                                FechaAperturaTicket = DateTime.Parse(dr[7].ToString().Trim()),
                                DescripcionTicket = dr[8].ToString().Trim(),
                                ComentarioEnProcesoTicket= dr[9].ToString().Trim(),
                                ComentarioEnEsperaTicket= dr[10].ToString().Trim(),
                                ComentarioResueltoTicket = dr[11].ToString().Trim(),
                                FechaEnProcesoTicket = dr[12] != DBNull.Value ? DateTime.Parse(dr[11].ToString().Trim()) : new DateTime(DateTime.Now.Year, 1, 1),
                                FechaEnEsperaTicket = dr[13] != DBNull.Value ? DateTime.Parse(dr[12].ToString().Trim()) : new DateTime(DateTime.Now.Year, 1, 1),
                                FechaResueltoTicket = dr[14] != DBNull.Value ? DateTime.Parse(dr[13].ToString().Trim()) : new DateTime(DateTime.Now.Year, 1, 1),
                                PrioridadTicket = dr[15].ToString().Trim(),
                                NombreUsuario = dr[16].ToString().Trim(),
                                NombreUsuarioResponsable = dr[17].ToString().Trim(),
                                NombreLaboratorio = dr[18].ToString().Trim(),
                                NombreDetalleActivo = dr[19].ToString().Trim(),
                                NombreAccesorio = dr[20].ToString().Trim(),

                            };
                            lstTickets.Add(objTickets);
                        }
                        conn_BD.Close();
                        msjTickets.ListaObjetoInventarios = lstTickets;
                        msjTickets.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjTickets.OperacionExitosa = false;
                msjTickets.MensajeError = e.Message;
            }
            return msjTickets;
        }
    }
}
