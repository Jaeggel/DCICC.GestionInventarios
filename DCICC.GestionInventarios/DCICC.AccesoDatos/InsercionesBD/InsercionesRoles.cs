using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesRoles
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesRoles()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles RegistroRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_roles (nombre_rol,descripcion_rol,habilitado_rol,) VALUES (@nr,@dr,@hr,@pa,@pm,@pt,@pr)", conn_BD))
                {
                    cmd.Parameters.Add("nr", NpgsqlTypes.NpgsqlDbType.Varchar).Value=infoRol.NombreRol;
                    cmd.Parameters.Add("dr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoRol.DescripcionRol) ? (object)infoRol.DescripcionRol : DBNull.Value;
                    cmd.Parameters.Add("hr", NpgsqlTypes.NpgsqlDbType.Boolean).Value=infoRol.HabilitadoRol;
                    cmd.Parameters.Add("pa", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoActivos;
                    cmd.Parameters.Add("pm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoMaqVirtuales;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoTickets;
                    cmd.Parameters.Add("pr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoReportes;
                    cmd.ExecuteNonQuery();
                }
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
