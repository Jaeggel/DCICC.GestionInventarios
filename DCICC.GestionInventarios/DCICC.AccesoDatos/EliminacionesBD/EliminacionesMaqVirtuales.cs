using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.EliminacionesBD
{
    public class EliminacionesMaqVirtuales
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public EliminacionesMaqVirtuales()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para eliminar una máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtuales"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales EliminacionMaqVirtual(MaqVirtuales infoMaqVirtuales)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM public.dcicc_maqvirtuales WHERE id_maqvirtuales=@im;", conn_BD))
                {
                    cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtuales.IdMaqVirtuales;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjMaqVirtuales.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjMaqVirtuales.OperacionExitosa = false;
                msjMaqVirtuales.MensajeError = e.Message;
            }
            return msjMaqVirtuales;
        }
    }
}
