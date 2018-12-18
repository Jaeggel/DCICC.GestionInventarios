using DCICC.Entidades.EntidadesInventarios;
using log4net;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DCICC.WebServiceInventarios.Configuration
{
    public class SentenciasRoles
    {
        //Instancia para la utilización de LOGS en la clase ConfigSeguridad
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ConfigurationBuilder configuration_Builder = new ConfigurationBuilder();
        string path_ArchivoConfig = string.Empty;
        IConfigurationRoot root_Config;
        Sentencias obj_Sentencias = new Sentencias();
        /// <summary>
        /// Constructor para llamar a los métodos que se encargan de obtener el path del JSON de Sentencias para Roles. 
        /// </summary>
        public SentenciasRoles()
        {
            configuration_Builder = new ConfigurationBuilder();
            path_ArchivoConfig = Path.Combine(Directory.GetCurrentDirectory(), "Configuration\\SentenciasRolesBD.json");
            configuration_Builder.AddJsonFile(path_ArchivoConfig, false);
            root_Config = configuration_Builder.Build();
            obj_Sentencias = ObtenerSentenciasComp();
        }
        /// <summary>
        /// Método para obtener el JSON completo sobre las sentencias para roles
        /// </summary>
        /// <returns></returns>
        public Sentencias ObtenerSentenciasComp()
        {
            Sentencias items = new Sentencias();
            using (StreamReader r = new StreamReader(path_ArchivoConfig))
            {
                path_ArchivoConfig = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Sentencias>(path_ArchivoConfig);
            }
            return items;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias generales.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasGenerales(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasGenerales)
            {
                lstSentencias.Add(string.Format(item,nombreRol));
            }
            return lstSentencias;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias de activos.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasActivos(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasActivos)
            {
                lstSentencias.Add(string.Format(item, nombreRol));
            }
            return lstSentencias;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias de máquinas virtuales.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasMaqVirtuales(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasMaqVirtuales)
            {
                lstSentencias.Add(string.Format(item, nombreRol));
            }
            return lstSentencias;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias de tickets.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasTickets(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasTickets)
            {
                lstSentencias.Add(string.Format(item, nombreRol));
            }
            return lstSentencias;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias de reportes.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasReportes(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasReportes)
            {
                lstSentencias.Add(string.Format(item, nombreRol));
            }
            return lstSentencias;
        }
        /// <summary>
        /// Método para obtener la lista de sentencias de revocación de roles.
        /// </summary>
        /// <param name="nombreRol">Rol que tendrá la sentencia.</param>
        /// <returns></returns>
        public List<string> ObtenerSentenciasRevocacion(string nombreRol)
        {
            List<string> lstSentencias = new List<string>();
            foreach (var item in obj_Sentencias.SentenciasRevocacion)
            {
                lstSentencias.Add(string.Format(item, nombreRol));
            }
            return lstSentencias;
        }
    }
}
