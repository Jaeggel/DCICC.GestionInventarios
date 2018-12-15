using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class TicketsController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TicketsController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas ( GET)
        /// <summary>
        /// Método (GET) para mostrar la vista GestionTickets
        /// </summary>
        /// <returns></returns>
        public ActionResult GestionTickets()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista GestionTickets.
        /// </summary>
        /// <param name="infoTickets"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarTicket(Tickets infoTicket)
        {
            if(infoTicket.EstadoTicket=="RESUELTO")
            {
                infoTicket.FechaResueltoTicket = DateTime.Now;
                infoTicket.ComentarioResueltoTicket = infoTicket.ComentarioTicket;
            }
            else if(infoTicket.EstadoTicket == "EN PROCESO")
            {
                infoTicket.FechaEnProcesoTicket = DateTime.Now;
                infoTicket.ComentarioEnProcesoTicket = infoTicket.ComentarioTicket;
            }
            else if (infoTicket.EstadoTicket == "EN ESPERA")
            {
                infoTicket.FechaEnEsperaTicket = DateTime.Now;
                infoTicket.ComentarioEnEsperaTicket = infoTicket.ComentarioTicket;
            }
            string mensajesTickets = string.Empty;
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos((string)Session["NickUsuario"]);
                msjTickets = objTicketsAccDatos.ActualizarTicket(infoTicket);
                if (msjTickets.OperacionExitosa)
                {
                    mensajesTickets = string.Format("El ticket con ID: {0} ha sido modificado correctamente.",infoTicket.IdTicket);
                    Logs.Info(mensajesTickets);
                }
                else
                {
                    mensajesTickets = string.Format("No se ha podido actualizar el ticket con ID: {0}: {1}",infoTicket.IdTicket,msjTickets.MensajeError);
                    Logs.Error(mensajesTickets);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesTickets, e.Message));
            }
            return Json(msjTickets, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Tickets de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTicketsComp()
        {
            TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos((string)Session["NickUsuario"]);
            return Json(objTicketsAccDatos.ObtenerTickets("Comp"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}