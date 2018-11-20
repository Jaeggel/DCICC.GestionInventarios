using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método para actualizar un ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        public MensajesTickets ActualizacionTicket(Tickets infoTicket)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("update dcicc_tickets set id_usuario=@iu,idresponsable_ticket=@iru,id_laboratorio=@il,id_detalleact=@ida,estado_ticket=@et,fechaapertura=@fat,fechasolucion=@fct,prioridad_ticket=@pt,comentario_ticket=@ct,descripcion_ticket=@dt,habilitado_ticket=@ht where id_ticket=@it", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdUsuario;
                    cmd.Parameters.Add("iru", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdResponsableUsuario.ToString()) ? (object)infoTicket.IdResponsableUsuario : DBNull.Value; ;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdLaboratorio.ToString()) ? (object)infoTicket.IdLaboratorio.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoTicket.IdDetalleActivo.ToString()) ? (object)infoTicket.IdDetalleActivo.ToString() : DBNull.Value; ;
                    cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.EstadoTicket;
                    cmd.Parameters.AddWithValue("fat", infoTicket.FechaAperturaTicket);
                    cmd.Parameters.AddWithValue("fct", infoTicket.FechaSolucionTicket).Value = !string.IsNullOrEmpty(infoTicket.FechaSolucionTicket.ToLongDateString()) ? (object)infoTicket.FechaSolucionTicket : DBNull.Value; ;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.PrioridadTicket;
                    cmd.Parameters.Add("ct", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.ComentarioTicket) ? (object)infoTicket.ComentarioTicket : DBNull.Value; ;
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTicket.DescripcionTicket) ? (object)infoTicket.DescripcionTicket : DBNull.Value; ;
                    cmd.Parameters.Add("ht", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTicket.HabilitadoTicket;
                    cmd.Parameters.Add("it", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdTicket;
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
