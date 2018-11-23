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
    public class LaboratoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase Usuarios
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                    mensajesLaboratorios = "El laboratorio ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesLaboratorios;
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = "No se ha podido registrar el laboratorio: " + msjLaboratorios.MensajeError;
                    TempData["MensajeError"] = mensajesLaboratorios;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesLaboratorios + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarLaboratorio", "Laboratorios");
        }
        /// <summary>
        /// Método para actualizar un laboratorio en la base de datos.
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
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = "No se ha podido actualizar el laboratorio: " + msjLaboratorios.MensajeError;
                    TempData["MensajeError"] = mensajesLaboratorios;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesLaboratorios + ": " + e.Message);    
            }
            return RedirectToAction("ModificarLaboratorio", "Laboratorios");
        }
        /// <summary>
        /// Método para actualizar el estado de un laboratorio en la base de datos.
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
                    Logs.Info(mensajesLaboratorios);
                }
                else
                {
                    mensajesLaboratorios = "No se ha podido actualizar el laboratorio: " + msjLaboratorios.MensajeError;
                    TempData["MensajeError"] = mensajesLaboratorios;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesLaboratorios + ": " + e.Message);
            }
            return RedirectToAction("ModificarLaboratorio", "Laboratorios");
        }
        /// <summary>
        /// Método para obtener todos los laboratorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerLaboratoriosComp()
        {
            LaboratoriosAccDatos objLaboratoriosActAccDatos = new LaboratoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objLaboratoriosActAccDatos.ObtenerLaboratorios("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}