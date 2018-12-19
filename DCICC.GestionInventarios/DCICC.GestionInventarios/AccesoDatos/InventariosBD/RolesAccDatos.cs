using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class RolesAccDatos
    {
        //Instancia para la utilización de LOGS en la clase RolesAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public RolesAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        #endregion
        #region Consultas
        /// <summary>
        /// Método para obtener una lista con los Roles de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesRoles ObtenerRoles(string nombreFuncion)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(string.Format("Roles/ObtenerRoles{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var rolesJson = response.Content.ReadAsStringAsync().Result;
                    msjRoles = JsonConvert.DeserializeObject<MensajesRoles>(rolesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los roles: {0}",e.Message));
            }
            return msjRoles;
        }
        /// <summary>
        /// Método para obtener los permisos de un rol determinado.
        /// </summary>
        /// <param name="nombreRol"></param>
        /// <returns></returns>
        public MensajesRoles ObtenerPermisosRol(string nombreRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                var response = client_Service.PostAsJsonAsync("Roles/ObtenerRolesPermisos", nombreRol).Result;
                if (response.IsSuccessStatusCode)
                {
                    var RolesJson = response.Content.ReadAsStringAsync().Result;
                    msjRoles = JsonConvert.DeserializeObject<MensajesRoles>(RolesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener los permisos de un Rol: {0}", e.Message));
            }
            return msjRoles;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles RegistrarRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                var response = client_Service.PostAsJsonAsync("Roles/RegistrarRol", infoRol).Result;
                if (response.IsSuccessStatusCode)
                {
                    var rolesJson = response.Content.ReadAsStringAsync().Result;
                    msjRoles = JsonConvert.DeserializeObject<MensajesRoles>(rolesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un rol: {0}",e.Message));
            }
            return msjRoles;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesRoles ActualizarRol(Roles infoRol, bool actEstado)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "Roles/ActualizarEstadoRol" : "Roles/ActualizarRol", infoRol).Result;
                if (response.IsSuccessStatusCode)
                {
                    var RolesJson = response.Content.ReadAsStringAsync().Result;
                    msjRoles = JsonConvert.DeserializeObject<MensajesRoles>(RolesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un Rol: {0}", e.Message));
            }
            return msjRoles;
        }
        #endregion
    }
}