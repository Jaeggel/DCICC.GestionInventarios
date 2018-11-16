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
        public MensajesUsuarios ObtenerUsuarios(string nombreFuncion)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuarios"+nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message+" - "+msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener una lista de la función UsuariosRoles.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuariosRoles()
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosRoles").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para registrar un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios RegistrarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
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
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
    }
}