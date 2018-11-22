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
    public class TipoAccesorioController : Controller
    {
        //Instancia para la utilización de LOGS en la clase Usuarios
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoAccesorio()
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
        /// Método (GET) para mostrar la vista ModificarTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoAccesorio()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ObtenerTipoAccesorioComp();
                return View();
            }
        }
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
                TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos(Session["userInfo"].ToString());
                msjTipoAccesorio = objTipoAccesorioAccDatos.RegistrarTipoAccesorio(infoTipoAccesorio);
                if (msjTipoAccesorio.OperacionExitosa)
                {
                    mensajesTipoAccesorio = "El tipo de accesorio ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesTipoAccesorio;
                    Logs.Info(mensajesTipoAccesorio);
                }
                else
                {
                    mensajesTipoAccesorio = "No se ha podido registrar el tipo de accesorio: " + msjTipoAccesorio.MensajeError;
                    TempData["MensajeError"] = mensajesTipoAccesorio;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoAccesorio + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarTipoAccesorio", "TipoAccesorio");
        }
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
                TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos(Session["userInfo"].ToString());
                msjTipoAccesorio = objTipoAccesorioAccDatos.ActualizarTipoAccesorio(infoTipoAccesorio);
                if (msjTipoAccesorio.OperacionExitosa)
                {
                    Logs.Info(mensajesTipoAccesorio);
                }
                else
                {
                    mensajesTipoAccesorio = "No se ha podido actualizar el tipo de accesorio: " + msjTipoAccesorio.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesTipoAccesorio + ": " + e.Message);
            }
            return View();
        }
        /// <summary>
        /// Método para obtener los tipos de accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerTipoAccesorioComp()
        {
            TipoAccesorioAccDatos objTipoAccesorioAccDatos = new TipoAccesorioAccDatos(Session["userInfo"].ToString());
            return Json(objTipoAccesorioAccDatos.ObtenerTipoAccesorio("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}