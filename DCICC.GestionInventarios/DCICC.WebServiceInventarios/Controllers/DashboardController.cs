using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.MensajesInventarios;
using DCICC.WebServiceInventarios.Configuration;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        //Instancia para la utilización de LOGS en la clase DashboardActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener los parámetros que irán en el dashboard.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerDashboard")]
        public MensajesDashboard ObtenerDashboardHab([FromBody]string nickUsuario)
        {
            MensajesDashboard msjDashboard = new MensajesDashboard();
            List<string> sentenciasDashboard = new List<string>();
            SentenciasRoles objSentencias = new SentenciasRoles();
            sentenciasDashboard = objSentencias.ObtenerSentenciasDashboard();
            ConsultasDashboard objConsultasDashboardBD = new ConsultasDashboard();
            msjDashboard = objConsultasDashboardBD.ObtenerDashboard(nickUsuario,sentenciasDashboard);
            if (msjDashboard.OperacionExitosa)
            {
                Logs.Info("Consulta de DashboardTop realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjDashboard.MensajeError);
            }
            return msjDashboard;
        }
        /// <summary>
        /// Método (GET) para obtener los activos por tipo.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerDashboardActivos")]
        public MensajesDashboard ObtenerDashboardActivos()
        {
            MensajesDashboard msjDashboard = new MensajesDashboard();
            ConsultasDashboard objConsultasDashboardBD = new ConsultasDashboard();
            msjDashboard = objConsultasDashboardBD.ObtenerDashboardActivos();
            if (msjDashboard.OperacionExitosa)
            {
                Logs.Info("Consulta de Dashboard Activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjDashboard.MensajeError);
            }
            return msjDashboard;
        }
        #endregion
    }
}