﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasSistOperativos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasSistOperativos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Sistemas Operativos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaso o sohabilitados</param>
        /// <returns></returns>
        public MensajesSistOperativos ObtenerSistOperativos(string nombreFuncion)
        {
            List<SistOperativos> lstSistOperativos = new List<SistOperativos>();
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SistOperativos objSistOperativos = new SistOperativos
                            {
                                IdSistOperativos = (int)dr[0],
                                NombreSistOperativos = dr[1].ToString().Trim(),
                                DescripcionSistOperativos = dr[2].ToString().Trim(),
                                HabilitadoSistOperativos = (bool)dr[3]
                            };
                            lstSistOperativos.Add(objSistOperativos);
                        }
                        conn_BD.Close();
                        msjSistOperativos.ListaObjetoInventarios = lstSistOperativos;
                        msjSistOperativos.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjSistOperativos.OperacionExitosa = false;
                msjSistOperativos.MensajeError = e.Message;
            }
            return msjSistOperativos;
        }
    }
}
