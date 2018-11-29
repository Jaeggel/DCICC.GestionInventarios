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
        public RolesAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
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
                HttpResponseMessage response = client_Service.GetAsync("Roles/ObtenerRoles"+nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var rolesJson = response.Content.ReadAsStringAsync().Result;
                    msjRoles = JsonConvert.DeserializeObject<MensajesRoles>(rolesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los roles: " + e.Message +" - "+msjRoles.MensajeError);
            }
            return msjRoles;
        }
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
                Logs.Error("Error en la conexión para registrar un rol: " + e.Message + " - " + msjRoles.MensajeError);
            }
            return msjRoles;
        }
    }
}