using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Tickets")]
    public class TicketsController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TicketsContTicketler
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Tickets de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsComp")]
        public MensajesTickets ObtenerTicketsComp()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
            msjTickets = objConsultasTicketsBD.ObtenerTickets("ticketsreportados");
            if (msjTickets.OperacionExitosa)
            {
                Logs.Info("Consulta de Tickets realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTickets.MensajeError);
            }
            return msjTickets;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        [HttpPost("RegistrarTicket")]
        public MensajesTickets RegistrarTicket([FromBody] Tickets infoTicket)
        {
            MensajesTickets msjTickets = null;
            InsercionesTickets objInsercionesTicketsBD = new InsercionesTickets();
            msjTickets = objInsercionesTicketsBD.RegistroTicket(infoTicket);
            if (msjTickets.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Ticket de Usuario: {0} realizado exitosamente.",infoTicket.NombreUsuario));
            }
            else
            {
                Logs.Error(msjTickets.MensajeError);
            }
            return msjTickets;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        [HttpPost("ActualizarTicket")]
        public MensajesTickets ActualizarTicket([FromBody] Tickets infoTicket)
        {
            MensajesTickets msjTickets = null;
            ActualizacionesTickets objActualizacionesTicketsBD = new ActualizacionesTickets();
            msjTickets = objActualizacionesTicketsBD.ActualizacionTicket(infoTicket);
            if (msjTickets.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Ticket con ID: {0} realizada exitosamente.",infoTicket.IdTicket));
            }
            else
            {
                Logs.Error(msjTickets.MensajeError);
            }
            return msjTickets;
        }
        #endregion
    }
}