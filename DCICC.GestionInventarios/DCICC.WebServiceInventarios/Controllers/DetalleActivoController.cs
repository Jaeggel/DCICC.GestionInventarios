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
    [Route("DetalleActivo")]
    public class DetalleActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CQRActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todos los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCQR")]
        public MensajesCQR ObtenerCQR()
        {
            MensajesCQR msjCQR = null;
            try
            {
                ConsultasDetalleActivo objConsultasCQRBD = new ConsultasDetalleActivo();
                msjCQR = objConsultasCQRBD.ObtenerCQR("consultaCQR");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los CQR: " + e.Message);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo CQR en la base de datos.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        [HttpPost("RegistrarCQR")]
        public MensajesCQR RegistrarCQR([FromBody] CQR infoCQR)
        {
            MensajesCQR msjCQR = null;
            try
            {
                InsercionesDetalleActivo objInsercionesCQRBD = new InsercionesDetalleActivo();
                msjCQR = objInsercionesCQRBD.RegistroCQR(infoCQR);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el CQR: " + e.Message);
            }
            return msjCQR;
        }
    }
}