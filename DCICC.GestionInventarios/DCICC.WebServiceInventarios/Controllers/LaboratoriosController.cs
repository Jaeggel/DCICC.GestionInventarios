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
    [Route("Laboratorios")]
    public class LaboratoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LaboratoriosActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de los Laboratorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLaboratoriosHab")]
        public MensajesLaboratorios ObtenerLaboratoriosHab()
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            ConsultasLaboratorios objConsultasLaboratoriosBD = new ConsultasLaboratorios();
            msjLaboratorios = objConsultasLaboratoriosBD.ObtenerLaboratorios("laboratorioshabilitados");    
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info("Consulta de Laboratorios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Laboratorios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLaboratoriosComp")]
        public MensajesLaboratorios ObtenerLaboratoriosComp()
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            ConsultasLaboratorios objConsultasLaboratoriosBD = new ConsultasLaboratorios();
            msjLaboratorios = objConsultasLaboratoriosBD.ObtenerLaboratorios("consultalaboratorios");
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info("Consulta de Laboratorios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLaboratorio")]
        public MensajesLaboratorios RegistrarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            InsercionesLaboratorios objInsercionesLaboratoriosBD = new InsercionesLaboratorios();
            msjLaboratorios = objInsercionesLaboratoriosBD.RegistroLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Laboratorio \"{0}\" realizado exitosamente.",infoLaboratorio.NombreLaboratorio));
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarLaboratorio")]
        public MensajesLaboratorios ActualizarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            ActualizacionesLaboratorios objActualizacionesLaboratoriosActBD = new ActualizacionesLaboratorios();
            msjLaboratorios = objActualizacionesLaboratoriosActBD.ActualizacionLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Laboratorio con ID: {0} realizada exitosamente.",infoLaboratorio.IdLaboratorio));
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoLaboratorio")]
        public MensajesLaboratorios ActualizarEstadoLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            ActualizacionesLaboratorios objActualizacionesLaboratoriosActBD = new ActualizacionesLaboratorios();
            msjLaboratorios = objActualizacionesLaboratoriosActBD.ActualizacionEstadoLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Laboratorio con ID: {0} realizada exitosamente.", infoLaboratorio.IdLaboratorio));
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        #endregion
    }
}