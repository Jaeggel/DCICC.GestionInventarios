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
    [Route("CategoriasActivos")]
    public class CategoriaActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CategoriaActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de todas las Categorías de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCategoriasActivosHab")]
        public MensajesCategoriasActivos ObtenerCategoriasActivosHab()
        {
            MensajesCategoriasActivos msjCategorias = null;
            ConsultasCategoriasActivos objConsultasCategoriasBD = new ConsultasCategoriasActivos();
            msjCategorias = objConsultasCategoriasBD.ObtenerCategoriasActivos("categoriahabilitados");
            if (msjCategorias.OperacionExitosa)
            {
                Logs.Info("Consulta de Categorías realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de las Categorías habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCategoriasActivosComp")]
        public MensajesCategoriasActivos ObtenerCategoriasActivosComp()
        {
            MensajesCategoriasActivos msjCategorias = null;
            ConsultasCategoriasActivos objConsultasCategoriasBD = new ConsultasCategoriasActivos();
            msjCategorias = objConsultasCategoriasBD.ObtenerCategoriasActivos("consultacategoriaactivos");
            if (msjCategorias.OperacionExitosa)
            {
                Logs.Info("Consulta de Categorías realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar una nueva Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarCategoriaActivo")]
        public MensajesCategoriasActivos RegistrarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = null;
            InsercionesCategoriasActivos objInsercionesCategoriasBD = new InsercionesCategoriasActivos();
            msjCategorias = objInsercionesCategoriasBD.RegistroCategoria(infoCategoriaActivo);
            if (msjCategorias.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Categoría \"{0}\" realizado exitosamente.",infoCategoriaActivo.NombreCategoriaActivo));
            }
            else
            {
                Logs.Error(msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar una Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarCategoriaActivo")]
        public MensajesCategoriasActivos ActualizarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = null;
            ActualizacionesCategoriasActivos objActualizacionesCategoriasActBD = new ActualizacionesCategoriasActivos();
            msjCategorias = objActualizacionesCategoriasActBD.ActualizacionCategoria(infoCategoriaActivo);
            if (msjCategorias.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Categoría con ID: {0} realizada exitosamente.",infoCategoriaActivo.IdCategoriaActivo));
            }
            else
            {
                Logs.Error(msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método (POST) para actualizar el estado de una Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoCategoriaActivo")]
        public MensajesCategoriasActivos ActualizarEstadoCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = null;            
            ActualizacionesCategoriasActivos objActualizacionesCategoriasActBD = new ActualizacionesCategoriasActivos();
            msjCategorias = objActualizacionesCategoriasActBD.ActualizacionEstadoCategoria(infoCategoriaActivo);
            if (msjCategorias.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Categoría con ID: {0} realizada exitosamente.", infoCategoriaActivo.IdCategoriaActivo));
            }
            else
            {
                Logs.Error(msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        #endregion
    }
}