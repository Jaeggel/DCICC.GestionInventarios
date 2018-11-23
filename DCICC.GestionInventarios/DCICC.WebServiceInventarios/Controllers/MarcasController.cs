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

namespace DCICC.WebServiceInventarios.ContMarcalers
{
    [Authorize(Policy = "Member")]
    [Route("Marcas")]
    public class MarcasContMarcaler : Controller
    {
        //Instancia para la utilización de LOGS en la clase MarcasController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de marcas habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMarcasHab")]
        public MensajesMarcas ObtenerMarcasHab()
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                ConsultasMarcas objConsultasMarcasBD = new ConsultasMarcas();
                msjMarcas = objConsultasMarcasBD.ObtenerMarcas("marcashabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las marcas: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todas las marcas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerMarcasComp")]
        public MensajesMarcas ObtenerMarcasComp()
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                ConsultasMarcas objConsultasMarcasBD = new ConsultasMarcas();
                msjMarcas = objConsultasMarcasBD.ObtenerMarcas("consultamarca");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las marcas: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (POST) para registrar una nueva marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("RegistrarMarca")]
        public MensajesMarcas RegistrarMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;
            try
            {
                InsercionesMarcas objInsercionesMarcasBD = new InsercionesMarcas();
                msjMarcas = objInsercionesMarcasBD.RegistroMarca(infoMarca);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar la marca: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (POST) para actualizar una marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("ActualizarMarca")]
        public MensajesMarcas ActualizarMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;
            try
            {
                ActualizacionesMarcas objActualizacionesMarcasBD = new ActualizacionesMarcas();
                msjMarcas = objActualizacionesMarcasBD.ActualizacionMarca(infoMarca);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la marca: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoMarca")]
        public MensajesMarcas ActualizarEstadoMarca([FromBody] Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = null;
            try
            {
                ActualizacionesMarcas objActualizacionesMarcasBD = new ActualizacionesMarcas();
                msjMarcas = objActualizacionesMarcasBD.ActualizacionEstadoMarca(infoMarca);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la marca: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
    }
}