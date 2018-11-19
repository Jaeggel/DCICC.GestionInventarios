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
    [Route("Laboratorios")]
    public class LaboratoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase LaboratoriosActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los laboratorios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLaboratoriosHab")]
        public MensajesLaboratorios ObtenerLaboratoriosHab()
        {
            MensajesLaboratorios msjLaboratorios = null;
            try
            {
                ConsultasLaboratorios objConsultasLaboratoriosBD = new ConsultasLaboratorios();
                msjLaboratorios = objConsultasLaboratoriosBD.ObtenerLaboratorios("laboratorioshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los laboratorios: " + e.Message);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los laboratorios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerLaboratoriosComp")]
        public MensajesLaboratorios ObtenerLaboratoriosComp()
        {
            MensajesLaboratorios msjLaboratorios = null;
            try
            {
                ConsultasLaboratorios objConsultasLaboratoriosBD = new ConsultasLaboratorios();
                msjLaboratorios = objConsultasLaboratoriosBD.ObtenerLaboratorios("consultalaboratorios");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los laboratorios: " + e.Message);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("RegistrarLaboratorio")]
        public MensajesLaboratorios RegistrarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = null;
            try
            {
                InsercionesLaboratorios objInsercionesLaboratoriosBD = new InsercionesLaboratorios();
                msjLaboratorios = objInsercionesLaboratoriosBD.RegistroLaboratorio(infoLaboratorio);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el laboratorio: " + e.Message);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método (POST) para actualizar un laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarLaboratorio")]
        public MensajesLaboratorios ActualizarLaboratorio([FromBody] Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = null;
            try
            {
                ActualizacionesLaboratorios objActualizacionesLaboratoriosActBD = new ActualizacionesLaboratorios();
                msjLaboratorios = objActualizacionesLaboratoriosActBD.ActualizacionLaboratorio(infoLaboratorio);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el laboratorio: " + e.Message);
            }
            return msjLaboratorios;
        }
    }
}