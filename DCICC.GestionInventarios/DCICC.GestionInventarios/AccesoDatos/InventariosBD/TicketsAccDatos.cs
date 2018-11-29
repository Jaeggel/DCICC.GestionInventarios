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
    public class TicketsAccDatos
    {
        //Instancia para la utilización de LOGS en la clase TicketsAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        public TicketsAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        /// <summary>
        /// Método para obtener una lista con los tickets de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesTickets ObtenerTickets(string nombreFuncion)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Tickets/ObtenerTickets" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TicketsJson = response.Content.ReadAsStringAsync().Result;
                    msjTickets = JsonConvert.DeserializeObject<MensajesTickets>(TicketsJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los tickets: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
        /// <summary>
        /// Método para actualizar un ticket en la base de datos.
        /// </summary>
        /// <param name="infoTicket"></param>
        /// <returns></returns>
        public MensajesTickets ActualizarTicket(Tickets infoTicket)
        {
            MensajesTickets msjTickets = new MensajesTickets();
            try
            {
                var response = client_Service.PostAsJsonAsync("Tickets/ActualizarTicket", infoTicket).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TicketsJson = response.Content.ReadAsStringAsync().Result;
                    msjTickets = JsonConvert.DeserializeObject<MensajesTickets>(TicketsJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un ticket: " + e.Message + " - " + msjTickets.MensajeError);
            }
            return msjTickets;
        }
    }
}