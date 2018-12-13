using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesTickets
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesTickets()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        public MensajesTickets ActualizacionTicket(Tickets infoTicket)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                infoTicket.IdResponsableUsuario = ConsultasUsuarios.ObtenerUsuarioPorNick(infoTicket.NombreUsuarioResponsable).ObjetoInventarios.IdUsuario;
                using (NpgsqlCommand cmd = new NpgsqlCommand("update dcicc_tickets set idresponsable_usuario=@iru,estado_ticket=@et,fechasolucion_ticket=@fct,comentario_ticket=@ct where id_ticket=@it", conn_BD))
                {
                    cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdResponsableUsuario;
                    cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                    cmd.Parameters.AddWithValue("fec", infoTicket.FechaEnProcesoTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaEnProcesoTicket.ToString()) ? (object)infoTicket.FechaEnProcesoTicket : DBNull.Value;
                    cmd.Parameters.AddWithValue("fee", infoTicket.FechaEnEsperaTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaEnEsperaTicket.ToString()) ? (object)infoTicket.FechaEnEsperaTicket : DBNull.Value;
                    cmd.Parameters.AddWithValue("fs", infoTicket.FechaResueltoTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaResueltoTicket.ToString()) ? (object)infoTicket.FechaResueltoTicket : DBNull.Value;
                    cmd.Parameters.Add("ct", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioTicket) ? (object)infoTicket.ComentarioTicket : DBNull.Value;
                    cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdTicket;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
