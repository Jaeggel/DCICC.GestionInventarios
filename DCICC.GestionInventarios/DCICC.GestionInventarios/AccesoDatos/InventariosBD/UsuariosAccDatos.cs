using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class UsuariosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructores Comunicación Servicio
        HttpClient client_Service = new HttpClient();
        ComunicacionServicio obj_ComunicacionServicio = new ComunicacionServicio();
        /// <summary>
        /// Constructor para inicializar el Token de Autorización de Transacciones
        /// </summary>
        /// <param name="NickUsuario_Sesion">Nick del usuario de la sesión actual</param>
        public UsuariosAccDatos(string NickUsuario_Sesion)
        {
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", obj_ComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        /// <summary>
        /// Constructor para acceder a usuarios para recuperar contraseña
        /// </summary>
        public UsuariosAccDatos() {
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLWebServiceInventarios"]);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
        #region Autenticación de Usuarios
        /// <summary>
        /// Método para obtener los datos del usuario con el que se está accediendo al sistema.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios AutenticarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("AccesoServicio/AutenticarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para autenticar el usuario: {0}", e.Message));
            }
            return msjUsuarios;
        }
        #endregion
        #region Recuperar Password
        /// <summary>
        /// Método para obtener los datos del usuario que desee recuperar su contraseña.
        /// </summary>
        /// <param name="infoCorreo"></param>
        /// <returns></returns>
        public MensajesUsuarios RecuperarPassword(string infoCorreo)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("AccesoServicio/RecuperarPassword", infoCorreo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para autenticar el usuario: {0}", e.Message));
            }
            return msjUsuarios;
        }
        #endregion
        #region Consultas
        /// <summary>
        /// Método para obtener una lista todos los Usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarios(string nombreFuncion)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(string.Format("Usuarios/ObtenerUsuarios{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de todos los usuarios: {0}.",e.Message));
            }
            return msjUsuarios;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios RegistrarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Usuarios/RegistrarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un usuario: {0}",e.Message));
            }
            return msjUsuarios;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <param name="actPerfil"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizarUsuario(Usuarios infoUsuario, int opAct)
        {
            string funcionServ = string.Empty;
            if (opAct == 1)
            {
                funcionServ = "Usuarios/ActualizarUsuario";
            }
            else if (opAct == 2)
            {
                funcionServ = "Usuarios/ActualizarEstadoUsuario";
            }
            else if (opAct == 3)
            {
                funcionServ = "Usuarios/ActualizarPerfilUsuario";
            }
            else
            {
                funcionServ = "Usuarios/ActualizarPasswordUsuario";
            }
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync(funcionServ, infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un usuario: {0}",e.Message));
            }
            return msjUsuarios;
        }
        #endregion
        #region Eliminaciones
        /// <summary>
        /// Método para eliminar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios EliminarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Usuarios/EliminarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para eliminar un usuario: {0}",e.Message));
            }
            return msjUsuarios;
        }
        #endregion
    }
}