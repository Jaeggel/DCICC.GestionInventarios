using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesTickets
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesTickets()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo Ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        public MensajesTickets RegistroTicket(Tickets infoTicket)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_tickets(id_usuario, idresponsable_usuario, id_laboratorio, id_detalleact, estado_ticket, fechaapertura_ticket, fechasolucion_ticket, prioridad_ticket, comentario_ticket, descripcion_ticket)VALUES (@iu, @iru, @il, @ida, @et, @fat, @fct, @pt, @ct, @dt);", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdUsuario;
                    cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdResponsableUsuario.ToString()) ? (object)infoTicket.IdResponsableUsuario: DBNull.Value; ;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdLaboratorio.ToString()) ? (object)infoTicket.IdLaboratorio.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdDetalleActivo.ToString()) ? (object)infoTicket.IdDetalleActivo.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                    cmd.Parameters.AddWithValue("fat", infoTicket.FechaAperturaTicket);
                    cmd.Parameters.AddWithValue("fct", infoTicket.FechaResueltoTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaResueltoTicket.ToLongDateString()) ? (object)infoTicket.FechaResueltoTicket : DBNull.Value; ;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.PrioridadTicket;
                    cmd.Parameters.Add("ct", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioTicket) ? (object)infoTicket.ComentarioTicket: DBNull.Value; ;
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.DescripcionTicket) ? (object)infoTicket.DescripcionTicket : DBNull.Value; ;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjTickets.OperacionExitosa = true;
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
