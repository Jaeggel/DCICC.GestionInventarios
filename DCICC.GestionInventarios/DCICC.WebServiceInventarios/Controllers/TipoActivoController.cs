﻿using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("TipoActivo")]
    public class TipoActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los Tipos de Activos habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoActivoHab")]
        public MensajesTipoActivo ObtenerTipoActivoHab()
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            ConsultasTipoActivo objConsultasTipoActivoBD = new ConsultasTipoActivo();
            msjTipoActivo = objConsultasTipoActivoBD.ObtenerTipoActivo("tipoactivohabilitados");
            if (msjTipoActivo.OperacionExitosa)
            {
                Logs.Info("Consulta de Tipo de Activo realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas los Tipos de Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoActivoComp")]
        public MensajesTipoActivo ObtenerTipoActivoComp()
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            ConsultasTipoActivo objConsultasTipoActivoBD = new ConsultasTipoActivo();
            msjTipoActivo = objConsultasTipoActivoBD.ObtenerTipoActivo("tipoactivocategorias");
            if (msjTipoActivo.OperacionExitosa)
            {
                Logs.Info("Consulta de Tipo de Activo realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarTipoActivo")]
        public MensajesTipoActivo RegistrarTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            InsercionesTipoActivo objInsercionesTipoActivoBD = new InsercionesTipoActivo();
            msjTipoActivo = objInsercionesTipoActivoBD.RegistroTipoActivo(infoTipoActivo);
            if (msjTipoActivo.OperacionExitosa)
            {
                Logs.Info("Registro de Tipo de Activo realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para actualizar un Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarTipoActivo")]
        public MensajesTipoActivo ActualizarTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            ActualizacionesTipoActivo objActualizacionesTipoActivoBD = new ActualizacionesTipoActivo();
            msjTipoActivo = objActualizacionesTipoActivoBD.ActualizacionTipoActivo(infoTipoActivo);
            if (msjTipoActivo.OperacionExitosa)
            {
                Logs.Info("Actualización de Tipo de Activo realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoTipoActivo")]
        public MensajesTipoActivo ActualizarEstadoTipoActivo([FromBody] TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = null;
            ActualizacionesTipoActivo objActualizacionesTipoActivoBD = new ActualizacionesTipoActivo();
            msjTipoActivo = objActualizacionesTipoActivoBD.ActualizacionEstadoTipoActivo(infoTipoActivo);
            if (msjTipoActivo.OperacionExitosa)
            {
                Logs.Info("Actualización de estado de Tipo de Activo realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
    }
}