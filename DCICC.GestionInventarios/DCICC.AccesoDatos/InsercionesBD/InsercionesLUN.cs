using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesLUN
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesLUN()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nueva LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        public MensajesLUN RegistroLUN(LUN infoLUN)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.dcicc_lun(id_storage, nombre_lun, capacidad_lun, raid_tp_lun, descripcion_lun, habilitado_lun) VALUES (@is, @nl, @cl, @rl, @dl, @hl);", conn_BD))
                {
                    cmd.Parameters.Add("is", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLUN.IdStorage;
                    cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.NombreLUN;
                    cmd.Parameters.Add("cl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.CapacidadLun;
                    cmd.Parameters.Add("rl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.RaidTPLUN;
                    cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Text).Value = !string.IsNullOrEmpty(infoLUN.DescripcionLUN) ? (object)infoLUN.DescripcionLUN: DBNull.Value;
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
    }
}
