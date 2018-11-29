using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
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
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para crear y obtener el Token de autenticación para realizar las operaciones con el Servicio REST.
        /// Utilizado para tener acceso a los usuarios y poder realizar el login.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ObtenerTokenInicioBD")]
        public IActionResult ObtenerTokenInicioBD([FromBody] Usuarios infoUsuario)
        {
            JwtToken token = null;
            try{
                if (infoUsuario.NickUsuario != null && infoUsuario.NickUsuario != null)
                {
                    ConsultasUsuarios objConUsuarios = new ConsultasUsuarios();
                    if (objConUsuarios.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuario.NickUsuario && x.PasswordUsuario == infoUsuario.PasswordUsuario) != null)
                    {
                        token=ConfiguracionToken();
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
        /// <summary>
        /// Método para crear y obtener el Token de autenticación para realizar las operaciones con el Servicio REST.
        /// Utilizado para la recuperación de contraseña.
        /// </summary>
        /// <param name="infoCorreo">Correo electrónico para recuperación de contraseña.</param>
        /// <returns></returns>
        [HttpPost("ObtenerTokenInicioCorreoBD")]
        public IActionResult ObtenerTokenInicioCorreoBD([FromBody] string infoCorreo)
        {
            JwtToken token = null;
            try
            {
                if (infoCorreo != null)
                {
                    ConsultasUsuarios objConUsuarios = new ConsultasUsuarios();
                    if (objConUsuarios.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.CorreoUsuario == infoCorreo) != null)
                    {
                        token = ConfiguracionToken();
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
            catch (Exception e)
            {
                Logs.Error("No se pudo generar el token de autorización: " + e.Message);
                return Unauthorized();
            }
            return Ok(token.Value);
        }
        /// <summary>
        /// Método para crear y obtener el Token de autenticación para realizar las operaciones con el Servicio REST.
        /// A diferencia del anterior, en este método se recibe el usuario con la sesión actual.
        /// </summary>
        /// <param name="infoUsuario">Cadena del usuario de la sesión actual.</param>
        /// <returns></returns>
        [HttpPost("ObtenerTokenTransacciones")]
        public IActionResult ObtenerTokenTransacciones([FromBody] string infoUsuarioSesion)
        {
            JwtToken token = null;
            try
            {
                if (infoUsuarioSesion != null)
                {
                    ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                    MensajesUsuarios msjUsuarios = objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados");
                    Usuarios infoUsuario = msjUsuarios.ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuarioSesion);
                    if(infoUsuario!=null)
                    {
                        token = ConfiguracionToken();
                        ConfigBaseDatos.SetCadenaConexion("Server=localhost;Port=5432;User Id=" + infoUsuario.NickUsuario + ";Password=" + ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario) + ";Database=DCICC_BDInventario; CommandTimeout=3020;");
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
            catch (Exception e)
            {
                Logs.Error("No se pudo generar el token de autorización: " + e.Message);
                return Unauthorized();
            }
            return Ok(token.Value);    
        }
        /// <summary>
        /// Método para inicializar los parámetros del token JWT
        /// </summary>
        /// <returns></returns>
        public JwtToken ConfiguracionToken()
        {
            Configuration.ConfigSeguridad confServ = new Configuration.ConfigSeguridad();
            JwtToken token = new JwtTokenBuilder()
                            .AddSecurityKey(JwtSecurityKey.Create("DCICC.Inventarios.Secret.Key"))
                            .AddSubject("Inventarios SecretKey")
                            .AddIssuer("DCICC.Inventarios.Seguridad.Bearer")
                            .AddAudience("DCICC.Inventarios.Seguridad.Bearer")
                            .AddClaim("DCICC.Inventarios.MemberId", "111")
                            .AddExpiry(confServ.ObtenerTimeExpToken())
                            .Build();
            return token;
        }
    }
}