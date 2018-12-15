using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesMaqVirtuales
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesMaqVirtuales()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar una Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales ActualizacionMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_maqvirtuales set id_so=@iso,id_lun=@ilun,usuario_maqvirtuales=@umv,nombre_maqvirtuales = @nmv,proposito_maqvirtuales=@pmv,direccionip_maqvirtuales=@dimv,disco_maqvirtuales=@dsmv,ram_maqvirtuales=@rmv,descripcion_maqvirtuales=@dcmv,habilitado_maqvirtuales = @hmv where id_maqvirtuales = @imv", conn_BD))
                {
                    cmd.Parameters.Add("iso", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdSistOperativos;
                    cmd.Parameters.Add("ilun", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdLUN;
                    cmd.Parameters.Add("umv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.UsuarioMaqVirtuales;
                    cmd.Parameters.Add("nmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.NombreMaqVirtuales;
                    cmd.Parameters.Add("pmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.PropositoMaqVirtuales;
                    cmd.Parameters.Add("dimv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.DireccionIPMaqVirtuales;
                    cmd.Parameters.Add("rmv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.RamMaqVirtuales;
                    cmd.Parameters.Add("dsmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.DiscoMaqVirtuales;
                    cmd.Parameters.Add("dcmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoMaqVirtual.DescripcionMaqVirtuales) ? (object)infoMaqVirtual.DescripcionMaqVirtuales : DBNull.Value;
                    cmd.Parameters.Add("hmv", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMaqVirtual.HabilitadoMaqVirtuales;
                    cmd.Parameters.Add("imv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdMaqVirtuales;
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
        /// <summary>
        /// Método para actualizar el estado de una Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales ActualizacionEstadoMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_maqvirtuales set habilitado_maqvirtuales = @hmv where id_maqvirtuales = @imv", conn_BD))
                {
                    cmd.Parameters.Add("hmv", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMaqVirtual.HabilitadoMaqVirtuales;
                    cmd.Parameters.Add("imv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdMaqVirtuales;
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
