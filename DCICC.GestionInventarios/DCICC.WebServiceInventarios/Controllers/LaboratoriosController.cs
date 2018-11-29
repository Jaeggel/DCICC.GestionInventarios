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
    [Route("Laboratorios")]
    public class LaboratoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LaboratoriosActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los Laboratorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLaboratoriosHab")]
        public MensajesLaboratorios ObtenerLaboratoriosHab()
        {
            MensajesLaboratorios msjLaboratorios = null;
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
            MensajesLaboratorios msjLaboratorios = null;
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
        /// <summary>
        /// Método (POST) para registrar un nuevo Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLaboratorio")]
        public MensajesLaboratorios RegistrarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = null;
            InsercionesLaboratorios objInsercionesLaboratoriosBD = new InsercionesLaboratorios();
            msjLaboratorios = objInsercionesLaboratoriosBD.RegistroLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info("Registro de Laboratorio realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (POST) para actualizar un Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarLaboratorio")]
        public MensajesLaboratorios ActualizarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = null;
            ActualizacionesLaboratorios objActualizacionesLaboratoriosActBD = new ActualizacionesLaboratorios();
            msjLaboratorios = objActualizacionesLaboratoriosActBD.ActualizacionLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info("Actualización de Laboratorio realizada exitosamente.");
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
            MensajesLaboratorios msjLaboratorios = null;
            ActualizacionesLaboratorios objActualizacionesLaboratoriosActBD = new ActualizacionesLaboratorios();
            msjLaboratorios = objActualizacionesLaboratoriosActBD.ActualizacionEstadoLaboratorio(infoLaboratorio);
            if (msjLaboratorios.OperacionExitosa)
            {
                Logs.Info("Actualízación de estado de Laboratorio realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
    }
}