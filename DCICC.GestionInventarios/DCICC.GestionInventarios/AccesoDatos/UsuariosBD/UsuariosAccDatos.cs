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
    public class UsuariosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para obtener una lista con todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> ObtenerUsuarios(string nombreFuncion)
        {
            try
            {
                List<Usuarios> lstUsuarios = new List<Usuarios>();
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuarios"+nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    lstUsuarios = JsonConvert.DeserializeObject<List<Usuarios>>(usersJson);
                }
                return lstUsuarios;
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Método para obtener una lista de la función UsuariosRoles.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> ObtenerUsuariosRoles()
        {
            try
            {
                List<Usuarios> lstUsuariosRoles = new List<Usuarios>();
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosRoles").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    lstUsuariosRoles = JsonConvert.DeserializeObject<List<Usuarios>>(usersJson);
                }
                return lstUsuariosRoles;
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Método para registrar un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public Boolean RegistrarUsuario(Usuarios infoUsuario)
        {
            Boolean banderaComprobacion = false;
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Usuarios/RegistrarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    banderaComprobacion = Boolean.Parse(usersJson.ToString());
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un usuario: " + e.Message);
                banderaComprobacion = false;
            }
            return banderaComprobacion;
        }
    }
}