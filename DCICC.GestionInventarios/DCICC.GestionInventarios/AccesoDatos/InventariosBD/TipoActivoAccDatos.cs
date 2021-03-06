﻿using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class TipoActivoAccDatos
    {
        //Instancia para la utilización de LOGS en la clase TipoActivoAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public TipoActivoAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con los Tipos de Activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesTipoActivo ObtenerTipoActivo(string nombreFuncion)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(string.Format("TipoActivo/ObtenerTipoActivo{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los tipos de activos: {0}",e.Message));
            }
            return msjTipoActivo;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo RegistrarTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                var response = client_Service.PostAsJsonAsync("TipoActivo/RegistrarTipoActivo", infoTipoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un tipo de activo: {0}",e.Message));
            }
            return msjTipoActivo;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Tipo de Activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesTipoActivo ActualizarTipoActivo(TipoActivo infoTipoActivo, bool actEstado)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "TipoActivo/ActualizarEstadoTipoActivo" : "TipoActivo/ActualizarTipoActivo", infoTipoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoActivoJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoActivo = JsonConvert.DeserializeObject<MensajesTipoActivo>(TipoActivoJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar una tipo de activo: {0}",e.Message));
            }
            return msjTipoActivo;
        }
        #endregion
    }
}