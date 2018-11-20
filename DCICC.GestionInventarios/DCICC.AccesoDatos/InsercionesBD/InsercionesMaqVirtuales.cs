using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método para ingresar una nueva máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales RegistroMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_maqvirtuales (id_so,usuario_maqvirtuales,nombre_maqvirtuales,proposito_maqvirtuales,direccion_maqvirtuales,disco_maqvirtuales,rammaqvirtuales,descripcion_maqvirtuales,habilitado_maqvirtuales) VALUES (@iso,@umv,@nmv,@pmv,@dimv,@dsmv,@rmv,@dcmv,@hmv)", conn_BD))
                {
                    cmd.Parameters.Add("iso", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.IdSistOperativos;
                    cmd.Parameters.Add("umv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.UsuarioMaqVirtuales;
                    cmd.Parameters.Add("nmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.NombreMaqVirtuales;
                    cmd.Parameters.Add("pmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.PropositoMaqVirtuales;
                    cmd.Parameters.Add("dimv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMaqVirtual.DireccionIPMaqVirtuales;
                    cmd.Parameters.Add("dsmv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.RamMaqVirtuales;
                    cmd.Parameters.Add("rmv", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMaqVirtual.DiscoMaqVirtuales;
                    cmd.Parameters.Add("dcmv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoMaqVirtual.DescripcionMaqVirtuales) ? (object)infoMaqVirtual.DescripcionMaqVirtuales : DBNull.Value;
                    cmd.Parameters.Add("hmv", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMaqVirtual.HabilitadoMaqVirtuales;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjMaqVirtuales.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjMaqVirtuales.OperacionExitosa = false;
                msjMaqVirtuales.MensajeError = e.Message;
            }
            return msjMaqVirtuales;
        }
    }
}
