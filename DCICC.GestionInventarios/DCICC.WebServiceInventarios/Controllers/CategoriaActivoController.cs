using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("CategoriasActivos")]
    public class CategoriaActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CategoriaActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todas las categorías de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCategoriasActivosHab")]
        public MensajesCategoriasActivos ObtenerCategoriasActivosHab()
        {
            MensajesCategoriasActivos msjCategorias = null;
            try
            {
                ConsultasCategoriasActivos objConsultasCategoriasBD = new ConsultasCategoriasActivos();
                msjCategorias = objConsultasCategoriasBD.ObtenerCategoriasActivos("categoriahabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las categorías habilitadas: " + e.Message);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de las categorías habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCategoriasActivosComp")]
        public MensajesCategoriasActivos ObtenerCategoriasActivosComp()
        {
            MensajesCategoriasActivos msjCategorias = null;
            try
            {
                ConsultasCategoriasActivos objConsultasCategoriasBD = new ConsultasCategoriasActivos();
                msjCategorias = objConsultasCategoriasBD.ObtenerCategoriasActivos("consultacategoriaactivos");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de todas las categorías: " + e.Message);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método (POST) para registrar una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarCategoriaActivo")]
        public MensajesCategoriasActivos RegistrarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = null;
            try
            {
                InsercionesCategoriasActivos objInsercionesCategoriasBD = new InsercionesCategoriasActivos();
                msjCategorias = objInsercionesCategoriasBD.RegistroCategoria(infoCategoriaActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar la categoría: " + e.Message);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método (POST) para actualizar una categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarCategoriaActivo")]
        public MensajesCategoriasActivos ActualizarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = null;
            try
            {
                ActualizacionesCategoriasActivos objActualizacionesCategoriasActBD = new ActualizacionesCategoriasActivos();
                msjCategorias = objActualizacionesCategoriasActBD.ActualizacionCategoria(infoCategoriaActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la categoría: " + e.Message);
            }
            return msjCategorias;
        }
    }
}