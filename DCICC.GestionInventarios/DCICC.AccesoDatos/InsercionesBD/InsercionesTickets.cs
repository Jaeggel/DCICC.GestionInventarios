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
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_tickets(id_usuario, id_laboratorio, id_detalleact, id_accesorio,estado_ticket, fechaapertura_ticket, descripcion_ticket, prioridad_ticket)VALUES (@iu,@il, @ida,@iac, @et, @fa, @dt, @pt);", conn_BD))
                {
                    cmd.Parameters.Add("iu", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdUsuario;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdLaboratorio == 0 ? DBNull.Value : (object)infoTicket.IdLaboratorio;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdDetalleActivo == 0 ? DBNull.Value : (object)infoTicket.IdDetalleActivo;
                    cmd.Parameters.Add("iac", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTicket.IdAccesorio == 0 ? DBNull.Value : (object)infoTicket.IdAccesorio;
                    cmd.Parameters.Add("et", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "ABIERTO";
                    cmd.Parameters.AddWithValue("fa", DateTime.Now);
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTicket.DescripcionTicket.Trim();
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
