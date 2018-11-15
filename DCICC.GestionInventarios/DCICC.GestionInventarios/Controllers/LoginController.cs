﻿using DCICC.GestionInventarios.AccesoDatos;
using DCICC.GestionInventarios.AccesoDatos.UsuariosBD;
using DCICC.GestionInventarios.Filtros;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LoginController : Controller
    {
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
                    Usuarios datosUsuario = ComprobarCredenciales(infoLogin);
                    if (datosUsuario != null)
                    {
                        if (datosUsuario.IdRol == 1)
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
                        UsuarioActionFilter.ObtenerUsuario(datosUsuario.NombresUsuario);
                        CorreoActionFilter.ObtenerCorreo(datosUsuario.CorreoUsuario);
                        ViewData["MensajeHome"] = "run";
                        Logs.Info("Autenticación Exitosa");
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
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos(infoLogin);
                var datosUsuario = objUsuariosAccDatos.ObtenerUsuariosComp().Find(x => x.NickUsuario == infoLogin.NickUsuario && x.PasswordUsuario == infoLogin.Password);
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
    }
}