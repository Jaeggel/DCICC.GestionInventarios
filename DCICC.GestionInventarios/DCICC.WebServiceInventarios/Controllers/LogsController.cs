using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("LogsSistema")]
    public class LogsController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LogsController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (POST) para registrar un nuevo log en la base de datos.
        /// </summary>
        /// <param name="infoLogs"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLog")]
        public bool RegistrarLog(string infoLogs)
        {
            try
            {
                InsercionesLogs objInsercionesLogsBD = new InsercionesLogs();
                //return objInsercionesLogsBD.RegistroLogs(infoLogs);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el log: " + e.Message);
                
            }
            return false;
        }
    }
}