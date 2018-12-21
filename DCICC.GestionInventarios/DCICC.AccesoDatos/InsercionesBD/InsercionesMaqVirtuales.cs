using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesMaqVirtuales
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesMaqVirtuales()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nueva Máquina Virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales RegistroMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_maqvirtuales (id_so,id_lun,usuario_maqvirtuales,nombre_maqvirtuales,proposito_maqvirtuales,direccionip_maqvirtuales,disco_maqvirtuales,ram_maqvirtuales,descripcion_maqvirtuales,habilitado_maqvirtuales) VALUES (@iso,@ilun,@umv,@nmv,@pmv,@dimv,@dsmv,@rmv,@dcmv,@hmv)", conn_BD))
                {
                    cmd.Parameters.Add("iso", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdSistOperativos;
                    cmd.Parameters.Add("ilun", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdLUN;
                    cmd.Parameters.Add("umv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.UsuarioMaqVirtuales.Trim();
                    cmd.Parameters.Add("nmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.NombreMaqVirtuales.Trim();
                    cmd.Parameters.Add("pmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.PropositoMaqVirtuales.Trim();
                    cmd.Parameters.Add("dimv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.DireccionIPMaqVirtuales.Trim();
                    cmd.Parameters.Add("rmv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.RamMaqVirtuales;
                    cmd.Parameters.Add("dsmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.DiscoMaqVirtuales;
                    cmd.Parameters.Add("dcmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoMaqVirtual.DescripcionMaqVirtuales) ? (object)infoMaqVirtual.DescripcionMaqVirtuales.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hmv", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMaqVirtual.HabilitadoMaqVirtuales;
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
