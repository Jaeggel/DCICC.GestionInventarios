﻿using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
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
    public class CategoriaActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase Usuarios
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista CategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoCategoriaActivo()
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
        /// Método (GET) para mostrar la vista ModificarCategoriaActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarCategoriaActivo()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoCategoriaActivo.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoCategoriaActivo(CategoriaActivo infoCategoriaActivo)
        {
            string mensajesCategorias = string.Empty;
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                CategoriasActivosAccDatos objCategoriasActivosAccDatos = new CategoriasActivosAccDatos(Session["userInfo"].ToString());
                msjCategorias = objCategoriasActivosAccDatos.RegistrarCategoriaActivo(infoCategoriaActivo);
                if (msjCategorias.OperacionExitosa)
                {
                    mensajesCategorias = "La categoría ha sido registrada exitosamente.";
                    TempData["Mensaje"] = mensajesCategorias;
                    Logs.Info(mensajesCategorias);
                }
                else
                {
                    mensajesCategorias = "No se ha podido registrar la categoría: " + msjCategorias.MensajeError;
                    TempData["MensajeError"] = mensajesCategorias;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCategorias + ": " + e.Message);
                return View();
            }
            return RedirectToAction("ModificarCategoriaActivo", "CategoriaActivo");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarCategoriaActivo.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarCategoriaActivo(CategoriaActivo infoCategoriaActivo)
        {
            string mensajesCategorias = string.Empty;
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                CategoriasActivosAccDatos objCategoriasAccDatos = new CategoriasActivosAccDatos(Session["userInfo"].ToString());
                msjCategorias = objCategoriasAccDatos.ActualizarCategoriaActivo(infoCategoriaActivo);
                if (msjCategorias.OperacionExitosa)
                {
                    Logs.Info(mensajesCategorias);
                }
                else
                {
                    mensajesCategorias= "No se ha podido actualizar la categoría: " + msjCategorias.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCategorias + ": " + e.Message);
            }
            return View();
        }
        /// <summary>
        /// Método para obtener todas las categorías de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerCategoriasActivosComp()
        {
            CategoriasActivosAccDatos objCategoriasActAccDatos = new CategoriasActivosAccDatos(Session["userInfo"].ToString());
            return Json(objCategoriasActAccDatos.ObtenerCategoriasActivos("Comp").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener las categorías habilitadas de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerCategoriasActivosHab()
        {
            CategoriasActivosAccDatos objCategoriasActAccDatos = new CategoriasActivosAccDatos(Session["userInfo"].ToString());
            return Json(objCategoriasActAccDatos.ObtenerCategoriasActivos("Hab").ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
    }
}