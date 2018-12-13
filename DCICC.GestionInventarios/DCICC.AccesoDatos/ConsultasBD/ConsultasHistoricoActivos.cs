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
                using (NpgsqlCommand cmd = new NpgsqlCommand("historicostotales", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            HistoricoActivos objHistoricoActivos = new HistoricoActivos
                            {                   
                                IdHistActivos= dr[0] != DBNull.Value ? (int)dr[0] : 0,
                                IdActivo = dr[1] != DBNull.Value ? (int)dr[1] : 0,
                                FechaModifHistActivos = DateTime.Parse(dr[2].ToString().Trim()),
                                IdAccesorio= dr[3] != DBNull.Value ? (int)dr[3] : 0,
                                NombreActivo = dr[4].ToString().Trim(),
                                ModeloHistActivo = dr[5].ToString().Trim(),
                                SerialHistActivo = dr[6].ToString().Trim(),
                                NombreAccesorio = dr[7].ToString().Trim(),
                                ModeloHistAccesorio= dr[8].ToString().Trim(),
                                SerialHistAccesorio= dr[9].ToString().Trim(),

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
