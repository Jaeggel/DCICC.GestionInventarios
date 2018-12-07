using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class TipoAccesorioAccDatos
    {
        //Instancia para la utilización de LOGS en la clase TipoAccesorioAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public TipoAccesorioAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con los Tipos de Accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesTipoAccesorio ObtenerTipoAccesorio(string nombreFuncion)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("TipoAccesorio/ObtenerTipoAccesorio" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los tipos de accesorios: " + e.Message);
            }
            return msjTipoAccesorio;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio RegistrarTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                var response = client_Service.PostAsJsonAsync("TipoAccesorio/RegistrarTipoAccesorio", infoTipoAccesorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un tipo de accesorio: " + e.Message);
            }
            return msjTipoAccesorio;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Tipo de Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio ActualizarTipoAccesorio(TipoAccesorio infoTipoAccesorio, bool actEstado)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "TipoAccesorio/ActualizarEstadoTipoAccesorio" : "TipoAccesorio/ActualizarTipoAccesorio", infoTipoAccesorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un tipo de accesorio: " + e.Message);
            }
            return msjTipoAccesorio;
        }
        #endregion
    }
}