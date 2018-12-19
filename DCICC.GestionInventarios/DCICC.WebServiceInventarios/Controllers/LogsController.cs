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
    [Route("Logs")]
    public class LogsController : Controller
    {
        //Instancia para la utilización de Logs en la clase LogsController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista completa de Logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLogsComp")]
        public MensajesLogs ObtenerLogsComp()
        {
            MensajesLogs msjLogs = null;
            ConsultasLogs objConsultasLogsBD = new ConsultasLogs();
            msjLogs = objConsultasLogsBD.ObtenerLogs("consultalogs");
            if (msjLogs.OperacionExitosa)
            {
                Logs.Info("Consulta de Logs realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLogs.MensajeError);
            }
            return msjLogs;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de Logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLogs")]
        public MensajesLogs ObtenerLogs()
        {
            MensajesLogs msjLogs = null;
            ConsultasLogs objConsultasLogsBD = new ConsultasLogs();
            msjLogs = objConsultasLogsBD.ObtenerLogs("");
            if (msjLogs.OperacionExitosa)
            {
                Logs.Info("Consulta de Logs realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLogs.MensajeError);
            }
            return msjLogs;
        }
        /// <summary>
        /// Método (GET) para obtener cuántas veces ha hecho login el usuario actual de la sesión
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerLogsLoginCount")]
        public MensajesLogs ObtenerLogsLoginCount([FromBody] string nickUsuario)
        {
            MensajesLogs msjLogs = null;
            ConsultasLogs objConsultasLogsBD = new ConsultasLogs();
            msjLogs = objConsultasLogsBD.ObtenerLogsLoginCount(nickUsuario);
            if (msjLogs.OperacionExitosa)
            {
                Logs.Info("Consulta de Logs de Inicio de Sesión realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjLogs.MensajeError);
            }
            return msjLogs;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar en un nuevo Log en la base de datos.
        /// </summary>
        /// <param name="infoLogsSistema"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLog")]
        public MensajesLogs RegistrarLog([FromBody]Logs infoLogsSistema)
        {
            MensajesLogs msjLogs = null;
            InsercionesLogs objInsercionesLogsBD = new InsercionesLogs();
            msjLogs=objInsercionesLogsBD.RegistroLogsInicioBD(infoLogsSistema);
            if (msjLogs.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Log de Usuario: {0} realizado exitosamente.",infoLogsSistema.IdUsuario));
            }
            else
            {
                Logs.Error(msjLogs.MensajeError);
            }
            return msjLogs;
        }
        #endregion
    }
}