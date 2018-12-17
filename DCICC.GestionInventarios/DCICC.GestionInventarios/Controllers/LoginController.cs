using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Mail;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LoginController : Controller
    {
        public static int cont_Msj=0;
        //Instancia para la utilización de LOGS en la clase LoginController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Mètodo (GET) para mostrar la vista Login.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if ((string)Session["NickUsuario"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        #endregion
        #region Login (POST)
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
                        //Construcción de sesion de usuario.
                        Session["NickUsuario"] = datosUsuario.NickUsuario;
                        Session["CorreoUsuario"] = datosUsuario.CorreoUsuario;
                        Session["IpUsuario"] = ObtenerIPCliente();
                        Session["NombresUsuario"] = datosUsuario.NombresUsuario;
                        //Definición del menú que tendrá el usuario en el sistema
                        if (datosUsuario.NombreRol.ToLower() == "administrador")
                        {
                            Session["PerfilUsuario"] = "Admin";
                        }
                        else if (datosUsuario.NombreRol.ToLower() == "estudiante")
                        {
                            Session["PerfilUsuario"] = "Estudiantes";
                        }
                        else if (datosUsuario.NombreRol.ToLower() == "reportes")
                        {
                            Session["PerfilUsuario"] = "Reporteria";
                        }
                        else if (datosUsuario.NombreRol.ToLower() == "pasante")
                        {
                            Session["PerfilUsuario"] = "Pasantes";
                        }
                        else
                        {
                            Session["PerfilUsuario"] = "Usuarios";
                        }//OJO REDIRECCIONAMIENTO -> NO HOME
                        Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoExpiracionMin"]);           
                        //Logs de reporte de transacción.
                        Logs.Info(string.Format("Autenticación exitosa de usuario: {0}.",infoLogin.NickUsuario));
                        cont_Msj = 1;
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
                Logs.Error(string.Format("Error en la autenticación con el sistema de usuario: {0}: {1}.",infoLogin.NickUsuario,e.Message));
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Método para registrar el Inicio y Cierre de Sesión en la tabla Logs.
        /// </summary>
        /// <returns></returns>
        public void RegistroSesionLogs(string operacion)
        {
            LogsAccDatos objLogsAccDatos = new LogsAccDatos((string)Session["NickUsuario"]);
            Logs infoLogs = new Logs
            {
                IdUsuario = (string)Session["NickUsuario"],
                FechaLogs = DateTime.Now,
                IpLogs = (string)Session["IpUsuario"]
            };
            if (operacion == "Login")
            {
                infoLogs.OperacionLogs = "Login";
                infoLogs.TablaLogs = "Acceso a base de datos.";
            }
            else if (operacion == "Logout")
            {
                infoLogs.OperacionLogs = "Logout";
                infoLogs.TablaLogs = "Finalización de sesión con la base de datos.";
            }
            if (objLogsAccDatos.RegistrarLog(infoLogs).OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de log exitoso de usuario: {0}.",infoLogs.IdUsuario));
            }
            else
            {
                Logs.Error(string.Format("No se pudo registrar el log de usuario: {0}.",infoLogs.IdUsuario));
            }
        }
        #endregion
        #region Recuperación de Password (POST)
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
                if (infoCorreo != null)
                {
                    UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos();
                    var datosUsuario = objUsuariosAccDatos.RecuperarPassword(infoCorreo).ObjetoInventarios;
                    if (datosUsuario != null)
                    {
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
                        Logs.Info(string.Format("El correo electrónico de recuperación de contraseña ha sido enviado correctamente a: {0}.",infoCorreo));
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
                Logs.Error(string.Format("No se pudo obtener los datos para recuperar la contraseña: {0}",e.Message));
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Otros
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
                    NickUsuario = infoLogin.NickUsuario,
                    PasswordUsuario = infoLogin.PasswordUsuario
                };
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos();
                Usuarios datosUsuario = objUsuariosAccDatos.AutenticarUsuario(infoUsuario).ObjetoInventarios;
                if (datosUsuario != null)
                {
                    return datosUsuario;
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la comprobacíón de credenciales de Usuario: {0}: {1}", infoLogin.NickUsuario, e.Message));
            }
            return null;
        }
        /// <summary>
        /// Método para cerrar la sesión actual
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            try
            {
                Logs.Info(string.Format("Cierre de sesión exitoso de: {0}.",(string)Session["NickUsuario"]));
                RegistroSesionLogs("Logout");
                Session["NickUsuario"] = null;
                Session["NombresUsuario"] = null;
                Session["CorreoUsuario"] = null;
                Session["IpUsuario"] = null;
                Session["PerfilUsuario"] = null;
                cont_Msj = 0;
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("Error en el cierre de la sesión: {0}.",e.Message));
            }
            return RedirectToAction("Login","Login");
        }
        /// <summary>
        /// Método para obtener la IP del cliente actual que hace uso del sistema.
        /// </summary>
        /// <returns></returns>
        private string ObtenerIPCliente()
        {
            try
            {
                string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }
            }catch(Exception e)
            {
                Logs.Error(string.Format("No se ha podido obtener la IP del cliente actual: {0}."+e.Message));
                return string.Empty;
            }
            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
    #endregion
}