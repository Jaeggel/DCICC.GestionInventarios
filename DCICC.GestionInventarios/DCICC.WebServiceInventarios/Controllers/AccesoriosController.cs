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
    [Route("Accesorios")]
    public class AccesoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase AccesoriosController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los accesorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerAccesoriosHab")]
        public MensajesAccesorios ObtenerAccesoriosHab()
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                ConsultasAccesorios objConsultasAccesoriosBD = new ConsultasAccesorios();
                msjAccesorios = objConsultasAccesoriosBD.ObtenerAccesorios("accesorioshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los accesorios: " + e.Message);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas los accesorios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerAccesoriosComp")]
        public MensajesAccesorios ObtenerAccesoriosComp()
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                ConsultasAccesorios objConsultasAccesoriosBD = new ConsultasAccesorios();
                msjAccesorios = objConsultasAccesoriosBD.ObtenerAccesorios("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los accesorios: " + e.Message);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para registrar una nuevo accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("RegistrarAccesorio")]
        public MensajesAccesorios RegistrarAccesorios([FromBody] Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = null;
            try
            {
                InsercionesAccesorios objInsercionesAccesoriosBD = new InsercionesAccesorios();
                msjAccesorios = objInsercionesAccesoriosBD.RegistroAccesorios(infoAccesorios);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el accesorio: " + e.Message);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para actualizar un accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("ActualizarAccesorio")]
        public MensajesAccesorios ActualizarAccesorios([FromBody] Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = null;
            try
            {
                ActualizacionesAccesorios objActualizacionesAccesoriosBD = new ActualizacionesAccesorios();
                msjAccesorios = objActualizacionesAccesoriosBD.ActualizacionAccesorios(infoAccesorios);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el accesorio: " + e.Message);
            }
            return msjAccesorios;
        }
    }
}