﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        /// Método para obtener los tipos de activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultatipoActivo o tipoactivohabilitados</param>
        /// <returns></returns>
        public MensajesTipoActivo ObtenerTipoActivo(string nombreFuncion)
        {
            List<TipoActivo> lstTipoActivo = new List<TipoActivo>();
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TipoActivo objTipoActivo = new TipoActivo
                            {
                                IdTipoActivo = int.Parse(dr[0].ToString().Trim()),
                                IdCategoriaActivo= int.Parse(dr[1].ToString().Trim()),
                                NombreTipoActivo = dr[2].ToString().Trim(),
                                DescripcionTipoActivo = dr[3].ToString().Trim(),
                                VidaUtilTipoActivo=int.Parse(dr[4].ToString().Trim()),
                                HabilitadoTipoActivo = bool.Parse(dr[5].ToString().Trim()),
                                NombreCategoriaActivo = dr[6].ToString().Trim()
                            };
                            lstTipoActivo.Add(objTipoActivo);
                        }
                        conn_BD.Close();
                        msjTipoActivo.ListaObjetoInventarios = lstTipoActivo;
                        msjTipoActivo.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjTipoActivo.OperacionExitosa = false;
                msjTipoActivo.MensajeError = e.Message;
            }
            return msjTipoActivo;
        }
    }
}
