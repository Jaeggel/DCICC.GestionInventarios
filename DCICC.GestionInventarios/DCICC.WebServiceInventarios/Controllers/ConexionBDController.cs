using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos;
using DCICC.Entidades.EntidadesInventarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("BD")]
    public class ConexionBDController : Controller
    {
        /// <summary>
        /// Método (POST) para iniciar la base de datos con el usuario actual.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("IniciarCadenaBD")]
        public bool IniciarCadenaBD([FromBody] Usuarios infoUsuario)
        {
            try
            {
                ConfigBaseDatos.SetCadenaConexion("Server=localhost;Port=5432;User Id=" + infoUsuario.NickUsuario + ";Password=" + infoUsuario.PasswordUsuario + ";Database=DCICC_BDInventario; CommandTimeout=3020;");
                ConfigBaseDatos.ConnectDB();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}