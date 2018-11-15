using DCICC.GestionInventarios.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos.UsuariosBD
{
    public class RolesAccDatos
    {
        //Instancia para la utilización de LOGS en la clase RolesAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Constructor para iniciar la comunicación con los métodos del WebService
        /// </summary>
        /// <param name="infoLogin"></param>
        public RolesAccDatos(Login infoLogin)
        {
            ComunicacionServicio objComunicacionServ = new ComunicacionServicio(infoLogin);
        }
        /// <summary>
        /// Método para obtener una lista con los roles habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Roles> ObtenerRolesHab()
        {
            try
            {
                List<Roles> lstRoles = new List<Roles>();
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Roles/ObtenerRolesHab").Result;
                if (response.IsSuccessStatusCode)
                {
                    var rolesJson = response.Content.ReadAsStringAsync().Result;
                    lstRoles = JsonConvert.DeserializeObject<List<Roles>>(rolesJson);
                }
                return lstRoles;
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los roles habilitados: " + e.Message);
                return null;
            }
        }
    }
}