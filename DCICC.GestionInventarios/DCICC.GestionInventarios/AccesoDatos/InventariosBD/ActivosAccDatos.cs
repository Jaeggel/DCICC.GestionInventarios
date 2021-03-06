﻿using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class ActivosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase ActivosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public ActivosAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        #endregion
        #region Consultas
        /// <summary>
        /// Método para obtener una lista con los Activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesActivos ObtenerActivos(string nombreFuncion)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(string.Format("Activos/ObtenerActivos{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los activos: {0}",e.Message));
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para obtener una lista con los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesCQR ObtenerCQR()
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Activos/ObtenerCQR").Result;
                if (response.IsSuccessStatusCode)
                {
                    var CQRJson = response.Content.ReadAsStringAsync().Result;
                    msjCQR = JsonConvert.DeserializeObject<MensajesCQR>(CQRJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener el CQR: {0}.",e.Message));
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para obtener una lista con los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesCQR ObtenerIdCQR()
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Activos/ObtenerIdCQR").Result;
                if (response.IsSuccessStatusCode)
                {
                    var CQRJson = response.Content.ReadAsStringAsync().Result;
                    msjCQR = JsonConvert.DeserializeObject<MensajesCQR>(CQRJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener el CQR: {0}.",e.Message));
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para obtener una lista con los Históricos de Activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesHistoricoActivos ObtenerHistoricoActivos()
        {
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Activos/ObtenerHistoricoActivosComp").Result;
                if (response.IsSuccessStatusCode)
                {
                    var HistActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjHistActivos = JsonConvert.DeserializeObject<MensajesHistoricoActivos>(HistActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los históricos de los activos: {0}.",e.Message));
            }
            return msjHistActivos;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos RegistrarActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                var response = client_Service.PostAsJsonAsync("Activos/RegistrarActivo", infoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un activo: {0}",e.Message));
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para registrar un nuevo CQR en la base de datos.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        public MensajesCQR RegistrarCQR(CQR infoCQR)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                var response = client_Service.PostAsJsonAsync("Activos/RegistrarCQR", infoCQR).Result;
                if (response.IsSuccessStatusCode)
                {
                    var CQRJson = response.Content.ReadAsStringAsync().Result;
                    msjCQR = JsonConvert.DeserializeObject<MensajesCQR>(CQRJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un CQR: {0}",e.Message));
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para registrar un Activo en el Historico de activos en la base de datos.
        /// </summary>
        /// <param name="infoHistActivo"></param>
        /// <returns></returns>
        public MensajesHistoricoActivos RegistrarHistoricoActivo(HistoricoActivos infoHistActivo)
        {
            MensajesHistoricoActivos msjHistActivos = new MensajesHistoricoActivos();
            try
            {
                var response = client_Service.PostAsJsonAsync("Activos/RegistrarHistoricoActivo", infoHistActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var histActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjHistActivos = JsonConvert.DeserializeObject<MensajesHistoricoActivos>(histActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un historico: {0}",e.Message));
            }
            return msjHistActivos;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesActivos ActualizarActivo(Activos infoActivo,bool actEstado)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "Activos/ActualizarEstadoActivo" : "Activos/ActualizarActivo", infoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un activo: {0}",e.Message));
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para actualizar el estado impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// /// <param name="actEstado">Boolean para definir si se actualizará un solo CQR o una lista</param>
        /// <returns></returns>
        public MensajesCQR ActualizarCQR(Activos infoActivo,List<Activos> lstActivos,bool actEstado)
        {
            MensajesCQR msjActivos = new MensajesCQR();
            try
            {
                var response = actEstado?client_Service.PostAsJsonAsync("Activos/ActualizarCQRLista", lstActivos).Result: client_Service.PostAsJsonAsync("Activos/ActualizarCQR", infoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesCQR>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un CQR: {0}",e.Message));
            }
            return msjActivos;
        }
        #endregion
    }
}