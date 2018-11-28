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
    [Route("TipoAccesorio")]
    public class TipoAccesorioController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TipoAccesorioContTipoAccesorioler
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los tipos de accesorios habilitados de la base de datos.
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
        /// Método (GET) para obtener una lista de todos los tipos de accesorios de la base de datos.
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
        /// <summary>
        /// Método (POST) para registrar un nuevo tipo de accesorio en la base de datos.
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
                Logs.Info("Registro de Tipo Accesorio realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método (POST) para actualizar un tipo de accesorio en la base de datos.
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
                Logs.Info("Actualización de Tipo Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un tipo de accesorio en la base de datos.
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
                Logs.Info("Actualización de estado de Tipo Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
    }
}