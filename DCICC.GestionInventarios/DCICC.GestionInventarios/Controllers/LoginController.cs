using DCICC.GestionInventarios.AccesoDatos;
using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Filtros;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LoginController : Controller
    {
        static Usuarios datos_Usuario = null;
        //Instancia para la utilización de LOGS en la clase Login
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Mètodo (GET) para mostrar la vista Login.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Session["userInfo"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista Login.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Login infoLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     datos_Usuario= ComprobarCredenciales(infoLogin);
                    if (datos_Usuario != null)
                    {
                        if (datos_Usuario.IdRol == 1)
                        {
                            MenuActionFilter.ObtenerMenu("Admin");
                        }
                        else
                        {
                            MenuActionFilter.ObtenerMenu("Usuarios");
                        }
                        int tiempoExpiracionMin = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoExpiracionMin"]);
                        Session["userInfo"] = infoLogin.NickUsuario;
                        Session.Timeout = tiempoExpiracionMin;
                        UsuarioActionFilter.ObtenerUsuario(datos_Usuario.NombresUsuario);
                        CorreoActionFilter.ObtenerCorreo(datos_Usuario.CorreoUsuario);
                        Logs.Info("Autenticación Exitosa");
                        LogsAccDatos objLogsAccDatos = new LogsAccDatos();
                        Logs infoLogs = new Logs
                        {
                            IdUsuario = datos_Usuario.NickUsuario,
                            FechaLogs=DateTime.Now,
                            OperacionLogs="Login",
                            TablaLogs= "Acceso a base de datos.",
                            IpLogs= ObtenerIPCliente()
                        };
                        if(objLogsAccDatos.RegistrarLog(infoLogs).OperacionExitosa)
                        {
                            Logs.Info("Registro de log exitoso");
                        }
                        else
                        {
                            Logs.Error("No se pudo registrar el log");
                        }
                    }
                    else
                    {
                        ViewData["MensajeLogin"] = "true";
                        return View();
                    }
                }
            }
            catch(Exception e)
            {
                Logs.Error("Error en la autenticación con el sistema: " + e.Message);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Método para realizar la comprobación de las credenciales en la base de datos.
        /// </summary>
        /// <param name="infoLogin"></param>
        /// <returns></returns>
        public Usuarios ComprobarCredenciales(Login infoLogin)
        {
            try
            {
                LoginAccDatos objLoginAccDatos = new LoginAccDatos(infoLogin);
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos();
                var datosUsuario = objUsuariosAccDatos.ObtenerUsuarios("Hab").ListaObjetoInventarios.Find(x => x.NickUsuario == infoLogin.NickUsuario && x.PasswordUsuario == infoLogin.PasswordUsuario);
                if (datosUsuario != null)
                {
                    return datosUsuario;
                }                
            }
            catch(Exception e)
            {
                Logs.Error("Error en la comprobación de las credenciales: " + e.Message);
                return null;
            }
            return null;
        }
        /// <summary>
        /// Método para cerrar la sesión actual
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            Session["userInfo"] = null;
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
        /// <summary>
        /// Método para obtener la IP del cliente que accede al sistema.
        /// </summary>
        /// <returns></returns>
        private string ObtenerIPCliente()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }
            return Request.ServerVariables["REMOTE_ADDR"];
        }
        /// <summary>
        /// Método para obtener el usuario actual del sistema
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuarioActual()
        {
            return Json(datos_Usuario, JsonRequestBehavior.AllowGet);
        }
    }
}