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
    [Route("MaqVirtuales")]
    public class MaqVirtualesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase MaqVirtualesContMaqVirtualler
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de las máquinas virtuales habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMaqVirtualesHab")]
        public MensajesMaqVirtuales ObtenerMaqVirtualesHab()
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                ConsultasMaqVirtuales objConsultasMaqVirtualesBD = new ConsultasMaqVirtuales();
                msjMaqVirtuales = objConsultasMaqVirtualesBD.ObtenerMaqVirtuales("maqvirtualeshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las máquinas virtuales: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas las máquinas virtuales de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMaqVirtualesComp")]
        public MensajesMaqVirtuales ObtenerMaqVirtualesComp()
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                ConsultasMaqVirtuales objConsultasMaqVirtualesBD = new ConsultasMaqVirtuales();
                msjMaqVirtuales = objConsultasMaqVirtualesBD.ObtenerMaqVirtuales("maqvsisto");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las máquinas virtuales: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para registrar una nueva máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("RegistrarMaqVirtual")]
        public MensajesMaqVirtuales RegistrarMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            try
            {
                InsercionesMaqVirtuales objInsercionesMaqVirtualesBD = new InsercionesMaqVirtuales();
                msjMaqVirtuales = objInsercionesMaqVirtualesBD.RegistroMaqVirtual(infoMaqVirtual);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar la máquina virtual: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para actualizar una máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("ActualizarMaqVirtual")]
        public MensajesMaqVirtuales ActualizarMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            try
            {
                ActualizacionesMaqVirtuales objActualizacionesMaqVirtualesBD = new ActualizacionesMaqVirtuales();
                msjMaqVirtuales = objActualizacionesMaqVirtualesBD.ActualizacionMaqVirtual(infoMaqVirtual);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la máquina virtual: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoMaqVirtual")]
        public MensajesMaqVirtuales ActualizarEstadoMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            try
            {
                ActualizacionesMaqVirtuales objActualizacionesMaqVirtualesBD = new ActualizacionesMaqVirtuales();
                msjMaqVirtuales = objActualizacionesMaqVirtualesBD.ActualizacionEstadoMaqVirtual(infoMaqVirtual);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la máquina virtual: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
    }
}