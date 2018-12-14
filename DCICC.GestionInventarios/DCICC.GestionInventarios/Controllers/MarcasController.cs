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
    public class MarcasController : Controller
    {
        //Instancia para la utilización de LOGS en la clase MarcasController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMarca()
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
        /// Método (GET) para mostrar la vista ModificarMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMarca()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoMarca.
        /// </summary>
        /// <param name="infoMarcas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaMarca(Marcas infoMarca)
        {
            string mensajesMarcas = string.Empty;
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
                msjMarcas = objMarcasAccDatos.RegistrarMarca(infoMarca);
                if (msjMarcas.OperacionExitosa)
                {
                    mensajesMarcas = string.Format("La marca \"{0}\" ha sido registrada exitosamente.",infoMarca.NombreMarca);
                    TempData["Mensaje"] = mensajesMarcas;
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = string.Format("No se ha podido registrar la marca \"{0}\": {1}",infoMarca.NombreMarca,msjMarcas.MensajeError);
                    TempData["MensajeError"] = mensajesMarcas;
                    Logs.Error(mensajesMarcas);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesMarcas, e.Message));
                return View();
            }
            return RedirectToAction("ModificarMarca", "Marcas");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMarca.
        /// </summary>
        /// <param name="infoMarcas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarMarca(Marcas infoMarca)
        {
            string mensajesMarcas = string.Empty;
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
                msjMarcas = objMarcasAccDatos.ActualizarMarca(infoMarca,false);
                if (msjMarcas.OperacionExitosa)
                {
                    mensajesMarcas = string.Format("La marca con ID: {0} ha sido modificada correctamente.",infoMarca.IdMarca);
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = string.Format("No se ha podido actualizar la marca con ID: {0}: {1}",infoMarca.IdMarca,msjMarcas.MensajeError);
                    Logs.Error(mensajesMarcas);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesMarcas, e.Message));
            }
            return Json(msjMarcas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMarca.
        /// </summary>
        /// <param name="infoMarcas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoMarca(Marcas infoMarca)
        {
            string mensajesMarcas = string.Empty;
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
                msjMarcas = objMarcasAccDatos.ActualizarMarca(infoMarca, true);
                if (msjMarcas.OperacionExitosa)
                {
                    mensajesMarcas = string.Format("La marca con ID: {0} ha sido modificada correctamente.", infoMarca.IdMarca);
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = string.Format("No se ha podido actualizar la marca con ID: {0}: {1}", infoMarca.IdMarca, msjMarcas.MensajeError);
                    Logs.Error(mensajesMarcas);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesMarcas, e.Message));
            }
            return Json(msjMarcas, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todas las Marcas de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerMarcasComp()
        {
            MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
            return Json(objMarcasAccDatos.ObtenerMarcas("Comp"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener las Marcas habilitadas de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerMarcasHab()
        {
            MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
            return Json(objMarcasAccDatos.ObtenerMarcas("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}