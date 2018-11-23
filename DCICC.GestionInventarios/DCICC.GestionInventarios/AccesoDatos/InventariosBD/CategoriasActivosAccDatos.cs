using DCICC.GestionInventarios.Models;
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
    public class CategoriasActivosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase CategoriasAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public CategoriasActivosAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con las categorias habilitadas de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab(Los registros habilitados)</ param >
        /// <returns></returns>
        public MensajesCategoriasActivos ObtenerCategoriasActivos(string nombreFuncion)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("CategoriasActivos/ObtenerCategoriasActivos"+nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de las categorías: " + e.Message + " - " + msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método para registrar una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoria"></param>
        /// <returns></returns>
        public MensajesCategoriasActivos RegistrarCategoriaActivo(CategoriaActivo infoCategoria)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("CategoriasActivos/RegistrarCategoriaActivo", infoCategoria).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar una categoría: " + e.Message + " - " + msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
        /// <summary>
        /// Método para actualizar el estado de una categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoria"></param>
        /// <returns></returns>
        public MensajesCategoriasActivos ActualizarCategoriaActivo(CategoriaActivo infoCategoria, bool actEstado)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync(actEstado ? "CategoriasActivos/ActualizarEstadoCategoriaActivo" : "CategoriasActivos/ActualizarCategoriaActivo", infoCategoria).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjCategorias = JsonConvert.DeserializeObject<MensajesCategoriasActivos>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar una categoría: " + e.Message + " - " + msjCategorias.MensajeError);
            }
            return msjCategorias;
        }
    }
}