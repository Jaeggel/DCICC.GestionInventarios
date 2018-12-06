using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasHistoricoActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasHistoricoActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Históricos de activos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesHistoricoActivos ObtenerHistoricoActivos()
        {
            List<HistoricoActivos> lstHistoricoActivos = new List<HistoricoActivos>();
            MensajesHistoricoActivos msjHistoricoActivos = new MensajesHistoricoActivos();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            HistoricoActivos objHistoricoActivos = new HistoricoActivos
                            {                                
                                NombreActivo= dr[1].ToString().Trim(),
                                NombreAccesorio = dr[2].ToString().Trim(),
                                NombreTipoActivo = dr[2].ToString().Trim(),
                                FechaModifHistActivos = DateTime.Parse(dr[2].ToString().Trim()),
                                SerialHistActivo = dr[2].ToString().Trim(),
                                ModeloHistActivo = dr[2].ToString().Trim()
                            };
                            lstHistoricoActivos.Add(objHistoricoActivos);
                        }
                        conn_BD.Close();
                        msjHistoricoActivos.ListaObjetoInventarios = lstHistoricoActivos;
                        msjHistoricoActivos.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjHistoricoActivos.OperacionExitosa = false;
                msjHistoricoActivos.MensajeError = e.Message;
            }
            return msjHistoricoActivos;
        }
    }
}
