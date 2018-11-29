using System;
using Microsoft.AspNetCore.Mvc;
using DCICC.Seguridad.Encryption;
using Microsoft.AspNetCore.Authorization;
using log4net;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Encriptacion")]
    public class EncriptacionController : Controller
    {
        //Instancia para la utilización de LOGS en la clase EncriptacionController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Encriptación
        /// <summary>
        /// Método (Post) que encripta la cadena recibida.
        /// </summary>
        /// <param name="valorSinEncriptar"></param>
        /// <returns></returns>
        [HttpPost("Encriptar")]
        public string Encriptar([FromBody] string valorSinEncriptar)
        {
            try
            {
                return ConfigEncryption.EncriptarValor(valorSinEncriptar);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo encriptar la cadena: " + e.Message);
                return null;
            }
        }
        #endregion
        #region Desencriptación
        /// <summary>
        /// Método (Post) que desencripta la cadena recibida.
        /// </summary>
        /// <param name="valorEncriptado"></param>
        /// <returns></returns>
        [HttpPost("Desencriptar")]
        public string Desencriptar([FromBody] string valorEncriptado)
        {
            try
            {
                return ConfigEncryption.DesencriptarValor(valorEncriptado);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo desencriptar la cadena: " + e.Message);
                return null;
            }
        }
        #endregion
    }
}