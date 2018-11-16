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
    [Route("CategoriaActivo")]
    public class CategoriaActivoController : Controller
    {
        //Instancia para la utilización de LOGS en la clase CategoriaActivoController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todas los categorías habilitadas de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerCategoriasActivosHab")]
        public MensajesCategoriaActivo ObtenerUsuariosHab()
        {
            MensajesCategoriaActivo msjCategoriaActivo = null;
            try
            {
                
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de las categorías: " + e.Message);
            }
            return msjCategoriaActivo;
        }
        /// <summary>
        /// Método (POST) para registrar una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("RegistrarCategoriaActio")]
        public MensajesCategoriaActivo RegistrarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriaActivo msjCategoriaActivo = null;
            try
            {
                
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar la categoría: " + e.Message);
            }
            return msjCategoriaActivo;
        }
        /// <summary>
        /// Método (POST) para actualizar una categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        [HttpPost("ActualizarCategoriaActivo")]
        public MensajesCategoriaActivo ActualizarCategoriaActivo([FromBody] CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriaActivo msjCategoriaActivo = null;
            try
            {

            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar la categoría: " + e.Message);
            }
            return msjCategoriaActivo;
        }
    }
}