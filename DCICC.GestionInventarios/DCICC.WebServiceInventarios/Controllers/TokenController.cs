using System;
using DCICC.AccesoDatos;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using DCICC.Seguridad.TokensJWT;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Route("Token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        //Instancia para la utilización de LOGS en la clase TokenController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Obtención Token
        [HttpPost("ObtenerTokenTransacciones")]
        public IActionResult ObtenerTokenTransacciones([FromBody] string infoUsuarioSesion)
        {
            JwtToken token = null;
            try
            {
                if (infoUsuarioSesion != null)
                {
                    ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                    MensajesUsuarios msjUsuarios = new MensajesUsuarios();
                    msjUsuarios= objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados");
                    if (msjUsuarios.OperacionExitosa)
                    {
                        Usuarios infoUsuario = msjUsuarios.ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuarioSesion);
                        if (infoUsuario != null)
                        {
                            token = ConfiguracionToken();
                            ConfigBaseDatos.SetCadenaConexion(string.Format("Server='192.168.1.11';Port=5432;User Id={0};Password={1};Database=DCICC_BDInventario; CommandTimeout=3020;", infoUsuario.NickUsuario.ToLower(), ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario)));
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
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo generar el token de autorización para transacciones del usuario: {0}: {1}",infoUsuarioSesion,e.Message));
                return Unauthorized();
            }
            return Ok(token.Value);    
        }
        #endregion
        #region Generación Token
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
        #endregion
    }
}