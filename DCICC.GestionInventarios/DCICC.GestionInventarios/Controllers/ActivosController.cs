using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using DCICC.GestionInventarios.QR;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    public class ActivosController : Controller
    {
        bool ActivoQRRegistrado = false;
        static string Id_CQR = string.Empty;
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoActivo()
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
        /// Método (GET) para mostrar la vista ConsultaActivos
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultaActivos()
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
                if(!ActivoQRRegistrado)
                {
                    MensajesCQR msjCQR = NuevoCQR();
                    if (msjCQR.OperacionExitosa)
                    {
                        infoActivo.IdCQR = msjCQR.ObjetoInventarios.IdCqr;
                        ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                        msjActivos = objActivosAccDatos.RegistrarActivo(infoActivo);
                        ActivoQRRegistrado = true;
                        if (msjActivos.OperacionExitosa)
                        {
                            mensajesActivos = "El activo ha sido registrado exitosamente.";
                            Id_CQR = infoActivo.IdCQR;
                            ObtenerImagenQR();
                            TempData["Mensaje"] = mensajesActivos;
                            Logs.Info(mensajesActivos);
                        }
                        else
                        {
                            mensajesActivos = "No se ha podido registrar el activo: " + msjActivos.MensajeError;
                            TempData["MensajeError"] = mensajesActivos;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesActivos + ": " + e.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(msjActivos.ObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para mostrar el código QR
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerImagenQR()
        {
            if(Id_CQR=="")
            {
                return null;
            }
            else
            {
                GeneracionCQR objGeneracionQR = new GeneracionCQR();
                var bitmap = objGeneracionQR.GenerarCodigoQR(Id_CQR);//OJO CQR GLOBAL
                Id_CQR = null;
                var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
                ActivoQRRegistrado = false;
                return File(bitmapBytes, "image/jpeg");
            }
            
            //<img src='@Url.Action("ObtenerImagenQR")' />
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
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoActivo.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoAccesorio(Accesorios infoAccesorios)
        {
            string mensajesAccesorios = string.Empty;
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            try
            {
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
            return Json(msjAccesorios.OperacionExitosa, JsonRequestBehavior.AllowGet);
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
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
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
            return RedirectToAction("ConsultaActivos", "Activos");//OJO
        }
        /// <summary>
        /// Método para mostrar el código QR  y registrar el QR en la base de datos en el view
        /// </summary>
        /// <returns></returns>
        public MensajesCQR NuevoCQR()
        {
            GeneracionCQR objGeneracionQR = new GeneracionCQR();
            string IdCQR = objGeneracionQR.GenerarIdCodigoQR((string)Session["NickUsuario"]);
            var bitmap = objGeneracionQR.GenerarCodigoQR(IdCQR);
            var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
            CQR infoCQR = new CQR
            {
                IdCqr = IdCQR,
                Bytea=bitmapBytes
            };
            string mensajesCQR = string.Empty;
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                ActivosAccDatos objCQRAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                msjCQR = objCQRAccDatos.RegistrarCQR(infoCQR);
                if (msjCQR.OperacionExitosa)
                {
                    mensajesCQR = "El CQR ha sido registrado exitosamente.";
                    msjCQR.ObjetoInventarios = infoCQR;
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
            return msjCQR;
        }
        /// <summary>
        /// Método para obtener los accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerAccesoriosComp()
        {
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objAccesoriosAccDatos.ObtenerAccesorios("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerActivosComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los nombres de los activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerNombresActivos()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("Nombres").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}