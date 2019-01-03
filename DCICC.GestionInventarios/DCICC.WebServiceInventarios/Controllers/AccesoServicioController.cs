using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Route("AccesoServicio")]
    [AllowAnonymous]
    public class AccesoServicioController : Controller
    {
        //Instancia para la utilización de LOGS en la clase AccesoServicioController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            Usuarios datosUsuario = new Usuarios();
            try
            {
                if (infoUsuario.NickUsuario != null && infoUsuario.NickUsuario != null)
                {
                    ConsultasUsuarios objConUsuarios = new ConsultasUsuarios();
                    datosUsuario = objConUsuarios.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.NickUsuario == infoUsuario.NickUsuario && x.PasswordUsuario == infoUsuario.PasswordUsuario);
                    if (datosUsuario != null)
                    {
                        msjUsuarios.ObjetoInventarios = datosUsuario;
                        msjUsuarios.OperacionExitosa = true;
                        Logs.Info(string.Format("Autenticación exitosa con el Web Service del usuario: {0}.",infoUsuario.NickUsuario));
                    }
                    else
                    {
                        msjUsuarios.ObjetoInventarios = null;
                        msjUsuarios.OperacionExitosa = false;
                        Logs.Error(string.Format("Autenticación fallida con el Web Service del usuario: {0}.", infoUsuario.NickUsuario));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Autenticación fallida con el Web Service del usuario: {0}: {1}", infoUsuario.NickUsuario,e.Message));
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
            Usuarios datosUsuario = new Usuarios();
            try
            {
                if (infoCorreo != null)
                {
                    ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                    datosUsuario = objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados").ListaObjetoInventarios.Find(x => x.CorreoUsuario == infoCorreo);
                    if (datosUsuario != null)
                    {
                        datosUsuario.PasswordUsuario = ConfigEncryption.EncriptarValor(datosUsuario.PasswordUsuario);
                        msjUsuarios.ObjetoInventarios = datosUsuario;
                        msjUsuarios.OperacionExitosa = true;
                        Logs.Info(string.Format("Solicitud de datos exitosa para recuperación de contraseña para el correo: {0}.", infoCorreo));
                    }
                    else
                    {
                        msjUsuarios.ObjetoInventarios = null;
                        msjUsuarios.OperacionExitosa = false;
                        Logs.Error(string.Format("Solicitud de datos fallida para recuperación de contraseña para el correo: {0}.", infoCorreo));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Solicitud de datos fallida para recuperación de contraseña para el correo: {0}: {1}", infoCorreo,e.Message));
                msjUsuarios.ObjetoInventarios = null;
                msjUsuarios.OperacionExitosa = false;
            }
            return msjUsuarios;
        }
        #endregion
    }
}