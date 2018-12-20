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
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_tickets(id_usuario, id_laboratorio, id_detalleact, estado_ticket, fechaapertura_ticket, descripcion_ticket, prioridad_ticket)VALUES (@iu, @il, @ida, @et, @fat, @dt, @pt);", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdUsuario;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdLaboratorio.ToString()) ? (object)infoTicket.IdLaboratorio.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdDetalleActivo.ToString()) ? (object)infoTicket.IdDetalleActivo.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket.Trim();
                    cmd.Parameters.AddWithValue("fat", infoTicket.FechaAperturaTicket);
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.DescripcionTicket) ? (object)infoTicket.DescripcionTicket.Trim() : DBNull.Value; ;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.PrioridadTicket.Trim();
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjTickets.OperacionExitosa = true;
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
