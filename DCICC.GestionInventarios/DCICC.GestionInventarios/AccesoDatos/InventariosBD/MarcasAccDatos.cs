using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class MarcasAccDatos
    {
        //Instancia para la utilización de LOGS en la clase MarcasAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public MarcasAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con las Marcas de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesMarcas ObtenerMarcas(string nombreFuncion)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Marcas/ObtenerMarcas" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de las marcas: {0}" + e.Message));
            }
            return msjMarcas;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar una nueva Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas RegistrarMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                var response = client_Service.PostAsJsonAsync("Marcas/RegistrarMarca", infoMarca).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar una marca: {0}",e.Message));
            }
            return msjMarcas;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar una Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesMarcas ActualizarMarca(Marcas infoMarca,bool actEstado)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "Marcas/ActualizarEstadoMarca" : "Marcas/ActualizarMarca", infoMarca).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar una marca: {0}",e.Message));
            }
            return msjMarcas;
        }
        #endregion
    }
}