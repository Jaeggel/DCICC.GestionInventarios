﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasMarcas
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasMarcas()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener las marcas de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultamarcas o marcashabilitados</param>
        /// <returns></returns>
        public MensajesMarcas ObtenerMarcas(string nombreFuncion)
        {
            List<Marcas> lstMarcas = new List<Marcas>();
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Marcas objMarcas = new Marcas
                            {
                                IdMarca = Int32.Parse(dr[0].ToString().Trim()),
                                NombreMarca = dr[1].ToString().Trim(),
                                DescripcionMarca = dr[2].ToString().Trim(),
                                HabilitadoMarca = Boolean.Parse(dr[3].ToString().Trim())
                            };
                            lstMarcas.Add(objMarcas);
                        }
                        conn_BD.Close();
                        msjMarcas.ListaObjetoInventarios = lstMarcas;
                        msjMarcas.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjMarcas.OperacionExitosa = false;
                msjMarcas.MensajeError = e.Message;
            }
            return msjMarcas;
        }
    }
}
