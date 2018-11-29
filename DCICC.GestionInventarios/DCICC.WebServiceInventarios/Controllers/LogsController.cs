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
        /// Método (GET) para obtener una lista de Logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLogsComp")]
        public MensajesLogs ObtenerLogsComp()
        {
            MensajesLogs msjLogs = null;
            ConsultasLogs objConsultasLogsBD = new ConsultasLogs();
            msjLogs = objConsultasLogsBD.ObtenerLogs();
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
                Logs.Info("Registro de Log realizado exitosamente.");
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