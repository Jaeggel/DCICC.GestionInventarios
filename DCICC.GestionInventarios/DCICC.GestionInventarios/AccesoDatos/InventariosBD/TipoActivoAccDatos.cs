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
    public class TipoActivoAccDatos
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public TipoActivoAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con los tipos de activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesTipoActivo ObtenerTipoActivo(string nombreFuncion)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("TipoActivo/ObtenerTipoActivo" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los tipos de activos: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método para registrar un nuevo tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo RegistrarTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("TipoActivo/RegistrarTipoActivo", infoTipoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un tipo de activo: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
        /// <summary>
        /// Método para actualizar un tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo ActualizarTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("TipoActivo/ActualizarTipoActivo", infoTipoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar una tipo de activo: " + e.Message + " - " + msjTipoActivo.MensajeError);
            }
            return msjTipoActivo;
        }
    }
}