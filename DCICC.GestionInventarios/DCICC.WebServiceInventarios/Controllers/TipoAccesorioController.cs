using DCICC.AccesoDatos.ActualizacionesBD;
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
    [Route("TipoAccesorio")]
    public class TipoAccesorioController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoAccesorioController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de los Tipos de Accesorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoAccesorioHab")]
        public MensajesTipoAccesorio ObtenerTipoAccesorioHab()
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            ConsultasTipoAccesorio objConsultasTipoAccesorioBD = new ConsultasTipoAccesorio();
            msjTipoAccesorio = objConsultasTipoAccesorioBD.ObtenerTipoAccesorio("tipoaccesoriohabilitado");
            if (msjTipoAccesorio.OperacionExitosa)
            {
                Logs.Info("Consulta de Tipo Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Tipos de Accesorios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTipoAccesorioComp")]
        public MensajesTipoAccesorio ObtenerTipoAccesorioComp()
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            ConsultasTipoAccesorio objConsultasTipoAccesorioBD = new ConsultasTipoAccesorio();
            msjTipoAccesorio = objConsultasTipoAccesorioBD.ObtenerTipoAccesorio("consultatipoaccesorio");
            if (msjTipoAccesorio.OperacionExitosa)
            {
                Logs.Info("Consulta de Tipo Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost("RegistrarTipoAccesorio")]
        public MensajesTipoAccesorio RegistrarTipoAccesorio([FromBody] TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = null;
            InsercionesTipoAccesorio objInsercionesTipoAccesorioBD = new InsercionesTipoAccesorio();
            msjTipoAccesorio = objInsercionesTipoAccesorioBD.RegistroTipoAccesorio(infoTipoAccesorio);
            if (msjTipoAccesorio.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Tipo Accesorio \"{0}\" realizado exitosamente.",infoTipoAccesorio.NombreTipoAccesorio));
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarTipoAccesorio")]
        public MensajesTipoAccesorio ActualizarTipoAccesorio([FromBody] TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = null;
            ActualizacionesTipoAccesorio objActualizacionesTipoAccesorioBD = new ActualizacionesTipoAccesorio();
            msjTipoAccesorio = objActualizacionesTipoAccesorioBD.ActualizacionTipoAccesorio(infoTipoAccesorio);
            if (msjTipoAccesorio.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Tipo Accesorio con ID: {0} realizada exitosamente.",infoTipoAccesorio.IdTipoAccesorio));
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoTipoAccesorio")]
        public MensajesTipoAccesorio ActualizarEstadoTipoAccesorio([FromBody] TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = null;
            ActualizacionesTipoAccesorio objActualizacionesTipoAccesorioBD = new ActualizacionesTipoAccesorio();
            msjTipoAccesorio = objActualizacionesTipoAccesorioBD.ActualizacionEstadoTipoAccesorio(infoTipoAccesorio);
            if (msjTipoAccesorio.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Tipo Accesorio con ID: {0} realizada exitosamente.", infoTipoAccesorio.IdTipoAccesorio));
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        #endregion
    }
}