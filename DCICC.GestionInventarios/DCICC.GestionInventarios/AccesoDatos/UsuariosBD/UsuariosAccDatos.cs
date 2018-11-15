﻿using DCICC.GestionInventarios.Models;
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
        /// Constructor para iniciar la comunicación con los métodos del WebService
        /// </summary>
        /// <param name="infoLogin"></param>
        public UsuariosAccDatos(Login infoLogin)
        {
            ComunicacionServicio objComunicacionServ = new ComunicacionServicio(infoLogin);
        }
        /// <summary>
        /// Método para obtener una lista con todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> ObtenerUsuariosComp()
        {
            try
            {
                List<Usuarios> lstUsuarios = new List<Usuarios>();
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosComp").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    lstUsuarios = JsonConvert.DeserializeObject<List<Usuarios>>(usersJson);
                }
                return lstUsuarios;
            }
            catch(Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: "+e.Message);
                return null;
            }
        }
        public Boolean RegistrarUsuario(Usuarios infoUsuario)
        {
            Boolean banderaComprobacion=false;
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            //        var response = client.PostAsJsonAsync("EscuelaFut/EstudiantesEscuelaFutbol/Estudiantes/RegistroEstudiantes", estudiante).Result;
            //        if (response.IsSuccessStatusCode)
            //        {
            //            banderaComprobacion = true;
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Logs.Error("Error en el registro de un nuevo usuario: " + e.Message);
            //    banderaComprobacion = false;
            //}
            return banderaComprobacion;
        }
    }
}