using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.WebServiceInventarios.Configuration;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Activos")]
    public class ActivosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de los Activos habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosHab")]
        public MensajesActivos ObtenerActivosHab()
        {
            MensajesActivos msjActivos = new MensajesActivos();            
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("activoshabilitados");
            if(msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosComp")]
        public MensajesActivos ObtenerActivosComp()
        {
            MensajesActivos msjActivos = new MensajesActivos();            
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("activostotales");            
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los nombres de los Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosNombres")]
        public MensajesActivos ObtenerNombresActivos()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerNombresActivos();
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de nombres de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los Activos en relación al CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosCQR")]
        public MensajesActivos ObtenerActivosCQR()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("activoscqr");
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Activos CQR realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
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
            ConsultasActivos objConsultasCQRBD = new ConsultasActivos();
            msjCQR = objConsultasCQRBD.ObtenerCQR("consultaCQR");
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Consulta de CQR realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los ID de los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerIdCQR")]
        public MensajesCQR ObtenerIdCQR()
        {
            MensajesCQR msjCQR = null;
            ConsultasActivos objConsultasCQRBD = new ConsultasActivos();
            msjCQR = objConsultasCQRBD.ObtenerIdCQR();
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Consulta de ID de CQR realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Historicos de Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerHistoricoActivosComp")]
        public MensajesHistoricoActivos ObtenerHistoricoActivosComp()
        {
            MensajesHistoricoActivos msjActivos = new MensajesHistoricoActivos();
            ConsultasHistoricoActivos objConsultasHistActivosBD = new ConsultasHistoricoActivos();
            msjActivos = objConsultasHistActivosBD.ObtenerHistoricoActivos();
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Históricos de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Especificaciones Adicionales de los Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosEspAdi")]
        public MensajesActivos ObtenerActivosEspAdi()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("especificacionesactivos");
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Especificaciones Adicionales de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los activos que han superado su vida útil.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosVidaUtil")]
        public MensajesActivos ObtenerActivosVidaUtil()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("vidautil");
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Vida útil de Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para obtener un activo según su idCQR.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerActivoPorCQR")]
        public MensajesActivos ObtenerActivoPorCQR([FromBody]string idCQR)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivoPorIdCQR(idCQR);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de Activo según su CQR realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarActivo")]
        public MensajesActivos RegistrarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            InsercionesActivos objInsercionesActivosBD = new InsercionesActivos();
            msjActivos = objInsercionesActivosBD.RegistroActivo(infoActivo);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Activo \"{0}\"realizado exitosamente.", infoActivo.NombreActivo));
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
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
            InsercionesActivos objInsercionesCQRBD = new InsercionesActivos();
            msjCQR = objInsercionesCQRBD.RegistroCQR(infoCQR);
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de CQR \"{0}\" realizado exitosamente.",infoCQR.IdCqr));
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo Activo en el Historico de Activos en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarHistoricoActivo")]
        public MensajesHistoricoActivos RegistrarHistoricoActivo([FromBody] HistoricoActivos infoHistActivo)
        {
            MensajesHistoricoActivos msjHistActivos = null;
            InsercionesHistoricoActivos objInsercionesHistoricoActivosBD = new InsercionesHistoricoActivos();
            msjHistActivos = objInsercionesHistoricoActivosBD.RegistroHistoricoActivos(infoHistActivo);
            if (msjHistActivos.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Historico de Activo con ID: {0}-{1} realizado exitosamente.",infoHistActivo.IdActivo,infoHistActivo.IdAccesorio));
            }
            else
            {
                Logs.Error(msjHistActivos.MensajeError);
            }
            return msjHistActivos;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarActivo")]
        public MensajesActivos ActualizarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
            msjActivos = objActualizacionesActivosBD.ActualizacionActivo(infoActivo);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Activo con ID: {0} realizada exitosamente.",infoActivo.IdActivo));
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoActivo")]
        public MensajesActivos ActualizarEstadoActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
            msjActivos = objActualizacionesActivosBD.ActualizacionEstadoActivo(infoActivo);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Activo con ID: {0} realizada exitosamente.", infoActivo.IdActivo));
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarCQR")]
        public MensajesCQR ActualizarCQR([FromBody] Activos infoActivo)
        {
            MensajesCQR msjCQR = null;
            ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
            msjCQR = objActualizacionesActivosBD.ActualizacionQR(infoActivo);
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de CQR con ID: {0} realizada exitosamente.",infoActivo.IdCQR));
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="lstActivos"></param>
        /// <returns></returns>
        [HttpPost("ActualizarCQRLista")]
        public MensajesCQR ActualizarCQR([FromBody] List<Activos> lstActivos)
        {
            MensajesCQR msjCQR = null;
            ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
            msjCQR = objActualizacionesActivosBD.ActualizacionQR(lstActivos);
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Actualización de Lista de CQRs realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        #endregion
        #region Generación Bytes QR
        /// <summary>
        /// Método para generar los bytes de un CQR determinado.
        /// </summary>
        /// <param name="idCQR"></param>
        /// <returns></returns>
        [HttpPost("GenerarBytesQR")]
        public byte[] GenerarBytesQR([FromBody] string idCQR)//tipo de metodo por definir
        {
            byte[] bytesQR=null;
            try
            {
                GeneracionCQR objGeneracionQR = new GeneracionCQR();
                Bitmap bitmapQR = objGeneracionQR.GenerarCodigoQR(idCQR);
                bytesQR = objGeneracionQR.GenQRBytes(bitmapQR);
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el bitmap para el código QR: {0}", e.Message));
            }
            return bytesQR;
        }
        #endregion
    }
}