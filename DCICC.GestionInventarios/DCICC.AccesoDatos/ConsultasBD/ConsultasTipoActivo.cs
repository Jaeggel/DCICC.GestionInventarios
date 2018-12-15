using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasTipoActivo
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasTipoActivo()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Tipos de Activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultatipoActivo o tipoactivohabilitados</param>
        /// <returns></returns>
        public MensajesTipoActivo ObtenerTipoActivo(string nombreFuncion)
        {
            List<TipoActivo> lstTipoActivo = new List<TipoActivo>();
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (nombreFuncion == "tipoactivohabilitados")
                        {
                            while (dr.Read())
                            {
                                TipoActivo objTipoActivo = new TipoActivo
                                {
                                    IdTipoActivo = (int)dr[0],
                                    IdCategoriaActivo = (int)dr[1],
                                    NombreTipoActivo = dr[2].ToString().Trim(),
                                    DescripcionTipoActivo = dr[3].ToString().Trim(),
                                    VidaUtilTipoActivo = (int)dr[4],
                                    HabilitadoTipoActivo = (bool)dr[5],
                                };
                                lstTipoActivo.Add(objTipoActivo);
                            }
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                TipoActivo objTipoActivo = new TipoActivo
                                {
                                    IdTipoActivo = (int)dr[0],
                                    IdCategoriaActivo = (int)dr[1],
                                    NombreTipoActivo = dr[2].ToString().Trim(),
                                    DescripcionTipoActivo = dr[3].ToString().Trim(),
                                    VidaUtilTipoActivo = (int)dr[4],
                                    HabilitadoTipoActivo = (bool)dr[5],
                                    NombreCategoriaActivo = dr[6].ToString().Trim()
                                };
                                lstTipoActivo.Add(objTipoActivo);
                            }
                        }
                        conn_BD.Close();
                        msjTipoActivo.ListaObjetoInventarios = lstTipoActivo;
                        msjTipoActivo.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjTipoActivo.OperacionExitosa = false;
                msjTipoActivo.MensajeError = e.Message;
            }
            return msjTipoActivo;
        }
    }
}
