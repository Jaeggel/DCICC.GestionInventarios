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
                if (infoTicket.EstadoTicket == "EN PROCESO")
                {
                    NpgsqlTransaction tran = conn_BD.BeginTransaction();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("update dcicc_tickets set idresponsable_usuario=@iru,estado_ticket=@et,fecha_encurso_ticket=@fec,comentario_encurso_ticket=@cec where id_ticket=@it", conn_BD))
                    {
                        cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdResponsableUsuario;
                        cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                        cmd.Parameters.AddWithValue("fec", infoTicket.FechaEnProcesoTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaEnProcesoTicket.ToString()) ? (object)infoTicket.FechaEnProcesoTicket : DBNull.Value;
                        cmd.Parameters.Add("cec", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioEnProcesoTicket) ? (object)infoTicket.ComentarioEnProcesoTicket : DBNull.Value;
                        cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdTicket;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                else if (infoTicket.EstadoTicket == "EN ESPERA")
                {
                    NpgsqlTransaction tran = conn_BD.BeginTransaction();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("update dcicc_tickets set idresponsable_usuario=@iru,estado_ticket=@et,fecha_enespera_ticket=@fee,comentario_enespera_ticket=@cee where id_ticket=@it", conn_BD))
                    {
                        cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdResponsableUsuario;
                        cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                        cmd.Parameters.AddWithValue("fee", infoTicket.FechaEnEsperaTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaEnEsperaTicket.ToString()) ? (object)infoTicket.FechaEnEsperaTicket : DBNull.Value;
                        cmd.Parameters.Add("cee", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioEnEsperaTicket) ? (object)infoTicket.ComentarioEnEsperaTicket : DBNull.Value;
                        cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdTicket;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }else if (infoTicket.EstadoTicket == "RESUELTO")
                {
                    NpgsqlTransaction tran = conn_BD.BeginTransaction();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("update dcicc_tickets set idresponsable_usuario=@iru,estado_ticket=@et,fecha_solucion_ticket=@fs,comentario_resuelto_ticket=@cs where id_ticket=@it", conn_BD))
                    {
                        cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdResponsableUsuario;
                        cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                        cmd.Parameters.AddWithValue("fs", infoTicket.FechaResueltoTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaResueltoTicket.ToString()) ? (object)infoTicket.FechaResueltoTicket : DBNull.Value;
                        cmd.Parameters.Add("cs", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioResueltoTicket) ? (object)infoTicket.ComentarioResueltoTicket : DBNull.Value;
                        cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdTicket;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
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
