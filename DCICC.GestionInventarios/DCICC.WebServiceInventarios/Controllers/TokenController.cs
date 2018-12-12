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
                Logs.Error(string.Format("No se pudo generar el token de autorización para inicio de transacciones: {0}",e.Message));
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
                Logs.Error(string.Format("No se pudo generar el token de autorización para recuperación de contraseña: {0}", e.Message));
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
                        ConfigBaseDatos.SetCadenaConexion(string.Format("Server='192.168.0.4';Port=5432;User Id={0};Password={1};Database=DCICC_BDInventario; CommandTimeout=3020;", infoUsuario.NickUsuario, ConfigEncryption.EncriptarValor(infoUsuario.PasswordUsuario)));
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
                Logs.Error(string.Format("No se pudo generar el token de autorización para transacciones: {0}",e.Message));
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
        #region Autenticación de Usuarios
        /// <summary>
        /// Método para autenticar el usuario en el web service y retornar sus respectivos datos.
        /// </summary>
        /// <param name="infoUsuario">Información del usuario para </param>
        /// <returns></returns>
        [HttpPost("AutenticarUsuario")]
        public MensajesUsuarios AutenticarUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            Usuarios datosUsuario = null;
            try
            {
                if (infoUsuario.NickUsuario != null && infoUsuario.NickUsuario != null)
                {
                    ConsultasUsuarios objConUsuarios = new ConsultasUsuarios();
                    datosUsuario = objConUsuarios.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuario.NickUsuario && x.PasswordUsuario == infoUsuario.PasswordUsuario);
                    if(datosUsuario!=null)
                    {
                        msjUsuarios.ObjetoInventarios = datosUsuario;
                        msjUsuarios.OperacionExitosa = true;
                    }
                    else
                    {
                        msjUsuarios.ObjetoInventarios = null;
                        msjUsuarios.OperacionExitosa = false;
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo generar el token de autorización para inicio de transacciones: {0}", e.Message));
                msjUsuarios.ObjetoInventarios = null;
                msjUsuarios.OperacionExitosa = false;
            }
            return msjUsuarios;
        }
        #endregion
        #region Recuperación de Contraseña
        [HttpPost("RecuperarPassword")]
        public MensajesUsuarios RecuperarPassword([FromBody] string infoCorreo)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            Usuarios datosUsuario = null;
            try
            {
                if (infoCorreo != null)
                {
                    ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                    datosUsuario = objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.CorreoUsuario == infoCorreo);
                    if(datosUsuario!=null)
                    {
                        datosUsuario.PasswordUsuario = ConfigEncryption.EncriptarValor(datosUsuario.PasswordUsuario);
                        msjUsuarios.ObjetoInventarios = datosUsuario;
                        msjUsuarios.OperacionExitosa = true;
                    }
                    else
                    {
                        msjUsuarios.ObjetoInventarios = null;
                        msjUsuarios.OperacionExitosa = false;
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo generar obtener el usuario para la recuperación de contraseña: {0}", e.Message));
                msjUsuarios.ObjetoInventarios = null;
                msjUsuarios.OperacionExitosa = false;
            }
            return msjUsuarios;
        }
        #endregion
    }
}