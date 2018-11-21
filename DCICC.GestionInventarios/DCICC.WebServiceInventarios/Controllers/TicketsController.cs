using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Tickets")]
    public class TicketsController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TicketsContTicketler
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los tickets habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsHab")]
        public MensajesTickets ObtenerTicketsHab()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
                msjTickets = objConsultasTicketsBD.ObtenerTickets("ticketshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los tickets de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsComp")]
        public MensajesTickets ObtenerTicketsComp()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
                msjTickets = objConsultasTicketsBD.ObtenerTickets("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los tickets abiertos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsAbiertos")]
        public MensajesTickets ObtenerTicketsAbiertos()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
                msjTickets = objConsultasTicketsBD.ObtenerTickets("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los tickets en curso de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsEnCurso")]
        public MensajesTickets ObtenerTicketsEnCurso()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
                msjTickets = objConsultasTicketsBD.ObtenerTickets("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los tickets resueltos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTicketsResueltos")]
        public MensajesTickets ObtenerTicketsResueltos()
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
                msjTickets = objConsultasTicketsBD.ObtenerTickets("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        [HttpPost("RegistrarTicket")]
        public MensajesTickets RegistrarTicket([FromBody] Tickets infoTicket)
        {
            MensajesTickets msjTickets = null;
            try
            {
                InsercionesTickets objInsercionesTicketsBD = new InsercionesTickets();
                msjTickets = objInsercionesTicketsBD.RegistroTicket(infoTicket);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el ticket: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método (POST) para actualizar un ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        [HttpPost("ActualizarTicket")]
        public MensajesTickets ActualizarTicket([FromBody] Tickets infoTicket)
        {
            MensajesTickets msjTickets = null;
            try
            {
                ActualizacionesTickets objActualizacionesTicketsBD = new ActualizacionesTickets();
                msjTickets = objActualizacionesTicketsBD.ActualizacionTicket(infoTicket);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el ticket: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
    }
}