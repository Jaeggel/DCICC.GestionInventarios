using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesRoles
    {
        NpgsqlConnection conn_BD = null;
        List<string> sentencias_Generales = new List<string>();
        List<string> sentencias_Activos = new List<string>();
        List<string> sentencias_MaqVirtuales = new List<string>();
        List<string> sentencias_Tickets = new List<string>();
        List<string> sentencias_Reportes = new List<string>();
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesRoles(List<string> sentenciasGenerales, List<string> sentenciasActivos, List<string> sentenciasMaqVirtuales, List<string> sentenciasTickets, List<string> sentenciasReportes)
        {
            sentencias_Generales = sentenciasGenerales;
            sentencias_Activos = sentenciasActivos;
            sentencias_MaqVirtuales = sentenciasMaqVirtuales;
            sentencias_Tickets = sentenciasTickets;
            sentencias_Reportes = sentenciasReportes;
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
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_roles (nombre_rol, administracion_rol, activos_rol, maquinasvirtuales_rol, tickets_rol, reportes_rol, descripcion_rol, habilitado_rol) VALUES (@nr,@pad,@pa,@pm,@pt,@pr,@dr,@hr)", conn_BD))
                {
                    cmd.Parameters.Add("nr", NpgsqlTypes.NpgsqlDbType.Varchar).Value=infoRol.NombreRol.ToLower().Trim();
                    cmd.Parameters.Add("pad", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoAdministracion;
                    cmd.Parameters.Add("pa", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoActivos;
                    cmd.Parameters.Add("pm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoMaqVirtuales;
                    cmd.Parameters.Add("pt", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoTickets;
                    cmd.Parameters.Add("pr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.PermisoReportes;
                    cmd.Parameters.Add("dr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoRol.DescripcionRol) ? (object)infoRol.DescripcionRol.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hr", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoRol.HabilitadoRol;   
                    cmd.ExecuteNonQuery();
                }
                foreach (var item in sentencias_Generales)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(item, conn_BD))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                if(infoRol.PermisoActivos)
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
    }
}
