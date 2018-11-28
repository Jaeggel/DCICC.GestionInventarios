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
    [Route("Activos")]
    public class ActivosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CQRActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de los activos habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosHab")]
        public MensajesActivos ObtenerActivosHab()
        {
            MensajesActivos msjActivos = new MensajesActivos();            
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("activoshabilitados");
            if(msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosComp")]
        public MensajesActivos ObtenerActivosComp()
        {
            MensajesActivos msjActivos = new MensajesActivos();            
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerActivos("...");            
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los nombres de los activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerActivosNombres")]
        public MensajesActivos ObtenerNombresActivos()
        {
            MensajesActivos msjActivos = new MensajesActivos();
            ConsultasActivos objConsultasActivosBD = new ConsultasActivos();
            msjActivos = objConsultasActivosBD.ObtenerNombresActivos();
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Consulta de nombres de activos realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarActivo")]
        public MensajesActivos RegistrarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            InsercionesActivos objInsercionesActivosBD = new InsercionesActivos();
            msjActivos = objInsercionesActivosBD.RegistroActivo(infoActivo);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Registro de activo realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (POST) para actualizar un activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarActivo")]
        public MensajesActivos ActualizarActivo([FromBody] Activos infoActivo)
        {
            MensajesActivos msjActivos = null;
            ActualizacionesActivos objActualizacionesActivosBD = new ActualizacionesActivos();
            msjActivos = objActualizacionesActivosBD.ActualizacionActivo(infoActivo);
            if (msjActivos.OperacionExitosa)
            {
                Logs.Info("Actualización de activo realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCQR")]
        public MensajesCQR ObtenerCQR()
        {
            MensajesCQR msjCQR = null;
            ConsultasActivos objConsultasCQRBD = new ConsultasActivos();
            msjCQR = objConsultasCQRBD.ObtenerCQR("consultaCQR");
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Consulta de CQR realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los CQR mostrando su id de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerIdCQR")]
        public MensajesCQR ObtenerIdCQR()
        {
            MensajesCQR msjCQR = null;
            ConsultasActivos objConsultasCQRBD = new ConsultasActivos();
            msjCQR = objConsultasCQRBD.ObtenerIdCQR();
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Consulta de ID de CQR realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
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
            InsercionesActivos objInsercionesCQRBD = new InsercionesActivos();
            msjCQR = objInsercionesCQRBD.RegistroCQR(infoCQR);
            if (msjCQR.OperacionExitosa)
            {
                Logs.Info("Registro de CQR realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo activo en el historico en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarHistoricoActivo")]
        public MensajesHistoricoActivos RegistrarHistoricoActivo([FromBody] HistoricoActivos infoHistActivo)
        {
            MensajesHistoricoActivos msjHistActivos = null;
            InsercionesHistoricoActivos objInsercionesHistoricoActivosBD = new InsercionesHistoricoActivos();
            msjHistActivos = objInsercionesHistoricoActivosBD.RegistroHistoricoActivos(infoHistActivo);
            if (msjHistActivos.OperacionExitosa)
            {
                Logs.Info("Registro de Historico de Activo realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjHistActivos.MensajeError);
            }
            return msjHistActivos;
        }
    }
}