using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos
{
    public class ConfigBaseDatos
    {
        static string cadena_Conexion = string.Empty;
        /// <summary>
        /// Método para inicializar la cadena de conexión proveniente del appsettings.json 
        /// </summary>
        /// <param name="cadenaConexionWS"></param>
        public static void SetCadenaConexion(string cadenaConexionWS)
        {
            cadena_Conexion = cadenaConexionWS;
        }
        /// <summary>
        /// Método para realizar la conexión con la base de datos
        /// </summary>
        /// <returns></returns>
        public static NpgsqlConnection ConnectDB()
        {
            NpgsqlConnection connBD = null;
            try
            {
                connBD = new NpgsqlConnection(cadena_Conexion);
                connBD.Open();
            }
            catch (Exception e)
            {
                return null;
            }
            return connBD;
        }
    }
}
