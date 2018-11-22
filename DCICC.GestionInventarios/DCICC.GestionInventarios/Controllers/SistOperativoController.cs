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
    public class SistOperativoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase SistOperativosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoSistOperativo()
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
        /// Método (GET) para mostrar la vista ModificarSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarSistOperativo()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ObtenerSistOperativosComp();
                return View();
            }
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoSistOperativo.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoSistOperativo(SistOperativos infoSistOperativo)
        {
            string mensajesSistOperativos = string.Empty;
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                SistOperativosAccDatos objSistOperativosAccDatos = new SistOperativosAccDatos(Session["userInfo"].ToString());
                msjSistOperativos = objSistOperativosAccDatos.RegistrarSistOperativo(infoSistOperativo);
                if (msjSistOperativos.OperacionExitosa)
                {
                    mensajesSistOperativos = "El sistema operativo ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesSistOperativos;
                    Logs.Info(mensajesSistOperativos);
                }
                else
                {
                    mensajesSistOperativos = "No se ha podido registrar el sistema operativo: " + msjSistOperativos.MensajeError;
                    TempData["MensajeError"] = mensajesSistOperativos;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesSistOperativos + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarSistOperativo", "SistOperativo");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarSistOperativo.
        /// </summary>
        /// <param name="infoSistOperativos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarSistOperativo(SistOperativos infoSistOperativo)
        {
            string mensajesSistOperativos = string.Empty;
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                SistOperativosAccDatos objSistOperativosAccDatos = new SistOperativosAccDatos(Session["userInfo"].ToString());
                msjSistOperativos = objSistOperativosAccDatos.ActualizarSistOperativo(infoSistOperativo);
                if (msjSistOperativos.OperacionExitosa)
                {
                    Logs.Info(mensajesSistOperativos);
                }
                else
                {
                    mensajesSistOperativos = "No se ha podido actualizar el sistema operativo: " + msjSistOperativos.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesSistOperativos + ": " + e.Message);
            }
            return View();
        }
        /// <summary>
        /// Método para obtener los sistemas operativos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerSistOperativosComp()
        {
            SistOperativosAccDatos objSistOperativosAccDatos = new SistOperativosAccDatos(Session["userInfo"].ToString());
            return Json(objSistOperativosAccDatos.ObtenerSistOperativos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los sistemas operativos habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerSistOperativosHab()
        {
            SistOperativosAccDatos objSistOperativosAccDatos = new SistOperativosAccDatos(Session["userInfo"].ToString());
            return Json(objSistOperativosAccDatos.ObtenerSistOperativos("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}