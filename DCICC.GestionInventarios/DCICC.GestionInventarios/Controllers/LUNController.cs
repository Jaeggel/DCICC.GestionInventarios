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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LUNController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LUNController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaLUN
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaLUN()
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
        /// Método (GET) para mostrar la vista ModificarLUN
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarLUN()
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
        #region Registros (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoLUN.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaLUN(LUN infoLUN)
        {
            string mensajesLUN = string.Empty;
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                LUNAccDatos objLUNAccDatos = new LUNAccDatos((string)Session["NickUsuario"]);
                msjLUN = objLUNAccDatos.RegistrarLUN(infoLUN);
                if (msjLUN.OperacionExitosa)
                {
                    mensajesLUN = string.Format("La LUN \"{0}\" ha sido registrada exitosamente.", infoLUN.NombreLUN);
                    TempData["Mensaje"] = mensajesLUN;
                    Logs.Info(mensajesLUN);
                }
                else
                {
                    mensajesLUN = string.Format("No se ha podido registrar la LUN \"{0}\": {1}", infoLUN.NombreLUN, msjLUN.MensajeError);
                    TempData["MensajeError"] = mensajesLUN;
                    Logs.Error(mensajesLUN);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLUN, e.Message));
                return View();
            }
            return RedirectToAction("ModificarLUN", "LUN");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarLUN.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarLUN(LUN infoLUN)
        {
            string mensajesLUN = string.Empty;
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                LUNAccDatos objLUNAccDatos = new LUNAccDatos((string)Session["NickUsuario"]);
                msjLUN = objLUNAccDatos.ActualizarLUN(infoLUN, false);
                if (msjLUN.OperacionExitosa)
                {
                    mensajesLUN = string.Format("La LUN con ID: {0} ha sido modificada correctamente.", infoLUN.IdBDLUN);
                    Logs.Info(mensajesLUN);
                }
                else
                {
                    mensajesLUN = string.Format("No se ha podido actualizar la LUN con ID: {0}: {1}", infoLUN.IdBDLUN, msjLUN.MensajeError);
                    Logs.Error(mensajesLUN);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLUN, e.Message));
            }
            return Json(msjLUN, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarLUN.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoLUN(LUN infoLUN)
        {
            string mensajesLUN = string.Empty;
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                LUNAccDatos objLUNAccDatos = new LUNAccDatos((string)Session["NickUsuario"]);
                msjLUN = objLUNAccDatos.ActualizarLUN(infoLUN, true);
                if (msjLUN.OperacionExitosa)
                {
                    mensajesLUN = string.Format("La LUN con ID: {0} ha sido modificada correctamente.", infoLUN.IdBDLUN);
                    Logs.Info(mensajesLUN);
                }
                else
                {
                    mensajesLUN = string.Format("No se ha podido actualizar la LUN con ID: {0}: {1}", infoLUN.IdBDLUN, msjLUN.MensajeError);
                    Logs.Error(mensajesLUN);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLUN, e.Message));
            }
            return Json(msjLUN, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todas las LUN de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLUNComp()
        {
            LUNAccDatos objLUNAccDatos = new LUNAccDatos((string)Session["NickUsuario"]);
            return Json(objLUNAccDatos.ObtenerLUN("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}