using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class RolesAccDatos
    {
        //Instancia para la utilización de LOGS en la clase RolesAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public RolesAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con los roles habilitados de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesRoles ObtenerRoles(string nombreFuncion)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Roles/ObtenerRoles"+nombreFuncion).Result;
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
        /// Método para registrar un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles RegistrarRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Roles/RegistrarRol", infoRol).Result;
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