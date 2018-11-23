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
    [Route("TipoActivo")]
    public class TipoActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoContTipoActivoler
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los tipos de activos habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoActivoHab")]
        public MensajesTipoActivo ObtenerTipoActivoHab()
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                ConsultasTipoActivo objConsultasTipoActivoBD = new ConsultasTipoActivo();
                msjTipoActivo = objConsultasTipoActivoBD.ObtenerTipoActivo("tipocctivohabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tipos de activos: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas los tipos de activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoActivoComp")]
        public MensajesTipoActivo ObtenerTipoActivoComp()
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                ConsultasTipoActivo objConsultasTipoActivoBD = new ConsultasTipoActivo();
                msjTipoActivo = objConsultasTipoActivoBD.ObtenerTipoActivo("tipoactivocategorias");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los tipos de activos: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para registrar una nuevo tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarTipoActivo")]
        public MensajesTipoActivo RegistrarTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            try
            {
                InsercionesTipoActivo objInsercionesTipoActivoBD = new InsercionesTipoActivo();
                msjTipoActivo = objInsercionesTipoActivoBD.RegistroTipoActivo(infoTipoActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el tipo de activo: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para actualizar un tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarTipoActivo")]
        public MensajesTipoActivo ActualizarTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            try
            {
                ActualizacionesTipoActivo objActualizacionesTipoActivoBD = new ActualizacionesTipoActivo();
                msjTipoActivo = objActualizacionesTipoActivoBD.ActualizacionTipoActivo(infoTipoActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el tipo de activo: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoTipoActivo")]
        public MensajesTipoActivo ActualizarEstadoTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            try
            {
                ActualizacionesTipoActivo objActualizacionesTipoActivoBD = new ActualizacionesTipoActivo();
                msjTipoActivo = objActualizacionesTipoActivoBD.ActualizacionTipoActivo(infoTipoActivo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el tipo de activo: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
    }
}