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
                        while (dr.Read())
                        {
                            Roles objRoles = new Roles
                            {
                                IdRol = (int)dr[0],
                                NombreRol= dr[1].ToString().Trim(),
                                DescripcionRol= dr[2].ToString().Trim(),
                                HabilitadoRol = (bool)dr[3]
                            };
                            lstRoles.Add(objRoles);
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
        public static MensajesRoles ObtenerRolPorId(int IdRol)
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
                //conn_BD.Close();
                msjRoles.OperacionExitosa = false;
                msjRoles.MensajeError = e.Message;
            }
            return msjRoles;
        }
    }
}
