using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class CategoriasActivosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase CategoriasAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public CategoriasActivosAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con las Categorias de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab(Los registros habilitados)</ param >
        /// <returns></returns>
        public MensajesCategoriasActivos ObtenerCategoriasActivos(string nombreFuncion)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("CategoriasActivos/ObtenerCategoriasActivos"+nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de las categorías: " + e.Message);
            }
            return msjCategorias;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar una nueva Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoria"></param>
        /// <returns></returns>
        public MensajesCategoriasActivos RegistrarCategoriaActivo(CategoriaActivo infoCategoria)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                var response = client_Service.PostAsJsonAsync("CategoriasActivos/RegistrarCategoriaActivo", infoCategoria).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar una categoría: " + e.Message);
            }
            return msjCategorias;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar una Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoria"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesCategoriasActivos ActualizarCategoriaActivo(CategoriaActivo infoCategoria, bool actEstado)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "CategoriasActivos/ActualizarEstadoCategoriaActivo" : "CategoriasActivos/ActualizarCategoriaActivo", infoCategoria).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar una categoría: " + e.Message);
            }
            return msjCategorias;
        }
        #endregion
    }
}