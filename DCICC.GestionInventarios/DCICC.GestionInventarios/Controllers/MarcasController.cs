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
    public class MarcasController : Controller
    {
        //Instancia para la utilización de LOGS en la clase MarcasController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMarca()
        {
            if (Session["userInfo"] == null)
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
                    mensajesMarcas = "La marca ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesMarcas;
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = "No se ha podido registrar la marca: " + msjMarcas.MensajeError;
                    TempData["MensajeError"] = mensajesMarcas;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMarcas + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarMarca", "Marcas");
        }
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
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = "No se ha podido actualizar la marca: " + msjMarcas.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMarcas + ": " + e.Message);
            }
            return View();
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
                    Logs.Info(mensajesMarcas);
                }
                else
                {
                    mensajesMarcas = "No se ha podido actualizar la marca: " + msjMarcas.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMarcas + ": " + e.Message);
            }
            return View();
        }
        /// <summary>
        /// Método para obtener las marcas de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerMarcasComp()
        {
            MarcasAccDatos objMarcasAccDatos = new MarcasAccDatos((string)Session["NickUsuario"]);
            return Json(objMarcasAccDatos.ObtenerMarcas("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}