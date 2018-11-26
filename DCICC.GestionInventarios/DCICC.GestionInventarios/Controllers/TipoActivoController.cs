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
    public class TipoActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoActivo()
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
        /// Método (GET) para mostrar la vista ModificarTipoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoActivo()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoTipoActivo.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoTipoActivo(TipoActivo infoTipoActivo)
        {
            string mensajesTipoActivo = string.Empty;
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
                msjTipoActivo = objTipoActivoAccDatos.RegistrarTipoActivo(infoTipoActivo);
                if (msjTipoActivo.OperacionExitosa)
                {
                    mensajesTipoActivo = "El tipo de activo ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesTipoActivo;
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = "No se ha podido registrar el tipo de activo: " + msjTipoActivo.MensajeError;
                    TempData["MensajeError"] = mensajesTipoActivo;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoActivo + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarTipoActivo", "TipoActivo");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarTipoActivo.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarTipoActivo(TipoActivo infoTipoActivo)
        {
            string mensajesTipoActivo = string.Empty;
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
                msjTipoActivo = objTipoActivoAccDatos.ActualizarTipoActivo(infoTipoActivo,false);
                if (msjTipoActivo.OperacionExitosa)
                {
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = "No se ha podido actualizar el tipo de activo: " + msjTipoActivo.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoActivo + ": " + e.Message);
            }
            return RedirectToAction("ModificarTipoActivo", "TipoActivo");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarTipoActivo.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoTipoActivo(TipoActivo infoTipoActivo)
        {
            string mensajesTipoActivo = string.Empty;
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
                msjTipoActivo = objTipoActivoAccDatos.ActualizarTipoActivo(infoTipoActivo, true);
                if (msjTipoActivo.OperacionExitosa)
                {
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = "No se ha podido actualizar el tipo de activo: " + msjTipoActivo.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoActivo + ": " + e.Message);
            }
            return RedirectToAction("ModificarTipoActivo", "TipoActivo");
        }
        /// <summary>
        /// Método para obtener los tipos de activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoActivoComp()
        {
            TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoActivoAccDatos.ObtenerTipoActivo("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los tipos de activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoActivoHab()
        {
            TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoActivoAccDatos.ObtenerTipoActivo("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}