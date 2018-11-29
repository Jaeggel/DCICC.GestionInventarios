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
    [Route("Accesorios")]
    public class AccesoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase AccesoriosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los Accesorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerAccesoriosHab")]
        public MensajesAccesorios ObtenerAccesoriosHab()
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            ConsultasAccesorios objConsultasAccesoriosBD = new ConsultasAccesorios();
            msjAccesorios = objConsultasAccesoriosBD.ObtenerAccesorios("accesorioshabilitados");
            if (msjAccesorios.OperacionExitosa)
            {
                Logs.Info("Consulta de Accesorios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Accesorios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerAccesoriosComp")]
        public MensajesAccesorios ObtenerAccesoriosComp()
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            ConsultasAccesorios objConsultasAccesoriosBD = new ConsultasAccesorios();
            msjAccesorios = objConsultasAccesoriosBD.ObtenerAccesorios();
            if (msjAccesorios.OperacionExitosa)
            {
                Logs.Info("Consulta de Accesorios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para registrar una nuevo Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("RegistrarAccesorio")]
        public MensajesAccesorios RegistrarAccesorio([FromBody] Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = null;
            InsercionesAccesorios objInsercionesAccesoriosBD = new InsercionesAccesorios();
            msjAccesorios = objInsercionesAccesoriosBD.RegistroAccesorio(infoAccesorios);
            if (msjAccesorios.OperacionExitosa)
            {
                Logs.Info("Registro de Accesorio realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para actualizar un Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("ActualizarAccesorio")]
        public MensajesAccesorios ActualizarAccesorio([FromBody] Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = null;
            ActualizacionesAccesorios objActualizacionesAccesoriosBD = new ActualizacionesAccesorios();
            msjAccesorios = objActualizacionesAccesoriosBD.ActualizacionAccesorio(infoAccesorios);
            if (msjAccesorios.OperacionExitosa)
            {
                Logs.Info("Actualización de Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de un Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoAccesorio")]
        public MensajesAccesorios ActualizarEstadoAccesorio([FromBody] Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = null;
            ActualizacionesAccesorios objActualizacionesAccesoriosBD = new ActualizacionesAccesorios();
            msjAccesorios = objActualizacionesAccesoriosBD.ActualizacionEstadoAccesorio(infoAccesorios);
            if (msjAccesorios.OperacionExitosa)
            {
                Logs.Info("Actualización de Accesorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
    }
}