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
    public class TipoAccesorioController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoAccesorioController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoAccesorio()
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
        /// Método (GET) para mostrar la vista ModificarTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoAccesorio()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoTipoAccesorio.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            string mensajesTipoAccesorio = string.Empty;
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos((string)Session["NickUsuario"]);
                msjTipoAccesorio = objTipoAccesorioAccDatos.RegistrarTipoAccesorio(infoTipoAccesorio);
                if (msjTipoAccesorio.OperacionExitosa)
                {
                    mensajesTipoAccesorio = "El tipo de accesorio \"" + infoTipoAccesorio.NombreTipoAccesorio + "\" ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesTipoAccesorio;
                    Logs.Info(mensajesTipoAccesorio);
                }
                else
                {
                    mensajesTipoAccesorio = "No se ha podido registrar el tipo de accesorio \"" + infoTipoAccesorio.NombreTipoAccesorio + "\": " + msjTipoAccesorio.MensajeError;
                    TempData["MensajeError"] = mensajesTipoAccesorio;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoAccesorio + ": " + e.Message);
                return RedirectToAction("ModificarTipoAccesorio", "TipoAccesorio");
            }
            return RedirectToAction("ModificarTipoAccesorio", "TipoAccesorio");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarTipoAccesorio.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            string mensajesTipoAccesorio = string.Empty;
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos((string)Session["NickUsuario"]);
                msjTipoAccesorio = objTipoAccesorioAccDatos.ActualizarTipoAccesorio(infoTipoAccesorio,false);
                if (msjTipoAccesorio.OperacionExitosa)
                {
                    mensajesTipoAccesorio = "El tipo de accesorio \"" + infoTipoAccesorio.IdTipoAccesorio + "\" ha sido modificado correctamente.";
                    Logs.Info(mensajesTipoAccesorio);
                }
                else
                {
                    mensajesTipoAccesorio = "No se ha podido actualizar el tipo de accesorio \"" + infoTipoAccesorio.IdTipoAccesorio + "\": " + msjTipoAccesorio.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoAccesorio + ": " + e.Message);
            }
            return Json(msjTipoAccesorio, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarTipoAccesorio.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            string mensajesTipoAccesorio = string.Empty;
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos((string)Session["NickUsuario"]);
                msjTipoAccesorio = objTipoAccesorioAccDatos.ActualizarTipoAccesorio(infoTipoAccesorio,true);
                if (msjTipoAccesorio.OperacionExitosa)
                {
                    mensajesTipoAccesorio = "El tipo de accesorio \"" + infoTipoAccesorio.IdTipoAccesorio + "\" ha sido modificado correctamente.";
                    Logs.Info(mensajesTipoAccesorio);
                }
                else
                {
                    mensajesTipoAccesorio = "No se ha podido actualizar el tipo de accesorio \"" + infoTipoAccesorio.IdTipoAccesorio + "\": " + msjTipoAccesorio.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoAccesorio + ": " + e.Message);
            }
            return Json(msjTipoAccesorio, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Tipos de Accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoAccesorioComp()
        {
            TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoAccesorioAccDatos.ObtenerTipoAccesorio("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Tipos de Accesorios habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoAccesorioHab()
        {
            TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos((string)Session["NickUsuario"]);
            return Json(objTipoAccesorioAccDatos.ObtenerTipoAccesorio("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}