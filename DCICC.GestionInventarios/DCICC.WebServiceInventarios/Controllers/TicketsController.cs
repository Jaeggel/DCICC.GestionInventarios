using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.WebServiceInventarios.Configuration;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        /// <summary>
        /// Método (GET) para obtener una lista de los Tickets generados por un usuario de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerTicketsPorIdUsuario")]
        public MensajesTickets ObtenerTicketsPorIdUsuario([FromBody] int infoIdUsuario)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            ConsultasTickets objConsultasTicketsBD = new ConsultasTickets();
            msjTickets = objConsultasTicketsBD.ObtenerTicketsPorIdUsuario(infoIdUsuario);
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
            DateTime fechaRegistroTicket = DateTime.Now;
            infoTicket.FechaAperturaTicket = fechaRegistroTicket;
            MensajesTickets msjTickets = new MensajesTickets();
            InsercionesTickets objInsercionesTicketsBD = new InsercionesTickets();
            msjTickets = objInsercionesTicketsBD.RegistroTicket(infoTicket);
            if (msjTickets.OperacionExitosa)
            {
                EnviarCorreoNuevoTicket(infoTicket);
                Logs.Info(string.Format("Registro de Ticket de Usuario: {0} realizado exitosamente.", infoTicket.NombreUsuario));
            }
            else
            {
                Logs.Error(msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método para enviar correo a todos los administradores acerca de un nuevo ticket.
        /// </summary>
        /// <param name="infoTicket"></param>
        public void EnviarCorreoNuevoTicket(Tickets infoTicket)
        {
            ConfigSeguridad confServSeguridad = new ConfigSeguridad();
            ConsultasUsuarios objConsultaUsuariosBD = new ConsultasUsuarios();
            string emailEmisor = confServSeguridad.ObtenerEmailEmisor();
            string claveEmailEmisor = confServSeguridad.ObtenerClaveEmailEmisor();
            List<Usuarios> lstUsuariosAdmin = objConsultaUsuariosBD.ObtenerUsuariosAdministradores().ListaObjetoInventarios;
            foreach (var item in lstUsuariosAdmin)
            {
                Mail objMail = new Mail();
                infoTicket.NombreUsuarioResponsable = item.NombresUsuario;
                Correo correo = new Correo
                {
                    Body = objMail.FormatBody(infoTicket),
                    EmailEmisor = emailEmisor,
                    ClaveEmailEmisor = claveEmailEmisor,
                    EmailReceptor = item.CorreoUsuario,
                    Asunto = "Nuevo Ticket para Soporte Técnico"
                };
                objMail.SendMail(correo);
            }
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
            MensajesTickets msjTickets = new MensajesTickets();
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