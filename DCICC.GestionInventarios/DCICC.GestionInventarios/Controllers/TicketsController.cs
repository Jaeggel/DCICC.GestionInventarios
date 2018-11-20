using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class TicketsController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TicketsController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista GestionTickets
        /// </summary>
        /// <returns></returns>
        public ActionResult GestionTickets()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista GestionTickets.
        /// </summary>
        /// <param name="infoTickets"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarTicket(Tickets infoTicket)
        {
            string mensajesTickets = string.Empty;
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos(Session["userInfo"].ToString());
                msjTickets = objTicketsAccDatos.ActualizarTicket(infoTicket);
                if (msjTickets.OperacionExitosa)
                {
                    Logs.Info(mensajesTickets);
                }
                else
                {
                    mensajesTickets = "No se ha podido actualizar el ticket: " + msjTickets.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTickets + ": " + e.Message);
            }
            return View("GestionTickets");
        }
        /// <summary>
        /// Método para obtener los tickets abiertos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTicketsAbiertos()
        {
            TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos(Session["userInfo"].ToString());
            return Json(objTicketsAccDatos.ObtenerTickets("Abiertos").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los tickets en curso de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTicketsEnCurso()
        {
            TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos(Session["userInfo"].ToString());
            return Json(objTicketsAccDatos.ObtenerTickets("EnCurso").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los tickets resueltos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTicketsResueltos()
        {
            TicketsAccDatos objTicketsAccDatos = new TicketsAccDatos(Session["userInfo"].ToString());
            return Json(objTicketsAccDatos.ObtenerTickets("Resueltos").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}