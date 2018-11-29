using DCICC.AccesoDatos.ActualizacionesBD;
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
    [Route("MaqVirtuales")]
    public class MaqVirtualesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase MaqVirtualesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de las Máquinas Virtuales habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMaqVirtualesHab")]
        public MensajesMaqVirtuales ObtenerMaqVirtualesHab()
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            ConsultasMaqVirtuales objConsultasMaqVirtualesBD = new ConsultasMaqVirtuales();
            msjMaqVirtuales = objConsultasMaqVirtualesBD.ObtenerMaqVirtuales("maqvirtualeshabilitados");
            if (msjMaqVirtuales.OperacionExitosa)
            {
                Logs.Info("Consulta de Máquinas Virtuales realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas las Máquinas Virtuales de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMaqVirtualesComp")]
        public MensajesMaqVirtuales ObtenerMaqVirtualesComp()
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            ConsultasMaqVirtuales objConsultasMaqVirtualesBD = new ConsultasMaqVirtuales();
            msjMaqVirtuales = objConsultasMaqVirtualesBD.ObtenerMaqVirtuales("maqvsisto");
            if (msjMaqVirtuales.OperacionExitosa)
            {
                Logs.Info("Consulta de Máquinas Virtuales realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para registrar una nueva Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("RegistrarMaqVirtual")]
        public MensajesMaqVirtuales RegistrarMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            InsercionesMaqVirtuales objInsercionesMaqVirtualesBD = new InsercionesMaqVirtuales();
            msjMaqVirtuales = objInsercionesMaqVirtualesBD.RegistroMaqVirtual(infoMaqVirtual);
            if (msjMaqVirtuales.OperacionExitosa)
            {
                Logs.Info("Registro de Máquina Virtual realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para actualizar una Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("ActualizarMaqVirtual")]
        public MensajesMaqVirtuales ActualizarMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            ActualizacionesMaqVirtuales objActualizacionesMaqVirtualesBD = new ActualizacionesMaqVirtuales();
            msjMaqVirtuales = objActualizacionesMaqVirtualesBD.ActualizacionMaqVirtual(infoMaqVirtual);
            if (msjMaqVirtuales.OperacionExitosa)
            {
                Logs.Info("Actualización de Máquina Virtual realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoMaqVirtual")]
        public MensajesMaqVirtuales ActualizarEstadoMaqVirtual([FromBody] MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = null;
            ActualizacionesMaqVirtuales objActualizacionesMaqVirtualesBD = new ActualizacionesMaqVirtuales();
            msjMaqVirtuales = objActualizacionesMaqVirtualesBD.ActualizacionEstadoMaqVirtual(infoMaqVirtual);
            if (msjMaqVirtuales.OperacionExitosa)
            {
                Logs.Info("Actualización de estado de Máquina Virtual realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
    }
}