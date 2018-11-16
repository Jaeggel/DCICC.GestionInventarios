using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Accesorios")]
    public class AccesoriosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase AccesoriosActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todas los accesorios habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerAccesorios")]
        public MensajesAccesorios ObtenerAccesoriosHab()
        {
            MensajesAccesorios msjAccesorios = null;
            try
            {

            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las accesorios: " + e.Message);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        [HttpPost("RegistrarAccesorio")]
        public MensajesAccesorios RegistrarAccesorio([FromBody] Accesorios infoAccesorio)
        {
            MensajesAccesorios msjAccesorio = null;
            try
            {

            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el accesorio: " + e.Message);
            }
            return msjAccesorio;
        }
        /// <summary>
        /// Método (POST) para actualizar un accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorio"></param>
        /// <returns></returns>
        [HttpPost("ActualizarAccesorio")]
        public MensajesAccesorios ActualizarAccesorio([FromBody] Accesorios infoAccesorio)
        {
            MensajesAccesorios msjAccesorio = null;
            try
            {

            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el accesorio: " + e.Message);
            }
            return msjAccesorio;
        }
    }
}