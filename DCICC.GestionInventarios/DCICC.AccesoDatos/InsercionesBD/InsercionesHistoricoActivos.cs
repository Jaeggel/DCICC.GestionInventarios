using DCICC.Entidades.EntidadesInventarios;
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
        public MensajesHistoricoActivos RegistroHistoricoActivos(HistoricoActivos infoHistoricoActivos)
        {
            MensajesHistoricoActivos msjHistoricoActivos = new MensajesHistoricoActivos();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO public.dcicc_historicoactivos (id_detalleact,id_accesorio, fechamodif_histactivos) VALUES (@ida,@idac, @fmh);", conn_BD))
                {
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoHistoricoActivos.IdDetActivo.ToString()) ? (object)infoHistoricoActivos.IdDetActivo : DBNull.Value;
                    cmd.Parameters.Add("idac", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoHistoricoActivos.IdAccesorio.ToString()) ? (object)infoHistoricoActivos.IdAccesorio : DBNull.Value;
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
