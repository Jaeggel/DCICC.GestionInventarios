using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class TipoActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        #endregion
        #region Registros (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoTipoActivo.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoTipoActivoPOST(TipoActivo infoTipoActivo)
        {
            string mensajesTipoActivo = string.Empty;
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
                msjTipoActivo = objTipoActivoAccDatos.RegistrarTipoActivo(infoTipoActivo);
                if (msjTipoActivo.OperacionExitosa)
                {
                    mensajesTipoActivo = string.Format("El tipo de activo \"{0}\" ha sido registrada exitosamente.",infoTipoActivo.NombreTipoActivo);
                    TempData["Mensaje"] = mensajesTipoActivo;
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = string.Format("No se ha podido registrar el tipo de activo \"{0}\": {1}",infoTipoActivo.NombreTipoActivo,msjTipoActivo.MensajeError);
                    TempData["MensajeError"] = mensajesTipoActivo;
                    Logs.Error(mensajesTipoActivo);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesTipoActivo, e.Message));
                return View();
            }
            return RedirectToAction("ModificarTipoActivo", "TipoActivo");
        }
        #endregion
        #region Actualizaciones (POST)
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
                    mensajesTipoActivo = string.Format("El tipo de activo con ID: {0} ha sido modificado correctamente.",infoTipoActivo.IdTipoActivo);
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = string.Format("No se ha podido actualizar el tipo de activo con ID: {0}: {1}",infoTipoActivo.IdTipoActivo,msjTipoActivo.MensajeError);
                    Logs.Error(mensajesTipoActivo);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesTipoActivo, e.Message));
            }
            return Json(msjTipoActivo, JsonRequestBehavior.AllowGet);
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
                    mensajesTipoActivo = string.Format("El tipo de activo con ID: {0} ha sido modificado correctamente.", infoTipoActivo.IdTipoActivo);
                    Logs.Info(mensajesTipoActivo);
                }
                else
                {
                    mensajesTipoActivo = string.Format("No se ha podido actualizar el tipo de activo con ID: {0}: {1}", infoTipoActivo.IdTipoActivo, msjTipoActivo.MensajeError);
                    Logs.Error(mensajesTipoActivo);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesTipoActivo, e.Message));
            }
            return Json(msjTipoActivo, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Tipos de Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoActivo()
        {
            TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoActivoAccDatos.ObtenerTipoActivo(""), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener todos los Tipos de Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoActivoComp()
        {
            TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoActivoAccDatos.ObtenerTipoActivo("Comp"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Tipos de Activos habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoActivoHab()
        {
            TipoActivoAccDatos objTipoActivoAccDatos = new TipoActivoAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoActivoAccDatos.ObtenerTipoActivo("Hab"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}