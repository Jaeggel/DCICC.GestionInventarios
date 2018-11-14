using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DCICC.Seguridad.Encryption;
using Microsoft.AspNetCore.Authorization;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Encriptacion")]
    public class EncriptacionController : Controller
    {
        /// <summary>
        /// Método (Post) que encripta la cadena recibida.
        /// </summary>
        /// <param name="valorSinEncriptar"></param>
        /// <returns></returns>
        [HttpPost("Encriptar")]
        public string Encriptar([FromBody] String valorSinEncriptar)
        {
            return ConfigEncryption.EncriptarValor(valorSinEncriptar);
        }
        /// <summary>
        /// Método (Post) que desencripta la cadena recibida.
        /// </summary>
        /// <param name="valorEncriptado"></param>
        /// <returns></returns>
        [HttpPost("Desencriptar")]
        public string Desencriptar([FromBody] String valorEncriptado)
        {
            return ConfigEncryption.DesencriptarValor(valorEncriptado);
        }
    }
}