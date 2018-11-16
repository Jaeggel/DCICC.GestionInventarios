using DCICC.GestionInventarios.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos.UsuariosBD
{
    public class LoginAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Constructor para iniciar la comunicación con los métodos del WebService
        /// </summary>
        /// <param name="infoLogin"></param>
        public LoginAccDatos(Login infoLogin)
        {
            ComunicacionServicio objComunicacionServ = new ComunicacionServicio(infoLogin);
        }
        /// <summary>
        /// Método para iniciar la cadena de conexión en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public bool IniciarCadenaBD(Usuarios infoUsuario)
        {
            bool banderaComprobacion=false;
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                var response = clientService.PostAsJsonAsync("BD/IniciarCadenaBD", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    banderaComprobacion = Boolean.Parse(usersJson.ToString());
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en iniciar la cadena de conexión: " + e.Message);
                banderaComprobacion = false;
            }
            return banderaComprobacion;
        }
    }
}