using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        /// Método para obtener los tickets de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultatickets o ticketshabilitados</param>
        /// <returns></returns>
        public MensajesTickets ObtenerTickets(string nombreFuncion)
        {
            List<Tickets> lstTickets = new List<Tickets>();
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tickets objTickets = new Tickets
                            {
                                IdTicket = int.Parse(dr[0].ToString().Trim()),
                                IdUsuario= int.Parse(dr[1].ToString().Trim()),
                                IdResponsableUsuario = int.Parse(dr[2].ToString().Trim()),
                                IdLaboratorio = int.Parse(dr[3].ToString().Trim()),
                                IdDetalleActivo = int.Parse(dr[4].ToString().Trim()),
                                EstadoTicket = dr[5].ToString().Trim(),
                                FechaAperturaTicket= DateTime.Parse(dr[6].ToString().Trim()),
                                FechaSolucionTicket= DateTime.Parse(dr[7].ToString().Trim()),
                                PrioridadTicket= dr[8].ToString().Trim(),
                                ComentarioTicket= dr[9].ToString().Trim(),
                                DescripcionTicket = dr[10].ToString().Trim(),
                                HabilitadoTicket = bool.Parse(dr[11].ToString().Trim()),
                                NombreUsuario = dr[12].ToString().Trim(),
                                NombreUsuarioResponsable = dr[13].ToString().Trim(),
                                NombreLaboratorio = dr[14].ToString().Trim(),
                                NombreDetalleActivo = dr[15].ToString().Trim()
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
                msjTickets.OperacionExitosa = false;
                msjTickets.MensajeError = e.Message;
            }
            return msjTickets;
        }
    }
}
