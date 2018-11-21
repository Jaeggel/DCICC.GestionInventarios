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
    [OutputCache(NoStore = true, Duration = 0)]
    public class ActivosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoActivo()
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
        /// Método (GET) para mostrar la vista ConsultaActivos
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultaActivos()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoActivo.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoActivo(Activos infoActivo)
        {
            string mensajesActivos = string.Empty;
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos(Session["userInfo"].ToString());
                msjActivos = objActivosAccDatos.RegistrarActivo(infoActivo);
                if (msjActivos.OperacionExitosa)
                {
                    mensajesActivos = "El activo ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesActivos;
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = "No se ha podido registrar el activo: " + msjActivos.MensajeError;
                    TempData["MensajeError"] = mensajesActivos;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesActivos + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ConsultaActivos", "Activos");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ConsultaActivos.
        /// </summary>
        /// <param name="infoActivos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarActivo(Activos infoActivo)
        {
            string mensajesActivos = string.Empty;
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos(Session["userInfo"].ToString());
                msjActivos = objActivosAccDatos.ActualizarActivo(infoActivo);
                if (msjActivos.OperacionExitosa)
                {
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = "No se ha podido actualizar el activo: " + msjActivos.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesActivos + ": " + e.Message);
            }
            return RedirectToAction("ConsultaActivos", "Activos");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoCQR.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoCQR(CQR infoCQR)
        {
            string mensajesCQR = string.Empty;
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                ActivosAccDatos objCQRAccDatos = new ActivosAccDatos(Session["userInfo"].ToString());
                msjCQR = objCQRAccDatos.RegistrarCQR(infoCQR);
                if (msjCQR.OperacionExitosa)
                {
                    mensajesCQR = "El CQR ha sido registrado exitosamente.";
                    Logs.Info(mensajesCQR);
                }
                else
                {
                    mensajesCQR = "No se ha podido registrar el CQR: " + msjCQR.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCQR + ": " + e.Message);
            }
            return RedirectToAction("NuevoActivo", "Activos");//Revisar redireccionamiento
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoActivo.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoAccesorio(Accesorios infoAccesorios)
        {
            string mensajesAccesorios = string.Empty;
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos(Session["userInfo"].ToString());
                msjAccesorios = objAccesoriosAccDatos.RegistrarAccesorios(infoAccesorios);
                if (msjAccesorios.OperacionExitosa)
                {
                    mensajesAccesorios = "El accesorio ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesAccesorios;
                    Logs.Info(mensajesAccesorios);
                }
                else
                {
                    mensajesAccesorios = "No se ha podido registrar el accesorio: " + msjAccesorios.MensajeError;
                    TempData["MensajeError"] = mensajesAccesorios;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesAccesorios + ": " + e.Message);
            }
            return RedirectToAction("NuevoActivo", "Activos");//Revisar redireccionamiento
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarActivo.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarAccesorio(Accesorios infoAccesorios)
        {
            string mensajesAccesorios = string.Empty;
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos(Session["userInfo"].ToString());
                msjAccesorios = objAccesoriosAccDatos.ActualizarAccesorios(infoAccesorios);
                if (msjAccesorios.OperacionExitosa)
                {
                    Logs.Info(mensajesAccesorios);
                }
                else
                {
                    mensajesAccesorios = "No se ha podido actualizar el accesorio: " + msjAccesorios.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesAccesorios + ": " + e.Message);
            }
            return View();
        }
        /// <summary>
        /// Método para obtener los accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerAccesoriosComp()
        {
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos(Session["userInfo"].ToString());
            return Json(objAccesoriosAccDatos.ObtenerAccesorios("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerActivosComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos(Session["userInfo"].ToString());
            return Json(objActivosAccDatos.ObtenerActivos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}