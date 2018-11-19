﻿using DCICC.GestionInventarios.Models;
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
    public class UsuariosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        ComunicacionServicio obj_ComunicacionServicio = new ComunicacionServicio();
        /// <summary>
        /// Constructor para inicializar el token de autorización de transacciones
        /// </summary>
        /// <param name="NickUsuario_Sesion">Nick del usuario de la sesión actual</param>
        public UsuariosAccDatos(string NickUsuario_Sesion)
        {
            token_Autorizacion= "Bearer "+obj_ComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Constructor para inicializar el token de autorización de inicio de BD
        /// </summary>
        /// <param name="infoUsuario">Objeto del usuario</param>
        public UsuariosAccDatos(Usuarios infoUsuario)
        {
            token_Autorizacion = "Bearer " +obj_ComunicacionServicio.ObtenerTokenInicioBD(infoUsuario);
        }
        /// <summary>
        /// Método para obtener una lista con todos los usuarios de la base de datos.
        /// Importante para inicializar los procesos de la base de datos
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuariosHab()
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosHab").Result;
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
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
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
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
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
        /// <summary>
        /// Método para actualizar un usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Usuarios/ActualizarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
    }
}