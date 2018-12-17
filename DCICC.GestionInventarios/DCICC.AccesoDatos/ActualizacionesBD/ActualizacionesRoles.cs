using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesRoles
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesRoles()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles ActualizacionRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("...", conn_BD))
                {
                    cmd.Parameters.Add("nr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoRol.NombreRol;
                    cmd.Parameters.Add("dr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoRol.DescripcionRol) ? (object)infoRol.DescripcionRol : DBNull.Value;
                    cmd.Parameters.Add("hr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.HabilitadoRol;
                    cmd.Parameters.Add("pa", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoActivos;
                    cmd.Parameters.Add("pm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoMaqVirtuales;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoTickets;
                    cmd.Parameters.Add("pr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoReportes;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjRoles.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjRoles.OperacionExitosa = false;
                msjRoles.MensajeError = e.Message;
            }
            return msjRoles;
        }
        /// <summary>
        /// Método para actualizar el estado de un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles ActualizacionEstadoRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_roles set habilitado_rol = @hr where id_rol = @ir", conn_BD))
                {
                    cmd.Parameters.Add("hr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.HabilitadoRol;
                    cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoRol.IdRol;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjRoles.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjRoles.OperacionExitosa = false;
                msjRoles.MensajeError = e.Message;
            }
            return msjRoles;
        }
    }
}
