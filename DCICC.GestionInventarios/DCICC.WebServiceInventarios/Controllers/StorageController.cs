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
    [Route("Storage")]
    public class StorageController : Controller
    {
        //Instancia para la utilización de LOGS en la clase StorageActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de los Storage habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerStorageHab")]
        public MensajesStorage ObtenerStorageHab()
        {
            MensajesStorage msjStorage = null;
            ConsultasStorage objConsultasStorageBD = new ConsultasStorage();
            msjStorage = objConsultasStorageBD.ObtenerStorage("");
            if (msjStorage.OperacionExitosa)
            {
                Logs.Info("Consulta de Storage realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjStorage.MensajeError);
            }
            return msjStorage;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Storage de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerStorageComp")]
        public MensajesStorage ObtenerStorageComp()
        {
            MensajesStorage msjStorage = null;
            ConsultasStorage objConsultasStorageBD = new ConsultasStorage();
            msjStorage = objConsultasStorageBD.ObtenerStorage("consultastorage");
            if (msjStorage.OperacionExitosa)
            {
                Logs.Info("Consulta de Storage realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjStorage.MensajeError);
            }
            return msjStorage;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        [HttpPost("RegistrarStorage")]
        public MensajesStorage RegistrarStorage([FromBody] Storage infoStorage)
        {
            MensajesStorage msjStorage = null;
            InsercionesStorage objInsercionesStorageBD = new InsercionesStorage();
            msjStorage = objInsercionesStorageBD.RegistroStorage(infoStorage);
            if (msjStorage.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Storage \"{0}\" realizado exitosamente.", infoStorage.NombreStorage));
            }
            else
            {
                Logs.Error(msjStorage.MensajeError);
            }
            return msjStorage;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        [HttpPost("ActualizarStorage")]
        public MensajesStorage ActualizarStorage([FromBody] Storage infoStorage)
        {
            MensajesStorage msjStorage = null;
            ActualizacionesStorage objActualizacionesStorageActBD = new ActualizacionesStorage();
            msjStorage = objActualizacionesStorageActBD.ActualizacionStorage(infoStorage);
            if (msjStorage.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Storage con ID: {0} realizada exitosamente.", infoStorage.IdStorage));
            }
            else
            {
                Logs.Error(msjStorage.MensajeError);
            }
            return msjStorage;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoStorage")]
        public MensajesStorage ActualizarEstadoStorage([FromBody] Storage infoStorage)
        {
            MensajesStorage msjStorage = null;
            ActualizacionesStorage objActualizacionesStorageActBD = new ActualizacionesStorage();
            msjStorage = objActualizacionesStorageActBD.ActualizacionEstadoStorage(infoStorage);
            if (msjStorage.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Storage con ID: {0} realizada exitosamente.", infoStorage.IdStorage));
            }
            else
            {
                Logs.Error(msjStorage.MensajeError);
            }
            return msjStorage;
        }
        #endregion
    }
}