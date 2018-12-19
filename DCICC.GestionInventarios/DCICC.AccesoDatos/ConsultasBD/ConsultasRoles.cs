using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasRoles
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasRoles()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Roles de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaroles o roleshabilitados</param>
        /// <returns></returns>
        public MensajesRoles ObtenerRoles(string nombreFuncion)
        {
            List<Roles> lstRoles = new List<Roles>();
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (nombreFuncion == "roleshabilitados" || nombreFuncion=="consultaroles")
                        {
                            while (dr.Read())
                            {
                                Roles objRoles = new Roles
                                {
                                    IdRol = (int)dr[0],
                                    NombreRol = dr[1].ToString().Trim(),
                                    PermisoAdministracion = (bool)dr[2],
                                    PermisoActivos = (bool)dr[3],
                                    PermisoMaqVirtuales = (bool)dr[4],
                                    PermisoTickets = (bool)dr[5],
                                    PermisoReportes = (bool)dr[6],
                                    DescripcionRol = dr[7].ToString().Trim(),
                                    HabilitadoRol = (bool)dr[8]
                                };
                                lstRoles.Add(objRoles);
                            }
                        }
                        conn_BD.Close();
                        msjRoles.ListaObjetoInventarios = lstRoles;
                        msjRoles.OperacionExitosa = true;
                    }
                }
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
        /// Método para obtener un Rol en específico de la base de datos por su Id.
        /// </summary>
        /// <param name="IdRol"></param>
        /// <returns></returns>
        public MensajesRoles ObtenerRolPorId(int IdRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                ConsultasRoles objConsultasRolesBD = new ConsultasRoles();
                MensajesRoles msjRolesConsulta = objConsultasRolesBD.ObtenerRoles("consultaroles");
                Roles infoRol = msjRolesConsulta.ListaObjetoInventarios.Find(x => x.IdRol == IdRol);
                if (infoRol != null)
                {
                    msjRoles.ObjetoInventarios = infoRol;
                    msjRoles.OperacionExitosa = true;
                }
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
        /// Método para obtener los permisos de un rol por su nombre.
        /// </summary>
        /// <param name="nombreRol"></param>
        /// <returns></returns>
        public MensajesRoles ObtenerPermisosPorRol(string nombreRol)
        {
            Roles objRoles = new Roles();
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select nombre_rol,activos_rol,maquinasvirtuales_rol,tickets_rol,reportes_rol from dcicc_roles where nombre_rol=@nr", conn_BD))
                {
                    cmd.Parameters.Add("nr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = nombreRol;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objRoles = new Roles
                            {
                                NombreRol = (string)dr[0],
                                PermisoActivos = (bool)dr[1],
                                PermisoMaqVirtuales = (bool)dr[2],
                                PermisoTickets = (bool)dr[3],
                                PermisoReportes = (bool)dr[4],
                            };
                        }
                        conn_BD.Close();
                        msjRoles.ObjetoInventarios = objRoles;
                        msjRoles.OperacionExitosa = true;
                    }
                }
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
