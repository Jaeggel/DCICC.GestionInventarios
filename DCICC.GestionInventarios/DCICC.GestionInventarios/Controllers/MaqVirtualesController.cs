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
    public class MaqVirtualesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase MaqVirtualesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMaqVirtual()
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
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMaqVirtual()
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
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.RegistrarMaqVirtual(infoMaqVirtual);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    mensajesMaqVirtuales = "La máquina virtual ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesMaqVirtuales;
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido registrar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                    TempData["MensajeError"] = mensajesMaqVirtuales;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.ActualizarMaqVirtual(infoMaqVirtual,false);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido actualizar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
            }
            return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.ActualizarMaqVirtual(infoMaqVirtual, true);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido actualizar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
            }
            return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        /// <summary>
        /// Método para obtener las máquinas virtuales de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerMaqVirtualesComp()
        {
            MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
            return Json(objMaqVirtualesAccDatos.ObtenerMaqVirtuales("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}