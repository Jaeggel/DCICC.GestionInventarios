﻿using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using DCICC.GestionInventarios.QR;
using DCICC.GestionInventarios.Reportes;
using log4net;
using System;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class ActivosController : Controller
    {
        static string Id_CQR = string.Empty;
        static string Nombre_Activo = string.Empty;
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
                {
                    MensajesCQR msjCQR = NuevoCQR();
                    if (msjCQR.OperacionExitosa)
                    {
                        infoActivo.IdCQR = msjCQR.ObjetoInventarios.IdCqr;
                        ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                        msjActivos = objActivosAccDatos.RegistrarActivo(infoActivo);                        
                        if (msjActivos.OperacionExitosa)
                        {
                            SetIdCQR(infoActivo.IdCQR);
                            SetNombreActivo(infoActivo.NombreActivo);
                            ObtenerImagenQR();
                            ObtenerPDFQRSimple();
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
        /// Método para llenar la variable global Id_CQR.
        /// </summary>
        /// <param name="idCQR"></param>
        public void SetIdCQR(string idCQR)
        {
            Id_CQR =idCQR; 
        }
        /// <summary>
        /// Método para llenar la variable global Nombre_Activo.
        /// </summary>
        /// <param name="nombreActivo"></param>
        public void SetNombreActivo(string nombreActivo)
        {
            Nombre_Activo = nombreActivo;
        }
        /// <summary>
        /// Método para mostrar el código QR en la vista
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerImagenQR()
        {
            GeneracionCQR objGeneracionQR = new GeneracionCQR();
            var bitmap = objGeneracionQR.GenerarCodigoQR(Id_CQR);
            var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
            return File(bitmapBytes, "image/jpeg");            
        }
        /// <summary>
        /// Método para mostrar el PDF con el QR del nuevo activo
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerPDFQRSimple()
        {
            ReporteQR objReporteQR = new ReporteQR();
            var contentDispositionHeader = new System.Net.Mime.ContentDisposition
            {
                Inline = true,
                FileName = "DCICC.CQR."+ Nombre_Activo + DateTime.Now.ToString(".MM-dd-yyyy-mm-ss") + ".pdf"
            };
            Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
            return File(objReporteQR.GenerarPDFQRSimple(Id_CQR,Nombre_Activo), System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarActivo.
        /// </summary>
        /// <param name="infoActivos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarActivo(Activos infoActivo)
        {
            string mensajesActivos = string.Empty;
            MensajesActivos msjActivos = new MensajesActivos();
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                msjActivos = objActivosAccDatos.ActualizarActivo(infoActivo,false);
                if (msjActivos.OperacionExitosa)
                {
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = "No se ha podido actualizar el activo: " + msjActivos.MensajeError;
                }
                if(infoActivo.EstadoActivo=="DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdDetActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if(msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info("Historico de activo registrado exitosamente.");
                    }
                    else
                    {
                        Logs.Error("Historico de activo registrado exitosamente.");
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesActivos + ": " + e.Message);
            }
            return RedirectToAction("ConsultaActivos", "Activos");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarActivo.
        /// </summary>
        /// <param name="infoActivos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoActivo(Activos infoActivo)
        {
            string mensajesActivos = string.Empty;
            MensajesActivos msjActivos = new MensajesActivos();
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                msjActivos = objActivosAccDatos.ActualizarActivo(infoActivo,true);
                if (msjActivos.OperacionExitosa)
                {
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = "No se ha podido actualizar el activo: " + msjActivos.MensajeError;
                }
                if (infoActivo.EstadoActivo == "DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdDetActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if (msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info("Historico de activo registrado exitosamente.");
                    }
                    else
                    {
                        Logs.Error("Historico de activo registrado exitosamente.");
                    }
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
        /// Método (POST) para recibir los datos provenientes de la vista ConsultaActivos.
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
                msjAccesorios = objAccesoriosAccDatos.ActualizarAccesorios(infoAccesorios,false);
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
        /// Método (POST) para recibir los datos provenientes de la vista ConsultaActivos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoAccesorio(Accesorios infoAccesorios)
        {
            string mensajesAccesorios = string.Empty;
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
                msjAccesorios = objAccesoriosAccDatos.ActualizarAccesorios(infoAccesorios,true);
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
        /// Método para registrar el QR del nuevo Activo en la base de datos .
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
        /// Método para obtener los Accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerAccesoriosComp()
        {
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objAccesoriosAccDatos.ObtenerAccesorios("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerActivosComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los nombres de los Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerNombresActivos()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("Nombres").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}