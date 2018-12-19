using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.MensajesInventarios;
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
        [HttpPost("ObtenerDashboardTop")]
        public MensajesDashboard ObtenerDashboardHab([FromBody]string nickUsuario)
        {
            MensajesDashboard msjDashboard = null;
            ConsultasDashboard objConsultasDashboardBD = new ConsultasDashboard();
            msjDashboard = objConsultasDashboardBD.ObtenerDashboardTop(nickUsuario);
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
        #endregion
    }
}