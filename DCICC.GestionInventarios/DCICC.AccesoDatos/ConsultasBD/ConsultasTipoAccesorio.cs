using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasTipoAccesorio
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasTipoAccesorio()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Tipos de Accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultatipoaccesorio o tipoaccesoriohabilitados</param>
        /// <returns></returns>
        public MensajesTipoAccesorio ObtenerTipoAccesorio(string nombreFuncion)
        {
            List<TipoAccesorio> lstTipoAccesorio = new List<TipoAccesorio>();
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TipoAccesorio objTipoAccesorio = new TipoAccesorio
                            {
                                IdTipoAccesorio = (int)dr[0],
                                NombreTipoAccesorio = dr[1].ToString().Trim(),
                                DescripcionTipoAccesorio = dr[2].ToString().Trim(),
                                HabilitadoTipoAccesorio = (bool)dr[3]
                            };
                            lstTipoAccesorio.Add(objTipoAccesorio);
                        }
                        conn_BD.Close();
                        msjTipoAccesorio.ListaObjetoInventarios = lstTipoAccesorio;
                        msjTipoAccesorio.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjTipoAccesorio.OperacionExitosa = false;
                msjTipoAccesorio.MensajeError = e.Message;
            }
            return msjTipoAccesorio;
        }
    }
}
