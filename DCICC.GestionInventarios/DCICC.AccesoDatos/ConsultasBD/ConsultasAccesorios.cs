﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasAccesorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasAccesorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        
        /// <summary>
        /// Método para obtener los Accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaaccesorios o accesorioshabilitados</param>
        /// <returns></returns>
        public MensajesAccesorios ObtenerAccesorios(string nombreFuncion)
        {
            List<Accesorios> lstAccesorios = new List<Accesorios>();
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(nombreFuncion== "accesoriostotales")
                        {
                            while (dr.Read())
                            {
                                Accesorios objAccesorios = new Accesorios
                                {
                                    IdAccesorio = (int)dr[0],
                                    IdTipoAccesorio = (int)dr[1],
                                    IdDetalleActivo = (int)dr[2],
                                    NombreAccesorio = dr[3].ToString().Trim(),
                                    SerialAccesorio = dr[4].ToString().Trim(),
                                    ModeloAccesorio = dr[5].ToString().Trim(),
                                    DescripcionAccesorio = dr[6].ToString().Trim(),
                                    EstadoAccesorio = dr[7].ToString().Trim(),
                                    IdCQR = dr[8].ToString().Trim(),
                                    NombreTipoAccesorio = dr[9].ToString().Trim(),
                                    NombreDetalleActivo = dr[10].ToString().Trim()
                                };
                                lstAccesorios.Add(objAccesorios);
                            }
                        }else if(nombreFuncion== "accesorioshabilitados")
                        {
                            while (dr.Read())
                            {
                                Accesorios objAccesorios = new Accesorios
                                {
                                    IdAccesorio = int.Parse(dr[0].ToString().Trim()),
                                    IdTipoAccesorio = int.Parse(dr[1].ToString().Trim()),
                                    IdDetalleActivo = int.Parse(dr[2].ToString().Trim()),
                                    NombreAccesorio = dr[3].ToString().Trim(),
                                    SerialAccesorio = dr[4].ToString().Trim(),
                                    ModeloAccesorio = dr[5].ToString().Trim(),
                                    DescripcionAccesorio = dr[6].ToString().Trim(),
                                    EstadoAccesorio = dr[7].ToString().Trim(),
                                    IdCQR = dr[8].ToString().Trim(),
                                };
                                lstAccesorios.Add(objAccesorios);
                            }
                        }
                        else if(nombreFuncion == "accesorioscqr")
                        {
                            while (dr.Read())
                            {
                                Accesorios objAccesorios = new Accesorios
                                {
                                    IdAccesorio = int.Parse(dr[0].ToString().Trim()),
                                    IdTipoAccesorio = int.Parse(dr[1].ToString().Trim()),
                                    IdDetalleActivo = int.Parse(dr[2].ToString().Trim()),
                                    IdCQR = dr[3].ToString().Trim(),
                                    NombreAccesorio = dr[4].ToString().Trim(),
                                    EstadoAccesorio = dr[5].ToString().Trim(),
                                    NombreTipoAccesorio = dr[6].ToString().Trim(),
                                    NombreDetalleActivo = dr[7].ToString().Trim(),
                                    ImpresoCQR = (bool)dr[8]
                                };
                                lstAccesorios.Add(objAccesorios);
                            }
                        }
                        conn_BD.Close();
                        msjAccesorios.ListaObjetoInventarios = lstAccesorios;
                        msjAccesorios.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjAccesorios.OperacionExitosa = false;
                msjAccesorios.MensajeError = e.Message;
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método para obtener los Accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaaccesorios o accesorioshabilitados</param>
        /// <returns></returns>
        public MensajesAccesorios ObtenerNombresAccesorios()
        {
            List<Accesorios> lstAccesorios = new List<Accesorios>();
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre_accesorio,id_cqr,nombre_detalleact FROM accesoriostotales();", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Accesorios objAccesorios = new Accesorios
                            {
                                NombreAccesorio = dr[0].ToString().Trim(),
                                IdCQR = dr[1].ToString().Trim(),
                                NombreDetalleActivo=dr[2].ToString().Trim()
                            };
                            lstAccesorios.Add(objAccesorios);
                        }
                        conn_BD.Close();
                        msjAccesorios.ListaObjetoInventarios = lstAccesorios;
                        msjAccesorios.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjAccesorios.OperacionExitosa = false;
                msjAccesorios.MensajeError = e.Message;
            }
            return msjAccesorios;
        }
        public MensajesAccesorios ObtenerAccesorioPorIdCQR(string idCQR)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            Accesorios objAccesorios = new Accesorios();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from accesoriostotales() where id_cqr=@cqr", conn_BD))
                {
                    cmd.Parameters.Add("cqr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idCQR;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objAccesorios = new Accesorios
                            {
                                IdAccesorio = (int)dr[0],
                                IdTipoAccesorio = (int)dr[1],
                                IdDetalleActivo = (int)dr[2],
                                NombreAccesorio = dr[3].ToString().Trim(),
                                SerialAccesorio = dr[4].ToString().Trim(),
                                ModeloAccesorio = dr[5].ToString().Trim(),
                                DescripcionAccesorio = dr[6].ToString().Trim(),
                                EstadoAccesorio = dr[7].ToString().Trim(),
                                IdCQR = dr[8].ToString().Trim(),
                                NombreTipoAccesorio = dr[9].ToString().Trim(),
                                NombreDetalleActivo = dr[10].ToString().Trim()
                            };
                        }
                        conn_BD.Close();
                        msjAccesorios.ObjetoInventarios = objAccesorios;
                        msjAccesorios.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjAccesorios.OperacionExitosa = false;
                msjAccesorios.MensajeError = e.Message;
            }
            return msjAccesorios;
        }
    }
}
