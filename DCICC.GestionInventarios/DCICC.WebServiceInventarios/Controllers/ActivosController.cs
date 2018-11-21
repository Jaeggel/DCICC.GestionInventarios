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
    [Route("Activos")]
    public class ActivosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CQRActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los activos habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosHab")]
        public MensajesActivos ObtenerActivosHab()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
                msjActivos = objConsultasActivosBD.ObtenerActivos("activoshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las activos: " + e.Message);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosComp")]
        public MensajesActivos ObtenerActivosComp()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
                msjActivos = objConsultasActivosBD.ObtenerActivos("...");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los activos: " + e.Message);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarActivo")]
        public MensajesActivos RegistrarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            try
            {
                InsercionesActivos objInsercionesActivosBD = new InsercionesActivos();
                msjActivos = objInsercionesActivosBD.RegistroActivo(infoActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el activo: " + e.Message);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para actualizar un activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarActivo")]
        public MensajesActivos ActualizarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            try
            {
                ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
                msjActivos = objActualizacionesActivosBD.ActualizacionActivo(infoActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el activo: " + e.Message);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCQR")]
        public MensajesCQR ObtenerCQR()
        {
            MensajesCQR msjCQR = null;
            try
            {
                ConsultasActivos objConsultasCQRBD = new ConsultasActivos();
                msjCQR = objConsultasCQRBD.ObtenerCQR("consultaCQR");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los CQR: " + e.Message);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo CQR en la base de datos.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        [HttpPost("RegistrarCQR")]
        public MensajesCQR RegistrarCQR([FromBody] CQR infoCQR)
        {
            MensajesCQR msjCQR = null;
            try
            {
                InsercionesActivos objInsercionesCQRBD = new InsercionesActivos();
                msjCQR = objInsercionesCQRBD.RegistroCQR(infoCQR);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el CQR: " + e.Message);
            }
            return msjCQR;
        }
    }
}