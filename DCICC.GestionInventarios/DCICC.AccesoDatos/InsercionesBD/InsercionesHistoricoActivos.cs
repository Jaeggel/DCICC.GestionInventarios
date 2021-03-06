﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesHistoricoActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesHistoricoActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nuevo nuevo Activo en el Historico de Activos en la base de datos.
        /// </summary>
        /// <param name="infoHistoricoActivos"></param>
        /// <returns></returns>
        public void RegistroHistoricoActivos(HistoricoActivos infoHistoricoActivos)
        {    
            using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_historicoactivos(id_detalleact, fechamodif_histactivos, id_accesorio) VALUES (@ida,@fmh,@idac);", conn_BD))
            {
                cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoHistoricoActivos.IdActivo==0 ? DBNull.Value: (object)infoHistoricoActivos.IdActivo;
                cmd.Parameters.AddWithValue("fmh", infoHistoricoActivos.FechaModifHistActivos);
                cmd.Parameters.Add("idac", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoHistoricoActivos.IdAccesorio == 0 ? DBNull.Value : (object)infoHistoricoActivos.IdAccesorio;
                cmd.ExecuteNonQuery();
            }
            conn_BD.Close();
        }
    }
}
