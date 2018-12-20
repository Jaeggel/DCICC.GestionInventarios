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
        List<string> sentencias_Revocacion = new List<string>();
        List<string> sentencias_Generales = new List<string>();
        List<string> sentencias_Activos = new List<string>();
        List<string> sentencias_MaqVirtuales = new List<string>();
        List<string> sentencias_Tickets = new List<string>();
        List<string> sentencias_Reportes = new List<string>();
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesRoles(List<string> sentenciasRevocacion, List<string> sentenciasGenerales, List<string> sentenciasActivos, List<string> sentenciasMaqVirtuales, List<string> sentenciasTickets, List<string> sentenciasReportes)
        {
            sentencias_Revocacion = sentenciasRevocacion;
            sentencias_Generales = sentenciasGenerales;
            sentencias_Activos = sentenciasActivos;
            sentencias_MaqVirtuales = sentenciasMaqVirtuales;
            sentencias_Tickets = sentenciasTickets;
            sentencias_Reportes = sentenciasReportes;
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        public MensajesRoles ActualizacionRol(Roles infoRol)
        {
            string nombreRol = string.Empty;
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                if (infoRol.NombreRolAntiguo!=null)
                {
                    nombreRol = infoRol.NombreRol;
                    using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("ALTER GROUP {0} RENAME TO {1};",infoRol.NombreRolAntiguo,infoRol.NombreRol), conn_BD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    nombreRol = infoRol.NombreRol;
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_roles SET nombre_rol=@nr, activos_rol=@pa, maquinasvirtuales_rol=@pm, tickets_rol=@pt, reportes_rol=@pr, descripcion_rol=@dr, habilitado_rol=@hr WHERE id_rol=@ir;", conn_BD))
                {
                    cmd.Parameters.Add("nr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = nombreRol.ToLower();
                    cmd.Parameters.Add("pa", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoActivos;
                    cmd.Parameters.Add("pm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoMaqVirtuales;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoTickets;
                    cmd.Parameters.Add("pr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoReportes;
                    cmd.Parameters.Add("dr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoRol.DescripcionRol) ? (object)infoRol.DescripcionRol : DBNull.Value;
                    cmd.Parameters.Add("hr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.HabilitadoRol;
                    cmd.Parameters.Add("ir", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoRol.IdRol;
                    cmd.ExecuteNonQuery();
                }
                foreach (var item in sentencias_Revocacion)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                foreach (var item in sentencias_Generales)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                if (infoRol.PermisoActivos)
                {
                    foreach (var item in sentencias_Activos)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (infoRol.PermisoMaqVirtuales)
                {
                    foreach (var item in sentencias_MaqVirtuales)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (infoRol.PermisoTickets)
                {
                    foreach (var item in sentencias_Tickets)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (infoRol.PermisoReportes)
                {
                    foreach (var item in sentencias_Reportes)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (NpgsqlCommand cmd = new NpgsqlCommand("update dcicc_usuarios set habilitado_usuario=@hr where id_rol=@ir", conn_BD))
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
