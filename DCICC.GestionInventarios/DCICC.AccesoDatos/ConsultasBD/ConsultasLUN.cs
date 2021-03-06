﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasLUN
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasLUN()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener las LUN de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaLUN o LUNhabilitados</param>
        /// <returns></returns>
        public MensajesLUN ObtenerLUN(string nombreFuncion)
        {
            List<LUN> lstLUN = new List<LUN>();
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LUN objLUN = new LUN
                            {
                                IdLUN = (int)dr[0],
                                IdStorage = (int)dr[1],
                                NombreLUN = dr[2].ToString().Trim(),
                                CapacidadLUN = dr[3].ToString().Trim(),
                                RaidTPLUN = dr[4].ToString().Trim(),
                                DescripcionLUN = dr[5].ToString().Trim(),
                                HabilitadoLUN = (bool)dr[6],
                                NombreStorage= dr[7].ToString().Trim()
                            };
                            string[] capacidadTemp = objLUN.CapacidadLUN.Split(new char[0]);
                            if (capacidadTemp.Length == 2)
                            {
                                objLUN.SizeLUN = int.Parse(capacidadTemp[0]);
                                objLUN.UnidadLUN = capacidadTemp[1];
                            }
                            else
                            {
                                objLUN.SizeLUN = int.Parse(capacidadTemp[0]);
                            }
                            lstLUN.Add(objLUN);
                        }
                        conn_BD.Close();
                        msjLUN.ListaObjetoInventarios = lstLUN;
                        msjLUN.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
    }
}
