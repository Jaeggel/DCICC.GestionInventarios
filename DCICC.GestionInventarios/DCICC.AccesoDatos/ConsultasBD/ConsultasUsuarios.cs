using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasUsuarios()
        {
            conn_BD=ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los usuarios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultausuarios o usuarioshabilitados</param>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarios(string nombreFuncion)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            List<Usuarios> lstUsuarios = new List<Usuarios>();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuarios objUsuarios = new Usuarios
                            {
                                IdUsuario = int.Parse(dr[0].ToString()),
                                IdRol = int.Parse(dr[1].ToString().Trim()),
                                NombresUsuario = dr[2].ToString().Trim(),
                                NickUsuario = dr[3].ToString().Trim(),
                                PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                CorreoUsuario = dr[5].ToString().Trim(),
                                TelefonoUsuario = dr[6].ToString().Trim(),
                                TelefonoCelUsuario = dr[7].ToString().Trim(),
                                DireccionUsuario = dr[8].ToString().Trim(),
                                HabilitadoUsuario = bool.Parse(dr[9].ToString().Trim())
                            };
                            lstUsuarios.Add(objUsuarios);
                        }
                        conn_BD.Close();
                        msjUsuarios.ListaObjetoInventarios = lstUsuarios;
                        msjUsuarios.OperacionExitosa = true;
                    }
                }                
            }
            catch (Exception e)
            {
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener todos los usuarios de la funcion UsuariosRoles de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuariosRoles()
        {
            List<Usuarios> lstUsuariosRoles = new List<Usuarios>();
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                using (var cmd = new NpgsqlCommand("usuariosRoles", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuarios objUsuariosRoles = new Usuarios
                            {
                                IdUsuario = Int32.Parse(dr[0].ToString()),
                                IdRol = Int32.Parse(dr[1].ToString().Trim()),
                                NombresUsuario = dr[2].ToString().Trim(),
                                NickUsuario = dr[3].ToString().Trim(),
                                PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                CorreoUsuario = dr[5].ToString().Trim(),
                                TelefonoUsuario = dr[6].ToString().Trim(),
                                TelefonoCelUsuario = dr[7].ToString().Trim(),
                                DireccionUsuario = dr[8].ToString().Trim(),
                                HabilitadoUsuario = Boolean.Parse(dr[9].ToString().Trim()),
                                NombreRol = dr[10].ToString().Trim()
                            };
                            lstUsuariosRoles.Add(objUsuariosRoles);
                        }
                        conn_BD.Close();
                        msjUsuarios.ListaObjetoInventarios = lstUsuariosRoles;
                        msjUsuarios.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener un usuario en específico de la base de datos por su ID.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public static MensajesUsuarios ObtenerUsuarioPorId(int IdUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                MensajesUsuarios msjUsuariosConsulta = objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");
                Usuarios infoUsuarioBD = msjUsuariosConsulta.ListaObjetoInventarios.Find(x => x.IdUsuario == IdUsuario);
                if (infoUsuarioBD != null)
                {
                    msjUsuarios.ObjetoInventarios = infoUsuarioBD;
                    msjUsuarios.OperacionExitosa = true;
                }
            }
            catch (Exception e)
            {
                msjUsuarios.OperacionExitosa = false;
                msjUsuarios.MensajeError = e.Message;
                msjUsuarios.ListaObjetoInventarios = null;
            }
            return msjUsuarios;
        }
    }
}
