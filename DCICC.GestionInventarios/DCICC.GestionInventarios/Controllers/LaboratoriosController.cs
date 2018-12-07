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
    public class LaboratoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LaboratoriosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoLaboratorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoLaboratorio()
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
        /// Método (GET) para mostrar la vista ModificarLaboratorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarLaboratorio()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoLaboratorio.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoLaboratorio(Laboratorios infoLaboratorio)
        {
            string mensajesLaboratorios = string.Empty;
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                LaboratoriosAccDatos objLaboratoriosActivosAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
                msjLaboratorios = objLaboratoriosActivosAccDatos.RegistrarLaboratorio(infoLaboratorio);
                if (msjLaboratorios.OperacionExitosa)
                {
                    mensajesLaboratorios = string.Format("El laboratorio \"{0}\" ha sido registrado exitosamente.",infoLaboratorio.NombreLaboratorio);
                    TempData["Mensaje"] = mensajesLaboratorios;
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = string.Format("No se ha podido registrar el laboratorio \"{0}\": {1}",infoLaboratorio.NombreLaboratorio,msjLaboratorios.MensajeError);
                    TempData["MensajeError"] = mensajesLaboratorios;
                    Logs.Error(mensajesLaboratorios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLaboratorios, e.Message));
                return View();
            }
            return RedirectToAction("ModificarLaboratorio", "Laboratorios");
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarLaboratorio.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarLaboratorio(Laboratorios infoLaboratorio)
        {
            string mensajesLaboratorios = string.Empty;
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                LaboratoriosAccDatos objLaboratoriosAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
                msjLaboratorios = objLaboratoriosAccDatos.ActualizarLaboratorio(infoLaboratorio,false);
                if (msjLaboratorios.OperacionExitosa)
                {
                    mensajesLaboratorios = string.Format("El laboratorio con ID: {0} ha sido modificado correctamente.",infoLaboratorio.IdLaboratorio);
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = string.Format("No se ha podido actualizar el laboratorio con ID: {0}: {1}",infoLaboratorio.IdLaboratorio ,msjLaboratorios.MensajeError);
                    Logs.Error(mensajesLaboratorios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLaboratorios, e.Message));
            }
            return Json(msjLaboratorios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarLaboratorio.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoLaboratorio(Laboratorios infoLaboratorio)
        {
            string mensajesLaboratorios = string.Empty;
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                LaboratoriosAccDatos objLaboratoriosAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
                msjLaboratorios = objLaboratoriosAccDatos.ActualizarLaboratorio(infoLaboratorio, true);
                if (msjLaboratorios.OperacionExitosa)
                {
                    mensajesLaboratorios = string.Format("El laboratorio con ID: {0} ha sido modificado correctamente.", infoLaboratorio.IdLaboratorio);
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = string.Format("No se ha podido actualizar el laboratorio con ID: {0}: {1}", infoLaboratorio.IdLaboratorio, msjLaboratorios.MensajeError);
                    Logs.Error(mensajesLaboratorios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesLaboratorios, e.Message));
            }
            return Json(msjLaboratorios, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Laboratorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLaboratoriosComp()
        {
            LaboratoriosAccDatos objLaboratoriosActAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objLaboratoriosActAccDatos.ObtenerLaboratorios("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Laboratorios habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLaboratoriosHab()
        {
            LaboratoriosAccDatos objLaboratoriosActAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objLaboratoriosActAccDatos.ObtenerLaboratorios("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}