using DCICC.GestionInventarios.AccesoDatos;
using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Filtros;
using DCICC.GestionInventarios.Mail;
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
    public class LoginController : Controller
    {
        public static string nickUsuarioSesion = string.Empty;
        public static string ipUsuarioSesion = string.Empty;
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
            Usuarios datosUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    datosUsuario= ComprobarCredenciales(infoLogin);
                    if (datosUsuario != null)
                    {
                        //Definición del menú que tendrá el usuario en el sistema
                        if (datosUsuario.NombreRol.ToLower() == "administrador")
                        {
                            MenuActionFilter.ObtenerMenu("Admin");
                        }
                        else if (datosUsuario.NombreRol.ToLower() == "estudiante")
                        {
                            MenuActionFilter.ObtenerMenu("Estudiantes");
                        }
                        else if (datosUsuario.NombreRol.ToLower() == "reportes")
                        {
                            MenuActionFilter.ObtenerMenu("Reporteria");
                        }
                        else
                        {
                            MenuActionFilter.ObtenerMenu("Usuarios");
                        }//OJO REDIRECCIONAMIENTO -> NO HOME

                        //Logs de reporte de transacción.
                        Logs.Info("Autenticación Exitosa");
                        ipUsuarioSesion = ObtenerIPCliente();
                        //Construcción de sesion de usuario.
                        int tiempoExpiracionMin = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoExpiracionMin"]);
                        Session["userInfo"] = infoLogin.NickUsuario;
                        nickUsuarioSesion= infoLogin.NickUsuario;
                        Session.Timeout = tiempoExpiracionMin;

                        //Definición de parámetros para utilizar en toda la aplicación.
                        UsuarioActionFilter.ObtenerUsuario(datosUsuario.NickUsuario);
                        CorreoActionFilter.ObtenerCorreo(datosUsuario.CorreoUsuario);

                        //Registro de Log para Login
                        RegistroSesionLogs("Login");
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
                Usuarios infoUsuario = new Usuarios
                {
                    NickUsuario=infoLogin.NickUsuario,
                    PasswordUsuario=infoLogin.PasswordUsuario
                };
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos(infoUsuario);
                var datosUsuario = objUsuariosAccDatos.ObtenerUsuariosHab().ListaObjetoInventarios.Find(x => x.NickUsuario == infoLogin.NickUsuario && x.PasswordUsuario == infoLogin.PasswordUsuario);
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
        public bool CerrarSesion()
        {
            try
            {
                //Registro de Log para Login
                RegistroSesionLogs("Logout");
                //Cerrar Sesión
                Session["userInfo"] = null;
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                return true;
            }
            catch(Exception e)
            {
                Logs.Error("Error en el cierre de la sesión: " + e.Message);
            }
            return false;
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
        /// Método para registrar el inicio y cierre de sesión en la tabla logs.
        /// </summary>
        /// <returns></returns>
        public void RegistroSesionLogs(string operacion)
        {
            LogsAccDatos objLogsAccDatos = new LogsAccDatos(nickUsuarioSesion);
            Logs infoLogs = new Logs
            {
                IdUsuario = nickUsuarioSesion,
                FechaLogs = DateTime.Now,
                IpLogs = ipUsuarioSesion
            };
            if (operacion=="Login")
            {
                infoLogs.OperacionLogs = "Login";
                infoLogs.TablaLogs = "Acceso a base de datos.";
            }
            else if(operacion=="Logout")
            {
                infoLogs.OperacionLogs = "Logout";
                infoLogs.TablaLogs = "Finalización de sesión con la base de datos.";
            }
            if (objLogsAccDatos.RegistrarLog(infoLogs).OperacionExitosa)
            {
                Logs.Info("Registro de log exitoso.");
            }
            else
            {
                Logs.Error("No se pudo registrar el log.");
            }
        }
        /// <summary>
        /// Método [POST] para la recuperación de contraseña mediante el envío de la misma por correo electrónico.
        /// El mismo es llamado mediante ajax desde la vista Login.cshtml.
        /// </summary>
        /// <param name="infoCorreo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RecuperarPassword(string infoCorreo)
        {
            try
            {
                if(infoCorreo!=null)
                {
                    UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos();
                    Usuarios infoUsuario = new Usuarios();
                    var datosUsuario = objUsuariosAccDatos.ObtenerUsuarioCorreo(infoCorreo).ListaObjetoInventarios.Find(x => x.CorreoUsuario == infoCorreo);
                    if (datosUsuario != null)
                    {
                        infoUsuario = datosUsuario;
                        ConfiguracionMail mail = new ConfiguracionMail();
                        Correo correo = new Correo
                        {
                            Body = mail.FormatBodyEmailPassword(datosUsuario),
                            EmailEmisor = ConfigurationManager.AppSettings["EmailEmisor"],
                            ClaveEmailEmisor = ConfigurationManager.AppSettings["ClaveEmailEmisor"],
                            EmailReceptor = datosUsuario.CorreoUsuario,
                            Asunto = "Recuperación de Contraseña - Gestión de Inventarios y Ticketing"
                        };
                        mail.SendMail(correo);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener los datos para recuperar la contraseña: "+e.Message);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}