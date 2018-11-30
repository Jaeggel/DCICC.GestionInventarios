using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
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
    public class MaqVirtualesController : Controller
    {
        readonly string path_JsonPropositos = System.Web.Hosting.HostingEnvironment.MapPath("~/Json/Propositos.json");
        //Instancia para la utilización de LOGS en la clase MaqVirtualesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMaqVirtual()
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
        /// Método (GET) para mostrar la vista ModificarMaqVirtual
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMaqVirtual()
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
        /// Método (GET) para mostrar la vista NuevoProposito
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoProposito()
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
        /// Método para crear un nuevo Propósito o el archivo en caso de no existir
        /// </summary>
        /// <returns></returns>
        public bool NuevoPropositoJSON(Propositos infoProposito)
        {
            infoProposito.NombreProposito = infoProposito.NombreProposito.ToUpper();
            List<Propositos> lstPropositos = new List<Propositos>();
            if (!System.IO.File.Exists(path_JsonPropositos))
            {
                lstPropositos.Add(infoProposito);
                string dataJson = JsonConvert.SerializeObject(lstPropositos);
                System.IO.File.WriteAllText(@path_JsonPropositos, dataJson);
                return true;
            }
            else
            {
                lstPropositos = ObtenerPropositosJSON();
                if (lstPropositos == null)
                {
                    List<Propositos> lstPropositosNew = new List<Propositos> { infoProposito };
                    string dataJson = JsonConvert.SerializeObject(lstPropositosNew);
                    System.IO.File.WriteAllText(@path_JsonPropositos, dataJson);
                    return true;
                }
                else
                {
                    if (lstPropositos.Find(x => x.NombreProposito.ToLower() == infoProposito.NombreProposito.ToLower()) == null)
                    {
                        lstPropositos.Add(infoProposito);
                        string dataJson = JsonConvert.SerializeObject(lstPropositos);
                        System.IO.File.WriteAllText(@path_JsonPropositos, dataJson);
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevaMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.RegistrarMaqVirtual(infoMaqVirtual);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    mensajesMaqVirtuales = "La máquina virtual ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesMaqVirtuales;
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido registrar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                    TempData["MensajeError"] = mensajesMaqVirtuales;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoProposito.
        /// </summary>
        /// <param name="infoProposito"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoProposito(Propositos infoProposito)
        {
            string mensajesMaqVirtuales = string.Empty;
            try
            {
                if (NuevoPropositoJSON(infoProposito))
                {
                    mensajesMaqVirtuales = "El propósito ha sido registrado exitosamente. ";
                    TempData["Mensaje"] = mensajesMaqVirtuales;
                    Logs.Info(mensajesMaqVirtuales);
                }
            }
            catch (Exception e)
            {
                mensajesMaqVirtuales = "No se ha podido registrar el propósito: ";
                Logs.Error(mensajesMaqVirtuales + e.Message);
                TempData["MensajeError"] = mensajesMaqVirtuales + e.Message;
                return RedirectToAction("NuevaMaqVirtual", "MaqVirtuales");
            }
            return RedirectToAction("NuevaMaqVirtual", "MaqVirtuales");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.ActualizarMaqVirtual(infoMaqVirtual,false);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    mensajesMaqVirtuales = "La máquina virtual ha sido modificada correctamente.";
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido actualizar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
            }
            return Json(msjMaqVirtuales, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarMaqVirtual.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            string mensajesMaqVirtuales = string.Empty;
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
                msjMaqVirtuales = objMaqVirtualesAccDatos.ActualizarMaqVirtual(infoMaqVirtual, true);
                if (msjMaqVirtuales.OperacionExitosa)
                {
                    mensajesMaqVirtuales = "La máquina virtual ha sido modificada correctamente.";
                    Logs.Info(mensajesMaqVirtuales);
                }
                else
                {
                    mensajesMaqVirtuales = "No se ha podido actualizar la máquina virtual: " + msjMaqVirtuales.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesMaqVirtuales + ": " + e.Message);
            }
            return Json(msjMaqVirtuales, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("ModificarMaqVirtual", "MaqVirtuales");
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener las Máquinas virtuales de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerMaqVirtualesComp()
        {
            MaqVirtualesAccDatos objMaqVirtualesAccDatos = new MaqVirtualesAccDatos((string)Session["NickUsuario"]);
            return Json(objMaqVirtualesAccDatos.ObtenerMaqVirtuales("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Propósitos registrados del archivo JSON
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerPropositosComp()
        {
            return Json(ObtenerPropositosJSON(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Obtener todos los Propósitos del JSON
        /// </summary>
        /// <returns></returns>
        public List<Propositos> ObtenerPropositosJSON()
        {
            List<Propositos> items = new List<Propositos>();
            using (StreamReader r = new StreamReader(path_JsonPropositos))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Propositos>>(json);
            }
            return items;
        }
        #endregion
    }
}