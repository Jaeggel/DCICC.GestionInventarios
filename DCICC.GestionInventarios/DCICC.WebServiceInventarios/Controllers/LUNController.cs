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
    [Route("LUNS")]
    public class LUNController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LUNController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de LUN habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLUNSHab")]
        public MensajesLUN ObtenerLUNHab()
        {
            MensajesLUN msjLUN = new MensajesLUN();
            ConsultasLUN objConsultasLUNBD = new ConsultasLUN();
            msjLUN = objConsultasLUNBD.ObtenerLUN("");
            if (msjLUN.OperacionExitosa)
            {
                Logs.Info("Consulta de LUNS realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLUN.MensajeError);
            }
            return msjLUN;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas las LUN de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLUNSComp")]
        public MensajesLUN ObtenerLUNComp()
        {
            MensajesLUN msjLUN = new MensajesLUN();
            ConsultasLUN objConsultasLUNBD = new ConsultasLUN();
            msjLUN = objConsultasLUNBD.ObtenerLUN("lunstorage");
            if (msjLUN.OperacionExitosa)
            {
                Logs.Info("Consulta de LUN realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLUN.MensajeError);
            }
            return msjLUN;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar una nueva LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLUN")]
        public MensajesLUN RegistrarLUN([FromBody] LUN infoLUN)
        {
            MensajesLUN msjLUN = null;
            InsercionesLUN objInsercionesLUNBD = new InsercionesLUN();
            msjLUN = objInsercionesLUNBD.RegistroLUN(infoLUN);
            if (msjLUN.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de LUN \"{0}\" realizado exitosamente.", infoLUN.NombreLUN));
            }
            else
            {
                Logs.Error(msjLUN.MensajeError);
            }
            return msjLUN;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar una LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost("ActualizarLUN")]
        public MensajesLUN ActualizarLUN([FromBody] LUN infoLUN)
        {
            MensajesLUN msjLUN = null;
            ActualizacionesLUN objActualizacionesLUNBD = new ActualizacionesLUN();
            msjLUN = objActualizacionesLUNBD.ActualizacionLUN(infoLUN);
            if (msjLUN.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de LUN con ID: {0} realizada exitosamente.", infoLUN.IdLUN));
            }
            else
            {
                Logs.Error(msjLUN.MensajeError);
            }
            return msjLUN;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoLUN")]
        public MensajesLUN ActualizarEstadoLUN([FromBody] LUN infoLUN)
        {
            MensajesLUN msjLUN = null;
            ActualizacionesLUN objActualizacionesLUNBD = new ActualizacionesLUN();
            msjLUN = objActualizacionesLUNBD.ActualizacionEstadoLUN(infoLUN);
            if (msjLUN.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de LUN con ID: {0} realizada exitosamente.", infoLUN.IdLUN));
            }
            else
            {
                Logs.Error(msjLUN.MensajeError);
            }
            return msjLUN;
        }
        #endregion
    }
}