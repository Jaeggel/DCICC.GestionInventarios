using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Seguridad.TokensJWT;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Route("Token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TokenController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para crear y obtener el Token de autenticación para realizar las operaciones con el Servicio REST.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerToken")]
        public IActionResult Create([FromBody] Usuarios infoUsuario)
        {
            JwtToken token = null;
            try{
                if (infoUsuario.NickUsuario != null && infoUsuario.NickUsuario != null)
                {
                    Configuration.ConfigSeguridad confServ = new Configuration.ConfigSeguridad();
                    ConsultasUsuarios objConUsuarios = new ConsultasUsuarios();
                    if (objConUsuarios.ObtenerUsuarios("consultausuarios").ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuario.NickUsuario && x.PasswordUsuario == infoUsuario.PasswordUsuario) != null)
                    {
                        token = new JwtTokenBuilder()
                                 .AddSecurityKey(JwtSecurityKey.Create("DCICC.Inventarios.Secret.Key"))
                                 .AddSubject("Inventarios SecretKey")
                                 .AddIssuer("DCICC.Inventarios.Seguridad.Bearer")
                                 .AddAudience("DCICC.Inventarios.Seguridad.Bearer")
                                 .AddClaim("DCICC.Inventarios.MemberId", "111")
                                 .AddExpiry(confServ.ObtenerTimeExpToken())
                                 .Build();
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception e)
            {
                Logs.Error("No se pudo generar el token de autorización: " + e.Message);
                return Unauthorized();
            }
            return Ok(token.Value);
        }
    }
}