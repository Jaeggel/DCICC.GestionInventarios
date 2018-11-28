using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método para ingresar una nuevo nuevo activo en el historico en la base de datos.
        /// </summary>
        /// <param name="infoHistoricoActivos"></param>
        /// <returns></returns>
        public MensajesHistoricoActivos RegistroHistoricoActivos(HistoricoActivos infoHistoricoActivos)
        {
            MensajesHistoricoActivos msjHistoricoActivos = new MensajesHistoricoActivos();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO public.dcicc_historicoactivos (id_detalleact, fechamodif_histactivos) VALUES (@ida, @fmh);", conn_BD))
                {
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoHistoricoActivos.IdDetActivo;
                    cmd.Parameters.AddWithValue("fmh", infoHistoricoActivos.FechaModifHistActivos);
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjHistoricoActivos.OperacionExitosa = true;
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
