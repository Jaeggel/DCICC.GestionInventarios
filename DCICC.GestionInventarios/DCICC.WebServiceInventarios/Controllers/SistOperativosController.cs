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
    [Route("SistOperativos")]
    public class SistOperativosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase SistOperativosController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los sistemas operativos habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerSistOperativosHab")]
        public MensajesSistOperativos ObtenerSistOperativosHab()
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                ConsultasSistOperativos objConsultasSistOperativosBD = new ConsultasSistOperativos();
                msjSistOperativos = objConsultasSistOperativosBD.ObtenerSistOperativos("sohabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los sistemas operativos: " + e.Message + " - " + msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los sistemas operativos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerSistOperativosComp")]
        public MensajesSistOperativos ObtenerSistOperativosComp()
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                ConsultasSistOperativos objConsultasSistOperativosBD = new ConsultasSistOperativos();
                msjSistOperativos = objConsultasSistOperativosBD.ObtenerSistOperativos("consultaso");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los sistemas operativos: " + e.Message + " - " + msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        /// <summary>
        /// Método (POST) para registrar una nuevo sistema operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarSistOperativo")]
        public MensajesSistOperativos RegistrarSistOperativo([FromBody] SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = null;
            try
            {
                InsercionesSistOperativos objInsercionesSistOperativosBD = new InsercionesSistOperativos();
                msjSistOperativos = objInsercionesSistOperativosBD.RegistroSistOperativo(infoSistOperativo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el sistema operativo: " + e.Message + " - " + msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
        /// <summary>
        /// Método (POST) para actualizar un sistema operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarSistOperativo")]
        public MensajesSistOperativos ActualizarSistOperativo([FromBody] SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = null;
            try
            {
                ActualizacionesSistOperativos objActualizacionesSistOperativosBD = new ActualizacionesSistOperativos();
                msjSistOperativos = objActualizacionesSistOperativosBD.ActualizacionSistOperativo(infoSistOperativo);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el sistema operativo: " + e.Message + " - " + msjSistOperativos.MensajeError);
            }
            return msjSistOperativos;
        }
    }
}