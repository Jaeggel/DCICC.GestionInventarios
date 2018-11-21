using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("Logs")]
    public class LogsController : Controller
    {
        //Instancia para la utilización de Logs en la clase LogsController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para registrar en un nuevo log en la base de datos.
        /// </summary>
        /// <param name="infoLogsSistema"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLog")]
        public MensajesLogs RegistrarLog([FromBody]Logs infoLogsSistema)
        {
            MensajesLogs msjLogs = null;
            try
            {
                InsercionesLogs objInsercionesLogsBD = new InsercionesLogs();
                msjLogs=objInsercionesLogsBD.RegistroLogsInicioBD(infoLogsSistema);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el log: " + e.Message + " - " + msjLogs.MensajeError);
            }
            return msjLogs;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLogsComp")]
        public MensajesLogs ObtenerLogsComp()
        {
            MensajesLogs msjLogs = null;
            try
            {
                ConsultasLogs objConsultasLogsBD = new ConsultasLogs();
                msjLogs = objConsultasLogsBD.ObtenerLogs();
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los logs: " + e.Message + " - " + msjLogs.MensajeError);
            }
            return msjLogs;
        }
    }
}