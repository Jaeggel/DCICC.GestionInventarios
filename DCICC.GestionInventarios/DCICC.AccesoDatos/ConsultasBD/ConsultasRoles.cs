using DCICC.Entidades.EntidadesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        /// Método para obtener los roles habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Roles> ObtenerRolesHab()
        {
            List<Roles> lstRoles = new List<Roles>();
            try
            {
                using (var cmd = new NpgsqlCommand("roleshabilitados", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Roles objRoles = new Roles
                            {
                                IdRol = Int32.Parse(dr[0].ToString().Trim()),
                                NombreRol= dr[1].ToString().Trim(),
                                DescripcionRol= dr[2].ToString().Trim(),
                                HabilitadoRol = Boolean.Parse(dr[3].ToString().Trim())
                            };
                            lstRoles.Add(objRoles);
                        }
                        conn_BD.Close();
                    }
                }
            }
            catch (Exception)
            {
                lstRoles = null;
            }
            return lstRoles;
        }
    }
}
