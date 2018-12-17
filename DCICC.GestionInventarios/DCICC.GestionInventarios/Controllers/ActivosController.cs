using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using DCICC.GestionInventarios.QR;
using DCICC.GestionInventarios.Reportes;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class ActivosController : Controller
    {
        readonly string path_JsonResponsable = System.Web.Hosting.HostingEnvironment.MapPath("~/Json/Responsable.json");
        static string Id_CQR = string.Empty;
        static string Nombre_Activo = string.Empty;
        static string Tipo_CQR = string.Empty;
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
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
        /// Método (GET) para mostrar la vista ConsultaCQR
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultaCQR()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoActivo.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoActivo(Activos infoActivo)
        {
            Id_CQR = string.Empty;
            Nombre_Activo = string.Empty;
            Tipo_CQR = string.Empty;
            string mensajesActivos = string.Empty;
            MensajesActivos msjActivos = new MensajesActivos();
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            try
            {
                if(objActivosAccDatos.ObtenerActivos("Nombres").ListaObjetoInventarios.Find(x => x.NombreActivo.Trim().ToLower() == infoActivo.NombreActivo.Trim().ToLower())==null)
                {
                    Tipo_CQR = "ACT";
                    MensajesCQR msjCQR = NuevoCQR(Tipo_CQR);
                    if (msjCQR.OperacionExitosa)
                    {
                        infoActivo.IdCQR = msjCQR.ObjetoInventarios.IdCqr;
                        msjActivos = objActivosAccDatos.RegistrarActivo(infoActivo);                        
                        if (msjActivos.OperacionExitosa)
                        {
                            SetIdCQR(infoActivo.IdCQR);
                            SetNombreActivo(infoActivo.NombreActivo);
                            mensajesActivos = string.Format("El activo \"{0}\" ha sido registrado exitosamente.",infoActivo.NombreActivo);
                            TempData["Mensaje"] = mensajesActivos;
                            Logs.Info(mensajesActivos);
                        }
                        else
                        {
                            mensajesActivos = string.Format("No se ha podido registrar el activo \"{0}\": {1}",infoActivo.NombreActivo,msjActivos.MensajeError);
                            TempData["MensajeError"] = mensajesActivos;
                            Logs.Error(mensajesActivos);
                        }
                    }
                }
                else
                {
                    msjActivos.OperacionExitosa = false;
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesActivos, e.Message));
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(msjActivos, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoActivo.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoAccesorio(Accesorios infoAccesorios)
        {
            Id_CQR = string.Empty;
            Nombre_Activo = string.Empty;
            Tipo_CQR= string.Empty;
            string mensajesAccesorios = string.Empty;
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            try
            {
                if (objAccesoriosAccDatos.ObtenerAccesorios("Nombres").ListaObjetoInventarios.Find(x => x.NombreAccesorio.Trim().ToLower() == infoAccesorios.NombreAccesorio.Trim().ToLower()) == null)
                {
                    Tipo_CQR = "ACC";
                    MensajesCQR msjCQR = NuevoCQR(Tipo_CQR);
                    if (msjCQR.OperacionExitosa)
                    {
                        infoAccesorios.IdCQR = msjCQR.ObjetoInventarios.IdCqr;
                        msjAccesorios = objAccesoriosAccDatos.RegistrarAccesorios(infoAccesorios);
                        if (msjAccesorios.OperacionExitosa)
                        {
                            SetIdCQR(infoAccesorios.IdCQR);
                            SetNombreActivo(infoAccesorios.NombreAccesorio);
                            mensajesAccesorios = string.Format("El accesorio \"{0}\" ha sido registrado exitosamente.",infoAccesorios.NombreAccesorio);
                            Logs.Info(mensajesAccesorios);
                        }
                        else
                        {
                            mensajesAccesorios = string.Format("No se ha podido registrar el accesorio \"{0}\": {1}",infoAccesorios.NombreAccesorio,msjAccesorios.MensajeError);
                            Logs.Error(mensajesAccesorios);
                        }
                    }
                }
                else
                {
                    msjAccesorios.OperacionExitosa = false;
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesAccesorios, e.Message));
            }
            return Json(msjAccesorios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para registrar el QR del nuevo Activo en la base de datos .
        /// </summary>
        /// <returns></returns>
        public MensajesCQR NuevoCQR(string tipoQR)
        {
            GeneracionCQR objGeneracionQR = new GeneracionCQR(tipoQR);
            string IdCQR = objGeneracionQR.GenerarIdCodigoQR((string)Session["NickUsuario"]);
            var bitmap = objGeneracionQR.GenerarCodigoQR(IdCQR);
            var bitmapBytes = objGeneracionQR.GenQRBytes(bitmap);
            CQR infoCQR = new CQR
            {
                IdCqr = IdCQR,
                Impreso = false
            };
            string mensajesCQR = string.Empty;
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                ActivosAccDatos objCQRAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                msjCQR = objCQRAccDatos.RegistrarCQR(infoCQR);
                if (msjCQR.OperacionExitosa)
                {
                    mensajesCQR = string.Format("El CQR \"{0}\" ha sido registrado exitosamente.",infoCQR.IdCqr);
                    msjCQR.ObjetoInventarios = infoCQR;
                    Logs.Info(mensajesCQR);
                }
                else
                {
                    mensajesCQR = string.Format("No se ha podido registrar el CQR \"{0}\": {1}",infoCQR.IdCqr,msjCQR.MensajeError);
                    Logs.Error(mensajesCQR);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesCQR, e.Message));
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para crear un nuevo Propósito o el archivo en caso de no existir
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public  JsonResult ModificarResponsableJSON(string nombreResponsable)
        {
            Activos objActivos = new Activos();
            try
            {
                
                objActivos.ResponsableActivo = nombreResponsable;
                string dataJson = JsonConvert.SerializeObject(objActivos);
                System.IO.File.WriteAllText(@path_JsonResponsable, dataJson);
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("No se pudo registrar el Responsable {0} en el JSON: {1}",nombreResponsable,e.Message));
                return null;
            }
            return Json(objActivos.ResponsableActivo, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Actualizaciones (POST)
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
                    mensajesActivos = string.Format("El activo con ID: {0} ha sido modificado correctamente.", infoActivo.IdActivo);
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = string.Format("No se ha podido actualizar el activo con ID: {0}: {1}",infoActivo.IdActivo,msjActivos.MensajeError);
                    Logs.Error(mensajesActivos);
                }
                if(infoActivo.EstadoActivo=="DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if(msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info(string.Format("Historico de activo con con ID: {0} registrado exitosamente.",infoHistActivo.IdActivo));
                    }
                    else
                    {
                        Logs.Error(string.Format("No se ha podido actualizar el historico de activo con ID: {0}: {1}", infoHistActivo.IdActivo, msjHistActivos.MensajeError));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesActivos, e.Message));
            }
            return Json(msjActivos, JsonRequestBehavior.AllowGet);
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
                    mensajesActivos = string.Format("El activo con ID: {0} ha sido modificado correctamente.",infoActivo.IdActivo);
                    Logs.Info(mensajesActivos);
                }
                else
                {
                    mensajesActivos = string.Format("No se ha podido actualizar el activo con ID: {0}: {1}",infoActivo.IdActivo,msjActivos.MensajeError);
                    Logs.Error(mensajesActivos);
                }
                if (infoActivo.EstadoActivo == "DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if (msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info(string.Format("Historico de activo con ID: {0} registrado exitosamente.",infoHistActivo.IdActivo));
                    }
                    else
                    {
                        Logs.Error(string.Format("No se ha podido actualizar el historico de activo con ID: {0}: {1}", infoHistActivo.IdActivo,msjHistActivos.MensajeError));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesActivos, e.Message));
            }
            return Json(msjActivos, JsonRequestBehavior.AllowGet);
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
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
                msjAccesorios = objAccesoriosAccDatos.ActualizarAccesorios(infoAccesorios,false);
                if (msjAccesorios.OperacionExitosa)
                {
                    mensajesAccesorios = string.Format("El accesorio con ID: {0} ha sido modificado correctamente.",infoAccesorios.NombreAccesorio);
                    Logs.Info(mensajesAccesorios);
                }
                else
                {
                    mensajesAccesorios = string.Format("No se ha podido actualizar el accesorio con ID: {0}: {1}", infoAccesorios.NombreAccesorio,msjAccesorios.MensajeError);
                    Logs.Error(mensajesAccesorios);
                }
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                if (infoAccesorios.EstadoAccesorio == "DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdAccesorio = infoAccesorios.IdAccesorio,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if (msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info(string.Format("Historico de activo con ID: {0} registrado exitosamente.", infoHistActivo.IdActivo));
                    }
                    else
                    {
                        Logs.Error(string.Format("No se ha podido actualizar el historico de activo con ID: {0}: {1}", infoHistActivo.IdActivo, msjHistActivos.MensajeError));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesAccesorios, e.Message));
            }
            return Json(msjAccesorios, JsonRequestBehavior.AllowGet);
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
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
                msjAccesorios = objAccesoriosAccDatos.ActualizarAccesorios(infoAccesorios,true);
                if (msjAccesorios.OperacionExitosa)
                {
                    mensajesAccesorios= string.Format("El accesorio con ID: {0} ha sido modificado correctamente.",infoAccesorios.IdAccesorio);
                    Logs.Info(mensajesAccesorios);
                }
                else
                {
                    mensajesAccesorios = string.Format("No se ha podido actualizar el accesorio con ID: {0}: {1}",infoAccesorios.IdAccesorio,msjAccesorios.MensajeError);
                    Logs.Error(mensajesAccesorios);
                }
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                if (infoAccesorios.EstadoAccesorio == "DE BAJA")
                {
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdAccesorio = infoAccesorios.IdAccesorio,
                        FechaModifHistActivos = DateTime.Now
                    };
                    msjHistActivos = objActivosAccDatos.RegistrarHistoricoActivo(infoHistActivo);
                    if (msjHistActivos.OperacionExitosa)
                    {
                        Logs.Info(string.Format("Historico de activo con ID: {0} registrado exitosamente.", infoHistActivo.IdActivo));
                    }
                    else
                    {
                        Logs.Error(string.Format("No se ha podido actualizar el historico de activo con ID: {0}: {1}", infoHistActivo.IdActivo, msjHistActivos.MensajeError));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesAccesorios, e.Message));
            }
            return Json(msjAccesorios, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region PDF e Imagen Código QR
        /// <summary>
        /// Método para llenar la variable global Id_CQR.
        /// </summary>
        /// <param name="idCQR"></param>
        public static void SetIdCQR(string idCQR)
        {
            Id_CQR = idCQR;
        }
        /// <summary>
        /// Método para llenar la variable global Nombre_Activo.
        /// </summary>
        /// <param name="nombreActivo"></param>
        public static void SetNombreActivo(string nombreActivo)
        {
            Nombre_Activo = nombreActivo;
        }
        /// <summary>
        /// Método para mostrar el código QR en la vista
        /// </summary>
        /// <returns></returns>
        public ActionResult ObtenerImagenQR()
        {
            byte[] bytesQR = null;
            try
            {
                GeneracionCQR objGeneracionQR = new GeneracionCQR(Tipo_CQR);
                var bitmapQR = objGeneracionQR.GenerarCodigoQR(Id_CQR);
                bytesQR = objGeneracionQR.GenQRBytes(bitmapQR);
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el bitmap para el código QR: {0}", e.Message));
            }
            return File(bytesQR, "image/jpeg");
        }
        /// <summary>
        /// Método para mostrar el PDF con el código QR del nuevo activo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ObtenerPDFQRSimple()
        {
            ReporteQR objReporteQR = new ReporteQR();
            string mensajesActivos = string.Empty;
            MensajesCQR msjCQR = new MensajesCQR();
            byte[] pdfQR = null;
            try
            {
                pdfQR = objReporteQR.GenerarPDFQRSimple(objReporteQR.GenerarTablaReporteQR(Id_CQR, Nombre_Activo));
                Activos infoActivo = new Activos()
                {
                    IdCQR=Id_CQR,
                    NombreActivo=Nombre_Activo
                };
                ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
                {
                    msjCQR = objActivosAccDatos.ActualizarCQR(infoActivo, null, false);
                    if (msjCQR.OperacionExitosa)
                    {
                        mensajesActivos = string.Format("El CQR con ID: {0} ha sido modificado correctamente.",infoActivo.IdCQR);
                        Logs.Info(mensajesActivos);
                        var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                        {
                            Inline = true,
                            FileName = string.Format("DCICC.CQR.{0}.{1}.{2}",Nombre_Activo, DateTime.Now.ToString("dd-MM-yyyy.hh-mm-ss"), "pdf")
                        };
                        Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
                    }
                    else
                    {
                        mensajesActivos = string.Format("No se ha podido actualizar el CQR con ID: {0}: {1}",infoActivo.IdCQR,msjCQR.MensajeError);
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesActivos, e.Message));
            }
            return File(pdfQR, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener los Accesorios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerAccesoriosComp()
        {
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objAccesoriosAccDatos.ObtenerAccesorios("Comp"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerActivosComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            var jsonResult= Json(objActivosAccDatos.ObtenerActivos("Comp"), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        /// <summary>
        /// Método para obtener los Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerEspAdicionalesComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            var jsonResult = Json(objActivosAccDatos.ObtenerActivos("EspAdi"), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        /// <summary>
        /// Método para obtener los Activos CQR de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerActivosCQR()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("CQR"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Accesorios CQR de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerAccesoriosCQR()
        {
            AccesoriosAccDatos objAccesoriosAccDatos = new AccesoriosAccDatos((string)Session["NickUsuario"]);
            return Json(objAccesoriosAccDatos.ObtenerAccesorios("CQR"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los nombres de los Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerNombresActivos()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerActivos("Nombres"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Históricos de Activos de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerHistoricoActivosComp()
        {
            ActivosAccDatos objActivosAccDatos = new ActivosAccDatos((string)Session["NickUsuario"]);
            return Json(objActivosAccDatos.ObtenerHistoricoActivos(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener el responsable actual de los activos de TI del JSON
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerResponsableActual()
        {
            Activos objActivos=new Activos();
            using (StreamReader r = new StreamReader(path_JsonResponsable))
            {
                string json = r.ReadToEnd();
                objActivos = JsonConvert.DeserializeObject<Activos>(json);
            }
            return Json(objActivos.ResponsableActivo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener el responsable actual de los activos de TI del JSON
        /// </summary>
        /// <returns></returns>
        public Activos ObtenerResponsableActualObj()
        {
            Activos objActivos = new Activos();
            using (StreamReader r = new StreamReader(path_JsonResponsable))
            {
                string json = r.ReadToEnd();
                objActivos = JsonConvert.DeserializeObject<Activos>(json);
            }
            return objActivos;
        }
        #endregion
    }
}