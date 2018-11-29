using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.ContMarcalers
{
    [Authorize(Policy = "Member")]
    [Route("Marcas")]
    public class MarcasContMarcaler : Controller
    {
        //Instancia para la utilización de LOGS en la clase MarcasController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de Marcas habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMarcasHab")]
        public MensajesMarcas ObtenerMarcasHab()
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            ConsultasMarcas objConsultasMarcasBD = new ConsultasMarcas();
            msjMarcas = objConsultasMarcasBD.ObtenerMarcas("marcashabilitadas");
            if (msjMarcas.OperacionExitosa)
            {
                Logs.Info("Consulta de Marcas realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas las Marcas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMarcasComp")]
        public MensajesMarcas ObtenerMarcasComp()
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            ConsultasMarcas objConsultasMarcasBD = new ConsultasMarcas();
            msjMarcas = objConsultasMarcasBD.ObtenerMarcas("consultamarca");
            if (msjMarcas.OperacionExitosa)
            {
                Logs.Info("Consulta de Marcas realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar una nueva Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("RegistrarMarca")]
        public MensajesMarcas RegistrarMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;            
            InsercionesMarcas objInsercionesMarcasBD = new InsercionesMarcas();
            msjMarcas = objInsercionesMarcasBD.RegistroMarca(infoMarca);
            if (msjMarcas.OperacionExitosa)
            {
                Logs.Info("Registro de Marca realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar una Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("ActualizarMarca")]
        public MensajesMarcas ActualizarMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;
            ActualizacionesMarcas objActualizacionesMarcasBD = new ActualizacionesMarcas();
            msjMarcas = objActualizacionesMarcasBD.ActualizacionMarca(infoMarca);
            if (msjMarcas.OperacionExitosa)
            {
                Logs.Info("Actualización de Marca realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoMarca")]
        public MensajesMarcas ActualizarEstadoMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;
            ActualizacionesMarcas objActualizacionesMarcasBD = new ActualizacionesMarcas();
            msjMarcas = objActualizacionesMarcasBD.ActualizacionEstadoMarca(infoMarca);
            if (msjMarcas.OperacionExitosa)
            {
                Logs.Info("Actualización de estado de Marca realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        #endregion
    }
}