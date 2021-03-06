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
    [Route("SistOperativos")]
    public class SistOperativosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase SistOperativosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas 
        /// <summary>
        /// Método (GET) para obtener una lista de los Sistemas Operativos habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerSistOperativosHab")]
        public MensajesSistOperativos ObtenerSistOperativosHab()
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            ConsultasSistOperativos objConsultasSistOperativosBD = new ConsultasSistOperativos();
            msjSistOperativos = objConsultasSistOperativosBD.ObtenerSistOperativos("sohabilitados");
            if (msjSistOperativos.OperacionExitosa)
            {
                Logs.Info("Consulta de SO realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Sistemas Operativos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerSistOperativosComp")]
        public MensajesSistOperativos ObtenerSistOperativosComp()
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            ConsultasSistOperativos objConsultasSistOperativosBD = new ConsultasSistOperativos();
            msjSistOperativos = objConsultasSistOperativosBD.ObtenerSistOperativos("consultaso");
            if (msjSistOperativos.OperacionExitosa)
            {
                Logs.Info("Consulta de SO realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar una nuevo Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarSistOperativo")]
        public MensajesSistOperativos RegistrarSistOperativo([FromBody] SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            InsercionesSistOperativos objInsercionesSistOperativosBD = new InsercionesSistOperativos();
            msjSistOperativos = objInsercionesSistOperativosBD.RegistroSistOperativo(infoSistOperativo);
            if (msjSistOperativos.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de SO \"{0}\" realizado exitosamente.",infoSistOperativo.NombreSistOperativos));
            }
            else
            {
                Logs.Error(msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarSistOperativo")]
        public MensajesSistOperativos ActualizarSistOperativo([FromBody] SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            ActualizacionesSistOperativos objActualizacionesSistOperativosBD = new ActualizacionesSistOperativos();
            msjSistOperativos = objActualizacionesSistOperativosBD.ActualizacionSistOperativo(infoSistOperativo);
            if (msjSistOperativos.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de SO con ID: {0} realizada exitosamente.",infoSistOperativo));
            }
            else
            {
                Logs.Error(msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoSistOperativo")]
        public MensajesSistOperativos ActualizarEstadoSistOperativo([FromBody] SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            ActualizacionesSistOperativos objActualizacionesSistOperativosBD = new ActualizacionesSistOperativos();
            msjSistOperativos = objActualizacionesSistOperativosBD.ActualizacionEstadoSistOperativo(infoSistOperativo);
            if (msjSistOperativos.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de SO con ID: {0} realizada exitosamente.", infoSistOperativo));
            }
            else
            {
                Logs.Error(msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        #endregion
    }
}