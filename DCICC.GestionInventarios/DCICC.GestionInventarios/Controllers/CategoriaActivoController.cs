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
    public class CategoriaActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CategoriaActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoCategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoCategoriaActivo()
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
        /// Método (GET) para mostrar la vista ModificarCategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarCategoriaActivo()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoCategoriaActivo.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoCategoriaActivo(CategoriaActivo infoCategoriaActivo)
        {
            string mensajesCategorias = string.Empty;
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                CategoriasActivosAccDatos objCategoriasActivosAccDatos = new CategoriasActivosAccDatos((string)Session["NickUsuario"]);
                msjCategorias = objCategoriasActivosAccDatos.RegistrarCategoriaActivo(infoCategoriaActivo);
                if (msjCategorias.OperacionExitosa)
                {
                    mensajesCategorias = "La categoría ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesCategorias;
                    Logs.Info(mensajesCategorias);
                }
                else
                {
                    mensajesCategorias = "No se ha podido registrar la categoría: " + msjCategorias.MensajeError;
                    TempData["MensajeError"] = mensajesCategorias;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCategorias + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarCategoriaActivo", "CategoriaActivo");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarCategoriaActivo.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarCategoriaActivo(CategoriaActivo infoCategoriaActivo)
        {
            string mensajesCategorias = string.Empty;
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                CategoriasActivosAccDatos objCategoriasAccDatos = new CategoriasActivosAccDatos((string)Session["NickUsuario"]);
                msjCategorias = objCategoriasAccDatos.ActualizarCategoriaActivo(infoCategoriaActivo,false);
                if (msjCategorias.OperacionExitosa)
                {
                    Logs.Info(mensajesCategorias);
                }
                else
                {
                    mensajesCategorias= "No se ha podido actualizar la categoría: " + msjCategorias.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCategorias + ": " + e.Message);
            }
            return RedirectToAction("ModificarCategoriaActivo", "CategoriaActivo");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarCategoriaActivo.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoCategoriaActivo(CategoriaActivo infoCategoriaActivo)
        {
            string mensajesCategorias = string.Empty;
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                CategoriasActivosAccDatos objCategoriasAccDatos = new CategoriasActivosAccDatos((string)Session["NickUsuario"]);
                msjCategorias = objCategoriasAccDatos.ActualizarCategoriaActivo(infoCategoriaActivo, true);
                if (msjCategorias.OperacionExitosa)
                {
                    Logs.Info(mensajesCategorias);
                }
                else
                {
                    mensajesCategorias = "No se ha podido actualizar la categoría: " + msjCategorias.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCategorias + ": " + e.Message);
            }
            return RedirectToAction("ModificarCategoriaActivo", "CategoriaActivo");
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todas las Categorías de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerCategoriasActivosComp()
        {
            CategoriasActivosAccDatos objCategoriasActAccDatos = new CategoriasActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objCategoriasActAccDatos.ObtenerCategoriasActivos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener las Categorías habilitadas de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerCategoriasActivosHab()
        {
            CategoriasActivosAccDatos objCategoriasActAccDatos = new CategoriasActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objCategoriasActAccDatos.ObtenerCategoriasActivos("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}